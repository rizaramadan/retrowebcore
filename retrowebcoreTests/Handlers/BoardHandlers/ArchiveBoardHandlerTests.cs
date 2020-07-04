using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using retrowebcore.Handlers.Mediators;
using retrowebcore.Models;
using retrowebcore.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace retrowebcore.Handlers.Mediators.Tests
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
    }

    [TestFixture()]
    public class ArchiveBoardHandlerTests
    {
        [Test()]
        public async Task HandleTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(ArchiveBoardHandlerTests))
                .Options;

            using var context = new TestDbContext(options, long.MaxValue);

            var handler = new ArchiveBoardHandler(context);
            var ct = CancellationToken.None;
            var slug = Guid.NewGuid();

            context.Boards.Add(new Board { Slug = slug });
            await context.SaveChangesAsync();

            var output = await handler.Handle(new ArchiveBoardRequest { Slug = slug }, ct);

            var archived = context.Boards.FirstOrDefault(x => x.Slug == slug);
            Assert.IsNotNull(archived);
            Assert.That(archived.Slug, Is.EqualTo(slug));
            Assert.IsNotNull(archived?.DeletedAt);
            Assert.IsNotNull(archived?.Deletor);
            Assert.AreEqual(archived.Deletor, long.MaxValue);
        }
    }
}
