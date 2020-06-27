using MediatR;
using Microsoft.AspNetCore.SignalR;
using retrowebcore.Handlers.Mediators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            await Clients.All.SendAsync("hubNewBoardEvent", squad, board.Name);
        }
    }
}
