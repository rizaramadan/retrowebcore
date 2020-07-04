using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using retrowebcore.Handlers.Boards;
using System.Threading.Tasks;

namespace retrowebcore.Hubs
{
    public class BoardHub : Hub
    {
        readonly IMediator _mediator;
        public BoardHub(IMediator m) => _mediator = m;


        [HubMethodName("hubAddNewBoard")]
        public async Task addNewBoard(string squad, string sprint)
        {
            var board = await _mediator.Send(new CreateBoardRequest{ Squad = squad, Sprint = sprint });
            var json = JsonConvert.SerializeObject(new { squad = board.Name, sprint = board.Description, slug = board.Slug });
            await Clients.All.SendAsync("hubNewBoardEvent", json);
        }
    }
}
