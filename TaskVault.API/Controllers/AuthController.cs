using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskVault.API.Dtos.AuthDtos;
using TaskVault.API.Repositories.Interfaces;

namespace TaskVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await  _repo.RegisterAsync(dto);

            if(!result.Success)
            {
                return BadRequest(result);
            }

           return Ok(result);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result  = await _repo.LoginAsync(dto);
            if(!result.Success)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
    }
}
