using CodingChallenge.Core.Dtos.Users;
using CodingChallenge.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CodingChallenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDto registration)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(JsonSerializer.Serialize(allErrors));
            }

            var response = await _accountService.Register(registration);
            if (response.IsSucces) return Ok(response.Data);
            else return BadRequest(JsonSerializer.Serialize(response.ErrorMessages));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto login)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(JsonSerializer.Serialize(allErrors));
            }

            var response = await _accountService.Login(login);
            if (response.IsSucces) return Ok(response.Data);
            else return BadRequest(JsonSerializer.Serialize(response.ErrorMessages));
        }


    }
}
