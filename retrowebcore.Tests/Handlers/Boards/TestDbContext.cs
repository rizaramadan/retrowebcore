using Microsoft.EntityFrameworkCore;
using retrowebcore.Models;
using retrowebcore.Persistences;

namespace retrowebcore.Handlers.Boards.Tests
{
    public class TestDbContext : AppDbContext
    {
        public TestDbContext(DbContextOptions<AppDbContext> o, long user) : base(o) =>
            ScopedUserId = user;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Card>()
                .Ignore(x => x.LikerId)
                .Ignore(x => x.RelatedCardId)
                .Ignore(x => x.comments);
        }

        public static DbContextOptions<AppDbContext> NewDefaultOption() 
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(ArchiveBoardHandlerTests))
                .Options;
        }
    }
}
