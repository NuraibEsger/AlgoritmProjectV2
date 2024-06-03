using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTag_API.Entities;

namespace MyTag_API.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Instagram> Instagrams { get; set; }
        public DbSet<AppleMusic> AppleMusics { get; set; }
        public DbSet<Facebook> Facebooks { get; set; }
        public DbSet<LinkedIn> LinkedIns { get; set; }
        public DbSet<Snapchat>  Snapchats { get; set; }
        public DbSet<Spotify> Spotifies { get; set; }
        public DbSet<Telegram> Telegrams { get; set; }
        public DbSet<Threads> Threads { get; set; }
        public DbSet<TikTok> TikToks { get; set; }
        public DbSet<Twitch> Twitches { get; set; }
        public DbSet<Twitter> Twitters { get; set; }
        public DbSet<WebSite> WebSites { get; set; }
        public DbSet<WhatsApp> WhatsApps { get; set; }
        public DbSet<YouTube> YouTubes { get; set; }
    }
}
