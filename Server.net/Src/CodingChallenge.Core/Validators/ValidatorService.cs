using CodingChallenge.Core.Dtos.CodingChallenge.Completed;
using CodingChallenge.Core.Dtos.Stats;
using CodingChallenge.Core.Entities;
using CodingChallenge.Core.Infrastructure;
using CodingChallenge.Core.Services;
using CodingChallenge.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Validators
{
    public class ValidatorService : IValidatorService
    {
        private readonly IPlayerStatsRepository _PlayerRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserCodingChallengeRepository _CompletedCodeChallengeRepo;


        public ValidatorService(IPlayerStatsRepository PlayerRepo,
                                UserManager<ApplicationUser> userManager,
                                IUserCodingChallengeRepository CompletedCodeChallengeRepo)
        {
            _PlayerRepo = PlayerRepo;
            _userManager = userManager;
            _CompletedCodeChallengeRepo = CompletedCodeChallengeRepo;
        }


        public async Task<BaseServiceResponse<ApplicationUser>> GetUserById(string userId)
        {

            BaseServiceResponse<ApplicationUser> responseObject = new();
            var user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                responseObject.IsSucces = true;
                responseObject.Data = user;
            }
            else
                responseObject.ErrorMessages.Add("User not found");

            return responseObject;
        }

        public async Task<BaseServiceResponse<PlayerStatsResponseDto>> GetPlayerStatsByUserId(string userId)
        {
            var responseObject = new BaseServiceResponse<PlayerStatsResponseDto>();
            var dto = new PlayerStatsResponseDto();

            var user = await GetUserById(userId);
            if (user.IsSucces)
            {
                var statSheet = await _PlayerRepo.GetByUserIdAsync(userId);
                if (statSheet is not null)
                {
                    responseObject.IsSucces = true;
                    dto.TotalGuesses = statSheet.TotalGuesses;
                    dto.WrongGuesses = statSheet.WrongGuesses;
                    dto.CorrectGuesses = statSheet.CorrectGuesses;
                    dto.Level = statSheet.Level;
                    dto.PlayerName = user.Data.UserName;
                    responseObject.Data = dto;
                }
                else
                    responseObject.ErrorMessages.Add("Playerstats are not found");
            }
            else
                responseObject.ErrorMessages = user.ErrorMessages;
        
            return responseObject;
        }

      

        public async Task<BaseServiceResponse<CompletedUserCodeChallengeListDto>> GetCompletedChallengesForUser(string userId)
        {
            var responseObject = new BaseServiceResponse<CompletedUserCodeChallengeListDto>();
            var dto = new CompletedUserCodeChallengeListDto();
            var user = await GetUserById(userId);
            if (user.IsSucces)
            {
                var allChallenges = await _CompletedCodeChallengeRepo.GetAllByUserIdAsync(userId);
                if (allChallenges is not null)
                {
                    dto.CompletedChallenges = allChallenges.Select(c => new CompletedUserCodeChallengeDto
                    {
                        PuzzleInput = c.PuzzleInput,
                        ChallengeNumber = c.ChallengeNumber,
                        Description = c.Description,
                        Solution = c.Solution,
                    }).ToList();
                    responseObject.IsSucces = true;
                    responseObject.Data = dto;
                }
                else
                    responseObject.ErrorMessages.Add("Unable to retrieve completed challenges for user");                
            }
            else
                responseObject.ErrorMessages = user.ErrorMessages;

            return responseObject;

        }
    }
}
