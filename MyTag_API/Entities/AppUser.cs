using Microsoft.AspNetCore.Identity;

namespace MyTag_API.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? JobTitle { get; set; }
        public string? Company { get; set; }
        public List<Instagram>? Instagrams { get; set; }
        public List<WhatsApp>? WhatsApps { get; set; }
        public List<Facebook>? Facebooks { get; set; }
        public List<LinkedIn>? LinkedIns { get; set; }
        public List<Snapchat>? Snapchats { get; set; }
        public List<AppleMusic>? AppleMusics { get; set; }
        public List<Spotify>? Spotifies { get; set; }
        public List<Telegram>? Telegrams { get; set; }
        public List<Threads>? Threads { get; set; }
        public List<TikTok>? TikToks { get; set; }
        public List<Twitch>? Twitches { get; set; }
        public List<Twitter>? Twitters { get; set; }
        public List<WebSite>? WebSites { get; set; }
        public List<YouTube>? YouTubes { get; set; }
    }
}
