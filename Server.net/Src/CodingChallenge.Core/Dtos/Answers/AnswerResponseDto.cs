using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Dtos.Answers
{
    public class AnswerResponseDto
    {
        public bool IsCorrect { get; set; }
        public bool CorrrectAsnwerIsHigher { get; set; }
        public bool IsWithinTenDigitsOfBeingRight { get; set; }

    }
}
