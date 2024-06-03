using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Account;
using WebApi.Entities;
using WebApi.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        // GET: api/<AccountController>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, [FromServices] IJwtTokenService tokenService)
        {
            if (!ModelState.IsValid)
            {
                var messages = ModelState
                  .SelectMany(modelState => modelState.Value!.Errors)
                  .Select(err => err.ErrorMessage)
                  .ToList();

                return BadRequest(messages);
            }

            var user = await _userManager.FindByNameAsync(dto.UserName!);
            if(user is null) return NotFound();

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, dto.Password!);
            if (!isPasswordCorrect) return NotFound();

            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            var token = tokenService.GenerateToken(user.FirstName!, user.LastName!, user.UserName!, roles );

            if (token is null) return NotFound();

            return Ok(token);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new AppUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.UserName,
            };

            var result = await _userManager.CreateAsync(user, dto.Password!);

            if (!result.Succeeded) return BadRequest();

            return Ok();
        }
    }
}
