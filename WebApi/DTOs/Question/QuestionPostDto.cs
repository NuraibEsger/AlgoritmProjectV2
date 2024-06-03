using System.ComponentModel.DataAnnotations;
using WebApi.DTOs.Option;
using WebApi.Entities;

namespace WebApi.DTOs.Question
{
    public class QuestionPostDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal Points { get; set; }
        [Required]
        public List<OptionPostDto>? Options { get; set; }
    }
}
