using CodingChallenge.Core.Entities;
using CodingChallenge.Core.Infrastructure;
using CodingChallenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Infrastructure.Repositories
{
    public class CodeChallengeRepository : BaseRepository<CodeChallenge> , ICodeChallengeRepository
    {
        public CodeChallengeRepository(CodingChallengeDbContext context) :base(context)
        {
     
        }

        public async Task<CodeChallenge> GetByNumber(int challengeNumber)
        {
            try
            {
                var challenge = await _dbContext.Set<CodeChallenge>().SingleOrDefaultAsync(t => t.ChallengeNumber.Equals(challengeNumber));
                if (challenge == null)
                    return null;
                return challenge;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

       
    }
}
