using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Dtos.CodingChallenge
{
    public class UserCodingChallengeResponse : BaseDto
    {
        public int ChallengeNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? PuzzleInput { get; set; }
        public string  ActualSolution { get; set; }


    }
}
