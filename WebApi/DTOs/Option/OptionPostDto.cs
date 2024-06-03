using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Option
{
    public class OptionPostDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
    }
}
