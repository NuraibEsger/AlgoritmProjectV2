namespace MyTag_API.Entities
{
    public class Telegram
    {
        public int Id { get; set; }
        public string? Link { get; set; }
        public string? Title { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
