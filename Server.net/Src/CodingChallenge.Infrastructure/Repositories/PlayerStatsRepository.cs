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
    public class PlayerStatsRepository : BaseRepository<PlayerStats>, IPlayerStatsRepository
    {
        public PlayerStatsRepository(CodingChallengeDbContext context) : base(context)
        {
        }
            
        public async Task<PlayerStats> GetByUserIdAsync(string id)
        {
            var statsSheet = await _dbContext.Set<PlayerStats>().SingleOrDefaultAsync(t => t.ApplicationUserId.Equals(id));
            return statsSheet;
        }
    }
}
