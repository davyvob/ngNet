using CodingChallenge.Core.Dtos.Answers;
using CodingChallenge.Core.Dtos.CodingChallenge;
using CodingChallenge.Core.Dtos.CodingChallenge.Completed;
using CodingChallenge.Core.Dtos.Stats;
using CodingChallenge.Core.Dtos.Users;
using CodingChallenge.Core.Entities;
using CodingChallenge.Core.Infrastructure;
using CodingChallenge.Core.Managers.Interfaces;
using CodingChallenge.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Services
{
    public class CodingChallengeService : ICodingChallengeService
    {
        private readonly ICodeChallengeRepository _CodeChallengeRepo;
        private readonly IUserCodingChallengeRepository _CompletedCodeChallengeRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGameManager _gameManager;
        private readonly IPlayerStatsRepository _PlayerRepo;
        private readonly IValidatorService _ValidatorService;
        public CodingChallengeService(ICodeChallengeRepository CodeChallengeRepo,
                                      UserManager<ApplicationUser> userManager,
                                      IGameManager gameManager,
                                      IUserCodingChallengeRepository CompletedCodeChallengeRepo,
                                      IPlayerStatsRepository PlayerRepo ,
                                      IValidatorService ValidatorService)
        {
            _CodeChallengeRepo = CodeChallengeRepo;
            _userManager = userManager;
            _gameManager = gameManager;
            _CompletedCodeChallengeRepo = CompletedCodeChallengeRepo;
            _PlayerRepo = PlayerRepo;
            _ValidatorService = ValidatorService;
        }

        public async Task<BaseServiceResponse<IEnumerable<CodingChallengeResponseDto>>> GetAll()
        {
            try
            {
                var all = await _CodeChallengeRepo.ListAllAsync();
                var dtos = new List<CodingChallengeResponseDto>();  
                all.ToList().ForEach(c =>
                {
                    var dto = MapToCodingChallengeResponseDto(c);
                    dtos.Add(dto);

                });

                return new BaseServiceResponse<IEnumerable<CodingChallengeResponseDto>>
                {
                    Data = dtos,
                    IsSucces = true
                };
            }

            catch(Exception ex)
            {
                return new BaseServiceResponse<IEnumerable<CodingChallengeResponseDto>>
                {   
                    ErrorMessages = new List<string> { new string(ex.Message) },
                    IsSucces = false,                  
                };
            }
        }


        public async Task<BaseServiceResponse<UserCodingChallengeResponse>> ShowCurrent(string userId)
        {
            BaseServiceResponse<UserCodingChallengeResponse> responseObject = new();
            var user = await _userManager.FindByIdAsync(userId);
            var currentChallenge = await _CodeChallengeRepo.GetByNumber(user.CurrentChallengeNumber);

            if (user is not null && currentChallenge is not null)
            {
                var challenges = await _CodeChallengeRepo.ListAllAsync();
                if (user.CurrentChallengeNumber <= challenges.ToList().Count)
                {
                    
                    responseObject.Data = MapToUserCodingChallengeResponseDto(currentChallenge, user);
                    responseObject.IsSucces = true;
                }
                else
                {
                    responseObject.IsSucces = false;
                    responseObject.ErrorMessages.Add("Error has finished all Puzzles");
                }

            }
            else
            {
                responseObject.IsSucces = false;
                responseObject.ErrorMessages.Add("Unable to retrieve the User or Challenge details");
            }
            return responseObject;
        }

        public async Task<BaseServiceResponse<AnswerResponseDto>> AnswerCurrent(string userId , AnswerRequestDto answer)
        {
           
            BaseServiceResponse<AnswerResponseDto> responseObject = new();
            AnswerResponseDto responseDto = new();
            var user = await _userManager.FindByIdAsync(userId);
            var currentChallenge = await _CodeChallengeRepo.GetByNumber(user.CurrentChallengeNumber);
            //
            var userGameStats = await _PlayerRepo.GetByUserIdAsync(userId);

            // 1check if gamestats exists
            // 2 make a single function to update AND SAVE in db
            // the stats changes based on bool parameter
            //
            //

            if (user is not null)
            {
                bool isCorrect = await _gameManager.VerifyCodingChallengeAnswer(answer, user);
                if (isCorrect)
                {   

                    responseDto.IsCorrect = true;
                    responseObject.IsSucces = true;
                    responseObject.Data = responseDto;
                    
                    await AddCompletedChallengeToUserChallenges(user , currentChallenge);
                    await UpdateUserAfterCorrectAnswer(user);
                }
                else
                {
                    responseDto.IsCorrect = false;
                    responseObject.IsSucces = true;
                    await _gameManager.CheckIfHigherAndClose(user , answer , responseDto);
                    responseObject.Data = responseDto;
                }
            }
            else
            {
                responseObject.IsSucces = false;
                responseObject.ErrorMessages.Add("Unable to retrieve the User details");
            }
            return responseObject;
        }

        public async Task<BaseServiceResponse<CompletedUserCodeChallengeListDto>> GetCompletedChallengesForUser(string userId)
        {
            BaseServiceResponse<CompletedUserCodeChallengeListDto> responseObject = new();
            CompletedUserCodeChallengeListDto responseDtoList = new();
            var user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var allChallenges = await _CompletedCodeChallengeRepo.GetAllByUserIdAsync(userId);
                responseDtoList.CompletedChallenges = allChallenges.Select(c => new CompletedUserCodeChallengeDto
                {
                    PuzzleInput = c.PuzzleInput,
                    ChallengeNumber = c.ChallengeNumber,
                    Description = c.Description,
                    Solution = c.Solution,
                }).ToList();

                responseObject.IsSucces = true;
                responseObject.Data = responseDtoList;
            }
            else
            {
                responseObject.IsSucces = false;
                responseObject.ErrorMessages.Add("Unable to retrieve the User details");
            }
            return responseObject;

        }

        

        private async Task AddCompletedChallengeToUserChallenges(ApplicationUser user , CodeChallenge answeredChallenge)
        {
            var completed = await _CompletedCodeChallengeRepo.GetAllByUserIdAsync(user.Id);
            if(!completed.Any(c => c.ChallengeNumber == answeredChallenge.ChallengeNumber))
            {
                CompletedCodeChallenge challenge = new();
                challenge.ApplicationUserId = user.Id;
                challenge.ChallengeNumber = user.CurrentChallengeNumber;
                challenge.Solution = user.CurrentSolution;
                challenge.PuzzleInput = user.CurrentPuzzleInput ?? "no input";
                challenge.Description = answeredChallenge.Description;
                await _CompletedCodeChallengeRepo.AddAsync(challenge);
            }
        }

        private async Task UpdateUserAfterCorrectAnswer(ApplicationUser user)
        {
            _gameManager.UpdateUserAfterCorrectAnswer(user);
            await _userManager.UpdateAsync(user);
        }

       
        public async Task<BaseServiceResponse<UserStateResponseDto>> CheckIfUserHasFinishedAllChallenges(string userId)
        {
            BaseServiceResponse<UserStateResponseDto> responseObject = new();
            UserStateResponseDto responseDto = new();
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var completed = await _CompletedCodeChallengeRepo.GetAllByUserIdAsync(userId);
                var all = await _CodeChallengeRepo.ListAllAsync();
                var completedAll = completed.ToList().Count == all.ToList().Count ? responseDto.HasFinishedAll = true : responseDto.HasFinishedAll = false;
                responseObject.IsSucces = true;
                responseObject.Data = responseDto;
            }
            else
            {
                responseObject.IsSucces = false;
                responseObject.ErrorMessages.Add("Unable to retrieve the User details");
            }
            return responseObject;
        }

        //public async Task<BaseServiceResponse<PlayerStatsResponseDto>> GetUserGameStats(string userId)
        //{
        //    BaseServiceResponse<PlayerStatsResponseDto> responseObject = new();
        //    PlayerStatsResponseDto responseDto = new();
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        var statSheet = await _PlayerRepo.GetByUserIdAsync(userId);
        //        if(statSheet != null)
        //        {
        //            responseDto.PlayerName = user.UserName;
        //            responseDto.TotalGuesses = statSheet.TotalGuesses;
        //            responseDto.CorrectGuesses = statSheet.CorrectGuesses;
        //            responseDto.WrongGuesses = statSheet.WrongGuesses;
        //            responseDto.Level = statSheet.Level;

        //            responseObject.IsSucces = true;
        //            responseObject.Data = responseDto;

        //        }        
        //        else
        //        {
        //            responseObject.IsSucces = false;
        //            responseObject.ErrorMessages.Add("Unable to retrieve the User Stats");
        //        }
        //    }
        //    else
        //    {
        //        responseObject.IsSucces = false;
        //        responseObject.ErrorMessages.Add("Unable to retrieve the User details");
        //    }
        //    return responseObject;

        //}

        public async Task<BaseServiceResponse<PlayerStatsResponseDto>> GetUserGameStats(string userId)
        {          
            var stats = await _ValidatorService.GetPlayerStatsByUserId(userId);
            return stats;
        }

        private CodingChallengeResponseDto MapToCodingChallengeResponseDto(CodeChallenge challenge)
        {
            var dto = new CodingChallengeResponseDto();
            dto.Id = challenge.Id;
            dto.ChallengeNumber = challenge.ChallengeNumber;
            dto.Description = challenge.Description;
            return dto;
        }

        private UserCodingChallengeResponse MapToUserCodingChallengeResponseDto(CodeChallenge challenge , ApplicationUser user)
        {
            var dto = new UserCodingChallengeResponse();
            dto.Id = challenge.Id;
            dto.ChallengeNumber = challenge.ChallengeNumber;
            dto.Description = challenge.Description;
            dto.PuzzleInput = user.CurrentPuzzleInput;
            dto.ActualSolution = user.CurrentSolution.ToString()?? "no solution";
            return dto;
        }
    }
}
