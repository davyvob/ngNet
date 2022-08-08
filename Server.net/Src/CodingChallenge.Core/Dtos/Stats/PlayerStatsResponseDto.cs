using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Dtos.Stats
{
    public class PlayerStatsResponseDto 
    {
        public string PlayerName { get; set; }
        public int TotalGuesses { get; set; }
        public int CorrectGuesses { get; set; }
        public int WrongGuesses { get; set; }
        public int Level { get; set; }
        

    }
}
