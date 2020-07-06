using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using retrowebcore.Handlers.Boards;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace retrowebcore.Handlers.Boards.Tests
{
    [TestFixture()]
    public class CreateBoardHandlerTests
    {
        [Test()]
        public async Task HandleTest()
        {
            var options = TestDbContext.NewDefaultOption();
            using var context = new TestDbContext(options, long.MaxValue);

            var handler = new CreateBoardHandler(context);

            var result = await handler.Handle(new CreateBoardRequest { Sprint = "14", Squad = "Games" }, CancellationToken.None);
            Assert.AreEqual(await context.Boards.CountAsync(x => x.Name == "Games" && x.Description == "14"),1);
        }
    }
}