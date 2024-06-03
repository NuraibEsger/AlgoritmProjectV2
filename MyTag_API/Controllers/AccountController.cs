using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTag_API.DTOs.Account;
using MyTag_API.Entities;
using MyTag_API.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyTag_API.Controllers
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, [FromServices] IJwtTokenService tokenService)
        {
            if(!ModelState.IsValid)
            {
                var message =  ModelState
                   .SelectMany(modelState => modelState.Value!.Errors)
                   .Select(err => err.ErrorMessage)
                   .ToList();

                return BadRequest(message);
            }

            var user = await _userManager.FindByEmailAsync(dto.Email!);
            if (user is null) return NotFound("User not found");

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, dto.Password!);
            if (!isPasswordCorrect) return BadRequest("Password is not correct");

            if (dto.Password != dto.ConfirmPassword)
            {
                ModelState.AddModelError(nameof(LoginDto.ConfirmPassword), "Confirm password doesn't match. Type again!");

                var validationErrors = ModelState
                .SelectMany(modelState => modelState.Value!.Errors)
                .Select(err => err.ErrorMessage)
                .ToList();

                return BadRequest(validationErrors);
            }

            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            var token = tokenService.GenerateToken(user.Name!, user.UserName!, roles, user.Id);

            if (token is null) return NotFound();

            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto, 
            [FromServices] ISendEmailService sendEmail, 
            [FromServices] IUserRegistrationService socailMediaService)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.Users.Any(x => x.Email == dto.Email)) return BadRequest("User alredy exists");

                var user = new AppUser
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    UserName = dto.Email,
                };

                if (dto.Instagrams is not null && dto.Instagrams.Any())
                {
                    var socialMedia = socailMediaService.MapAndAssignSocialMedia(user, dto.Instagrams);
                }

                if (dto.Password != dto.ConfirmPassword)
                {
                    ModelState.AddModelError(nameof(RegisterDto.ConfirmPassword), "Confirm password doesn't match. Type again!");

                    var validationErrors = ModelState
                    .SelectMany(modelState => modelState.Value!.Errors)
                    .Select(err => err.ErrorMessage)
                    .ToList();

                    return BadRequest(validationErrors);
                }

                var result = await _userManager.CreateAsync(user, dto.Password!);

                if (!result.Succeeded) return BadRequest();

                await _userManager.AddToRoleAsync(user, "User");

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = $"http://localhost:5173/EmailConfirmation?token={token}&email={dto.Email}";

                bool IsSendEmail = await sendEmail.EmailSend(dto.Email!, confirmationLink!);

                if (IsSendEmail)
                    return Ok("User registered. Confirmation email sent.");
                else
                    return BadRequest("Failed to send confirmation email");

            }
            return BadRequest();
        }
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] EmailConfirmationDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!);

            if (user == null)
                return BadRequest("Email not found");

            if (user.EmailConfirmed == true) return BadRequest();

            var result = await _userManager.ConfirmEmailAsync(user, request.Token!);

            if (result.Succeeded)
                return Ok("Email confirmed successfully");
            else
                return BadRequest(request.Token);
        }
    }
}
