using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Dtos.CodingChallenge.Completed
{
    public class CompletedUserCodeChallengeDto
    {        
        public int ChallengeNumber { get; set; }      
        public string? Description { get; set; }
        public string? PuzzleInput { get; set; }
        public double Solution { get; set; }
    }
}
