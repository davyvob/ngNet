using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Repositories
{
    public class ChallengeAlgoRepository
    {
        //dic
        public Dictionary<int, Func<string, long>> ChallengeAnswersById { get; private set; }
       
        public ChallengeAlgoRepository()
        {
            ChallengeAnswersById = new Dictionary<int, Func<string, long>>(); 
            ChallengeAnswersById.Add(1, FirstChallenge);
            ChallengeAnswersById.Add(2, SecondChallenge);
            ChallengeAnswersById.Add(3, ThirdChallenge);
        }


        
        //first
        private long FirstChallengeAlgo(string input) => input.Split(" ").ToList().Sum(x => int.Parse(x));
        private Func<string, long> FirstChallenge => FirstChallengeAlgo;

        //second
        private long SecondChallengeAlgo(string input) => input.Split(" ").ToList().Sum(x => int.Parse(x)) * input.Split(" ").Length;
        private Func<string, long> SecondChallenge => SecondChallengeAlgo;

        //third
        private long ThirdChallengeAlgo(string input) => input.Split(" ").ToList().Min(x => int.Parse(x)) * input.Split(" ").ToList().Max(x => int.Parse(x));
        private Func<string, long> ThirdChallenge => ThirdChallengeAlgo;



    }
}
