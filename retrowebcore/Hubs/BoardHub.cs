using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using retrowebcore.Handlers.Boards;
using System;
using System.Threading.Tasks;

namespace retrowebcore.Hubs
{
    public class BoardHub : Hub
    {
        public const string hubAddNewBoard = nameof(hubAddNewBoard);
        public const string hubNewBoardEvent = nameof(hubNewBoardEvent);
        
        public const string hubArchiveBoard = nameof(hubArchiveBoard);
        public const string hubArchiveEvent = nameof(hubArchiveEvent);

        readonly IMediator _mediator;
        public BoardHub(IMediator m) => _mediator = m;


        [HubMethodName(hubAddNewBoard)]
        public async Task addNewBoard(string squad, string sprint)
        {
            var board = await _mediator.Send(new CreateBoardRequest{ Squad = squad, Sprint = sprint });
            var json = JsonConvert.SerializeObject(new { squad = board.Name, sprint = board.Description, slug = board.Slug });
            await Clients.All.SendAsync(hubNewBoardEvent, json);
        }

        [HubMethodName(hubArchiveBoard)]
        public async Task archiveBoard(string slugStr)
        {
            if (!Guid.TryParse(slugStr, out var slug))
                return;

            await _mediator.Send(new ArchiveBoardRequest { Slug = slug });
            await Clients.All.SendAsync(hubArchiveEvent, slug);
        }
    }
}
