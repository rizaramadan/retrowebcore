using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using retrowebcore.Handlers.Boards;
using retrowebcore.Models;
using retrowebcore.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace retrowebcore.Handlers.Boards.Tests
{
    [TestFixture()]
    public class ArchiveBoardHandlerTests
    {
        [Test()]
        public async Task HandleTest()
        {
            var options = TestDbContext.NewDefaultOption();

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
