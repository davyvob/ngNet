using CodingChallenge.Core.Dtos.Users;
using CodingChallenge.Core.Entities;
using CodingChallenge.Core.Managers;
using CodingChallenge.Core.Managers.Interfaces;
using CodingChallenge.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Services
{
    public class AccountService :IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IGameManager _gameManager;

        public AccountService(UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        IConfiguration configuration,
                                        IGameManager gameManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _gameManager = gameManager;

        }

        public async Task<BaseServiceResponse<UserResponseDto>> Register(UserRequestDto registration)
        {
            BaseServiceResponse<UserResponseDto> response = new();
            if (await _userManager.FindByEmailAsync(registration.Email) is not null)
            {
                response.IsSucces = false;
                response.ErrorMessages.Add("Allready an account with this email");
                return response;
            }
            if (await _userManager.FindByNameAsync(registration.UserName) is not null)
            {
                response.IsSucces = false;
                response.ErrorMessages.Add("Allready an account with this username");
                return response;
            }

            ApplicationUser newUser = new ApplicationUser
            {
                Email = registration.Email,
                UserName = registration.UserName,
            };

            _gameManager.ConfigureUserForChallenges(newUser);

            IdentityResult result = await _userManager.CreateAsync(newUser, registration.Password);
            if (!result.Succeeded)
            {
                response.IsSucces = false;
                result.Errors.ToList().ForEach(error => response.ErrorMessages.Add(error.Description));
                return response;

            }

            newUser = await _userManager.FindByEmailAsync(registration.Email);
            await AddCustumClaims(newUser);
            await _userManager.AddToRoleAsync(newUser, "User");
            response.IsSucces = true;
            response.Data = MapById(newUser);
            return response;

        }

        public async Task<BaseServiceResponse<LoginResponseDto>> Login(LoginRequestDto login)
        {
            BaseServiceResponse<LoginResponseDto> response = new();
          
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                response.IsSucces = false;
                response.ErrorMessages.Add( "Login failed, please check your Credentials");
                return response;
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.IsSucces = false;
                response.ErrorMessages.Add("Login failed, please check your Credentials");
                return response;
            }
            var applicationUser = await _userManager.FindByNameAsync(user.UserName);

            var claims = await _userManager.GetClaimsAsync(user);
            if (claims.Count == 0)
            {
                await AddCustumClaims(applicationUser);
            }
            JwtSecurityToken token = await GenerateTokenAsync(applicationUser);
            string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
            response.IsSucces = true;
            response.Data = new LoginResponseDto()
            {
                Token = serializedToken,
                Email = applicationUser.Email,
                UserName = applicationUser.UserName,
                Id = applicationUser.Id,
            };
         
            return response;

        }

        


        private async Task<JwtSecurityToken> GenerateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>();
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            var roleClaims = await _userManager.GetRolesAsync(user);
            foreach (var roleClaim in roleClaims)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleClaim));
            }

            var expirationDays = _configuration["JWT:TokenExpirationDays"];
            var siginingKey = Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]);
            var token = new JwtSecurityToken
            (
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(double.Parse(expirationDays))),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(siginingKey),
                          SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        private async Task AddCustumClaims(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim("id", user.Id.ToString()));
            claims.Add(new Claim("username", user.UserName));
            await _userManager.AddClaimsAsync(user, claims);
        }

        private UserResponseDto MapById(ApplicationUser newUser)
        {
            UserResponseDto userDto = new UserResponseDto
            {
                SuccesMessage = "Registration succesfull you can now log in",
                Id = newUser.Id,
                UserName = newUser.UserName,
                Email = newUser.Email,
            };
            return userDto;
        }



    }
}
