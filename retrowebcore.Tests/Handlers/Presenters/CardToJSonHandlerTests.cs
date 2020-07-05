using NUnit.Framework;
using retrowebcore.Handlers.Presenters;
using retrowebcore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace retrowebcore.Handlers.Presenters.Tests
{
    [TestFixture()]
    public class CardToJSonHandlerTests
    {
        [Test()]
        public async Task HandleTest()
        {
            var handler = new CardToJSonHandler();
            var card = new Card();
            var json = await handler.Handle(new CardToJsonRequest(card), CancellationToken.None);
            Assert.NotNull(json);
        }
    }
}