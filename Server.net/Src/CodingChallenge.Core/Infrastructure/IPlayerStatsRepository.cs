using CodingChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Infrastructure
{
    public interface IPlayerStatsRepository: IBaseRepository<PlayerStats>
    {
        Task<PlayerStats> GetByUserIdAsync(string id);
    }
}
