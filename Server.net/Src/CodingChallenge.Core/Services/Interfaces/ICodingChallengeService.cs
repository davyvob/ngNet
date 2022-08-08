using CodingChallenge.Core.Dtos.Answers;
using CodingChallenge.Core.Dtos.CodingChallenge;
using CodingChallenge.Core.Dtos.CodingChallenge.Completed;
using CodingChallenge.Core.Dtos.Stats;
using CodingChallenge.Core.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Services.Interfaces
{
    public interface ICodingChallengeService
    {
        Task<BaseServiceResponse<IEnumerable<CodingChallengeResponseDto>>> GetAll();
        Task<BaseServiceResponse<UserCodingChallengeResponse>> ShowCurrent(string userId);
        Task<BaseServiceResponse<AnswerResponseDto>> AnswerCurrent(string userId, AnswerRequestDto answer);
        Task<BaseServiceResponse<CompletedUserCodeChallengeListDto>> GetCompletedChallengesForUser(string userId);
        Task<BaseServiceResponse<UserStateResponseDto>> CheckIfUserHasFinishedAllChallenges(string userId);
        Task<BaseServiceResponse<PlayerStatsResponseDto>> GetUserGameStats(string userId);
    }
}
