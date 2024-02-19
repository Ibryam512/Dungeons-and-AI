using DungeonsAndArtificialIntelligenceAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DungeonsAndArtificialIntelligenceAPI.Data
{
    public class DandAIDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DandAIDbContext(DbContextOptions<DandAIDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasMany(user => user.Stories)
                .WithOne(story => story.User)
                .HasForeignKey(story => story.UserId);

            modelBuilder.Entity<Story>()
                .HasMany(story => story.Messages)
                .WithOne(message => message.Story)
                .HasForeignKey(message => message.StoryId);

            modelBuilder.Entity<UserToken>()
                .HasOne(userToken => userToken.User)
                .WithMany(user => user.Tokens)
                .HasForeignKey(userToken => userToken.UserId);
        }
    }
}
