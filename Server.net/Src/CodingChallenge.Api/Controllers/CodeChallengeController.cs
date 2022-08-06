using CodingChallenge.Core.Dtos.Answers;
using CodingChallenge.Core.Infrastructure;
using CodingChallenge.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CodingChallenge.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CodeChallengeController : ControllerBase
    {
        private readonly ICodingChallengeService _challengeService;
        public CodeChallengeController(ICodingChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _challengeService.GetAll();
            if (response.IsSucces) return Ok(response.Data);
            else return BadRequest(JsonSerializer.Serialize(response.ErrorMessages));
        }


        [HttpGet("GetCurrent")]
        public async Task<IActionResult> GetCurrentForUser()
        {
            var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault()?.Value;
            var response = await _challengeService.ShowCurrent(userId);
            if (response.IsSucces) return Ok(response.Data);
            else return BadRequest(JsonSerializer.Serialize(response.ErrorMessages));
        }

        [HttpPost("AnswerCurrent")]
        public async Task<IActionResult> AnswerCurrentForUser(AnswerRequestDto answer)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(JsonSerializer.Serialize(allErrors));
            }

            var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault()?.Value;
            var response = await _challengeService.AnswerCurrent(userId , answer);
            if (response.IsSucces) return Ok(response.Data);
            else return BadRequest(JsonSerializer.Serialize(response.ErrorMessages));
        }

        [HttpGet("GetCompletedChallenges")]
        public async Task<IActionResult> GetCompletedChallenges()
        {
            var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault()?.Value;
            var response = await _challengeService.GetCompletedChallengesForUser(userId);
            if (response.IsSucces) return Ok(response.Data);
            else return BadRequest(JsonSerializer.Serialize(response.ErrorMessages));
        }

        [HttpGet("UserGameStatus")]
        public async Task<IActionResult> GetUserGameStatus()
        {
            var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault()?.Value;
            var response = await _challengeService.CheckIfUserHasFinishedAllChallenges(userId);
            if (response.IsSucces) return Ok(response.Data);
            else return BadRequest(JsonSerializer.Serialize(response.ErrorMessages));
        }
    }
}
