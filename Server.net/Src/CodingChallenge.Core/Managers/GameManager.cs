using CodingChallenge.Core.Dtos.Answers;
using CodingChallenge.Core.Entities;
using CodingChallenge.Core.Managers.Interfaces;
using CodingChallenge.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Managers
{
    public class GameManager :IGameManager
    {
        private ChallengeAlgoRepository _algos = new();

        private string GetRandomInput()
        {
            string input = "";
            for (int i = 0; i < 100; i++)
            {
                var random = new Random();
                int next = random.Next(1, 500);
                var last = i < 99 ? input += $"{next} " : input += $"{next}";
            }
            return input;
        }

        public void ConfigureUserForChallenges(ApplicationUser user)
        {
            // playerstats
            user.PlayerStats = new PlayerStats(user.Id);       
            //
            user.CurrentPuzzleInput = GetRandomInput();
            var algo = _algos.ChallengeAnswersById.FirstOrDefault(x => x.Key == user.CurrentChallengeNumber).Value;
            user.CurrentSolution = algo(user.CurrentPuzzleInput);
        }

        public async Task<bool> VerifyCodingChallengeAnswer(AnswerRequestDto answer, ApplicationUser user)
        {
            if (answer.Answer == user.CurrentSolution.ToString())
            {
                return true;
            }
                
            return false;
        }

        public async Task<AnswerResponseDto> CheckIfHigherAndClose(ApplicationUser user, AnswerRequestDto answer, AnswerResponseDto response)
        {
            response.IsWithinTenDigitsOfBeingRight = long.Parse(answer.Answer) - user.CurrentSolution <= 10 && long.Parse(answer.Answer) - user.CurrentSolution > 0 || long.Parse(answer.Answer) - user.CurrentSolution >= -10 && long.Parse(answer.Answer) - user.CurrentSolution < 0;
            response.CorrrectAsnwerIsHigher = user.CurrentSolution > long.Parse(answer.Answer);
            return response;
        }

        public void UpdateUserAfterCorrectAnswer(ApplicationUser user)
        {   
            if(user.CurrentChallengeNumber < _algos.ChallengeAnswersById.Count)
            {
                user.CurrentChallengeNumber++;
                user.CurrentPuzzleInput = GetRandomInput();
                var algo = _algos.ChallengeAnswersById.FirstOrDefault(x => x.Key == user.CurrentChallengeNumber).Value;
                user.CurrentSolution = algo(user.CurrentPuzzleInput);
            }
           
        }







    }
}
