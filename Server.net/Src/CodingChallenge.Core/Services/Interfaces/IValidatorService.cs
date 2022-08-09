using CodingChallenge.Core.Dtos.Stats;
using CodingChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Services.Interfaces
{
    public interface IValidatorService
    {
        Task<BaseServiceResponse<ApplicationUser>> GetUserById(string userId);
        Task<BaseServiceResponse<PlayerStatsResponseDto>> GetPlayerStatsByUserId(string userId);
    }
}
