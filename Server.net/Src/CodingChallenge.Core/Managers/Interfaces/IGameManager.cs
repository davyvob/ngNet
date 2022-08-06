using CodingChallenge.Core.Dtos.Answers;
using CodingChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Managers.Interfaces
{
    public interface IGameManager
    {
        void ConfigureUserForChallenges(ApplicationUser user);
        Task<bool> VerifyCodingChallengeAnswer(AnswerRequestDto answer, ApplicationUser user);
        void UpdateUserAfterCorrectAnswer(ApplicationUser user);
        Task<AnswerResponseDto> CheckIfHigherAndClose(ApplicationUser user, AnswerRequestDto answer, AnswerResponseDto response);
    }
}
