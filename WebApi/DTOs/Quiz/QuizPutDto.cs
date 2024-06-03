using System.ComponentModel.DataAnnotations;
using WebApi.DTOs.Question;

namespace WebApi.DTOs.Quiz
{
    public class QuizPutDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public DateTime CreatingDate { get; set; }
        [Required]
        public List<QuestionPostDto>? Questions { get; set; }
    }
}
