using WebApi.DTOs.Question;

namespace WebApi.DTOs.Quiz
{
    public class QuizGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<QuestionGetDto>? Questions { get; set; }
    }
}
