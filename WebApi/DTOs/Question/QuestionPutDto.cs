using System.ComponentModel.DataAnnotations;
using WebApi.DTOs.Option;

namespace WebApi.DTOs.Question
{
    public class QuestionPutDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal Points { get; set; }
        [Required]
        public List<OptionPostDto>? Options { get; set; }
    }
}
