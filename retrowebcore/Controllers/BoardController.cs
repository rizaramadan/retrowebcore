using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using retrowebcore.Handlers.Mediators;
using retrowebcore.Models;

namespace retrowebcore.Controllers
{
    [Authorize]
    public class BoardController : BaseController<BoardController>
    {
        const string BoardList = nameof(BoardList);
        const string BoardView = nameof(BoardView);

        public BoardController(ILogger<BoardController> l, IMediator m) : base(l,m) { }

        public async Task<IActionResult> List() =>
            View(BoardList, await _mediator.Send(new BoardListRequest()));

        public async Task<IActionResult> Archive(Guid id) 
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _mediator.Send(new ArchiveBoardRequest{ Slug = id });
            var response = await _mediator.Send(new BoardListRequest());
            return View(BoardList, response);
        }

        public async Task<IActionResult> View(Guid id) 
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var board = await _mediator.Send(new ViewBoardRequest{ Slug = id });
            return View(BoardView, board);

        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
