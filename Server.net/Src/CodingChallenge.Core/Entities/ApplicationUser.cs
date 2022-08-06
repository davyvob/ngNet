
using CodingChallenge.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {

       
        public string? CurrentPuzzleInput { get; set; } 
        public long CurrentSolution { get; set; }
        public int CurrentChallengeNumber { get; set; } = 1;

        public ICollection<CompletedCodeChallenge> CompletedChallenges { get; set; }


        


    }
}
