using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Dtos.CodingChallenge.Completed
{
    public class CompletedUserCodeChallengeListDto
    {
        public int NumberOfCompletedChallenges => CompletedChallenges.Count;
        public List<CompletedUserCodeChallengeDto> CompletedChallenges { get; set; }
    }
}
