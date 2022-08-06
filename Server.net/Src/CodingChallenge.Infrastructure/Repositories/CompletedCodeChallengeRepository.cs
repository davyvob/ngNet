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
    public class CompletedCodeChallengeRepository  : BaseRepository<CompletedCodeChallenge> , IUserCodingChallengeRepository
    {
        public CompletedCodeChallengeRepository(CodingChallengeDbContext context) : base(context)
        {

        }

        public override IQueryable<CompletedCodeChallenge> GetAll()
        {
            return _dbContext.CompletedChallenges.Include(ch => ch.ApplicationUser);
        }

        public async override Task<IEnumerable<CompletedCodeChallenge>> ListAllAsync()
        {
            var challenges = await GetAll().ToListAsync();
            return challenges;
        }

        public async override Task<CompletedCodeChallenge> GetByIdAsync(Guid id)
        {
            var challenge = await GetAll().SingleOrDefaultAsync(sk => sk.Id.Equals(id));
            return challenge;
        }

        public async Task<IEnumerable<CompletedCodeChallenge>> GetAllByUserIdAsync(string id)
        {
            var challenges = await GetAll().Where(c => c.ApplicationUserId.Equals(id)).ToListAsync();
            return challenges;
        }
    }
}
