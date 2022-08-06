using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Entities
{

    public class CodeChallenge : BaseClass
    {
        [Required]
        public int ChallengeNumber { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
    }

}
