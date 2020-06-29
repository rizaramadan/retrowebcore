using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using retrowebcore.Handlers.Mediators;
using retrowebcore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace retrowebcore.Hubs
{
    public class CardHub : Hub
    {
        readonly IMediator _mediator;
        public CardHub(IMediator m) => _mediator = m;

        [HubMethodName("hubAddNewCard")]
        public async Task addNewCard(string board, CardType type)
        {
            var card = await _mediator.Send(new CreateNewCard { BoardSlug = board, Type = type});
            var json = JsonConvert.SerializeObject(card);
            if(type == CardType.Liked)
                await Clients.All.SendAsync($"hubNewCard{nameof(CardType.Liked)}Event", json);
        }
    }
}
