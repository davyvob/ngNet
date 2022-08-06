using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Dtos.Answers
{
    public class AnswerRequestDto
    {   
        [Required]
        public string Answer { get; set; }
   

    }
}
