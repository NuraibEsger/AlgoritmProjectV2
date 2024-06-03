using System.ComponentModel.DataAnnotations;

namespace MyTag_API.DTOs.Account
{
    public class EmailConfirmationDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Token { get; set; }
    }
}
