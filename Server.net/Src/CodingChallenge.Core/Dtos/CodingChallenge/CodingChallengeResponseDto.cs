using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Dtos.CodingChallenge
{
    public class CodingChallengeResponseDto : BaseDto
    {
        public int ChallengeNumber { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
