using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Entities
{
    public class PlayerStats :BaseClass
    {
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int TotalGuesses { get; set; }
        public int CorrectGuesses { get; set; }
        public int WrongGuesses { get; set; }
        public int Level { get; set; }

        public PlayerStats(string applicationUserId)
        {   
            ApplicationUserId = applicationUserId;
            Id = Guid.NewGuid();
            TotalGuesses = 0;
            CorrectGuesses = 0;
            WrongGuesses = 0;
            Level = 1;
        }


    }
}
