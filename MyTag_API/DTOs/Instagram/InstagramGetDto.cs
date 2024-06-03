using MyTag_API.Entities;

namespace MyTag_API.DTOs.Instagram
{
    public class InstagramGetDto
    {
        public int Id { get; set; }
        public string? Link { get; set; }
        public string? Title { get; set; }
        public string? AppUserId { get; set; }
    }
}
