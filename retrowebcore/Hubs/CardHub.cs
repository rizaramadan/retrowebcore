using MediatR;
using Microsoft.AspNetCore.SignalR;
using retrowebcore.Handlers.Boards;
using retrowebcore.Handlers.Presenters;
using System.Threading.Tasks;

namespace retrowebcore.Hubs
{
    public class CardHub : Hub
    {
        readonly IMediator _mediator;
        public CardHub(IMediator m) => _mediator = m;

        [HubMethodName("hubAddNewCard")]
        public async Task addNewCard(string board, string type)
        {
            var card = await _mediator.Send(new CreateNewCard { BoardSlug = board, TypeStr = type});
            var json = await _mediator.Send(new CardToJsonRequest(card));
            await Clients.All.SendAsync("hubNewCardEvent", json);
        }
    }
}
