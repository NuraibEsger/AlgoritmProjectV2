using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Account
{
    public class LoginDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        public class LoginDtoValidator : AbstractValidator<LoginDto>
        {
            public LoginDtoValidator()
            {
                RuleFor(x => x.UserName)
                    .MinimumLength(5).WithMessage("Minimum 5 chars required!")
                    .NotNull().WithMessage("Required!")
                    .MaximumLength(255).WithMessage("Maximum 255 chars required!");
            }
        }
    }
}
