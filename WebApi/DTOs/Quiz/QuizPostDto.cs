using System.ComponentModel.DataAnnotations;
using WebApi.DTOs.Option;
using WebApi.DTOs.Question;
using WebApi.Entities;

namespace WebApi.DTOs.Quiz
{
    public class QuizPostDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public List<QuestionPostDto>? Questions { get; set; }
    }
}
