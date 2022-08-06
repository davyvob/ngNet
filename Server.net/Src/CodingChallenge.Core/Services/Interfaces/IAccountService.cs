using CodingChallenge.Core.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseServiceResponse<LoginResponseDto>> Login(LoginRequestDto login);
        Task<BaseServiceResponse<UserResponseDto>> Register(UserRequestDto registration);
    }
}
