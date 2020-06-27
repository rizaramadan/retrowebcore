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
    public class BoardController : BaseController
    {
        const string BoardList = nameof(BoardList);

        readonly ILogger<BoardController> _logger;
        readonly IMediator _mediator;

        public BoardController(ILogger<BoardController> l, IMediator m)
        {
            _logger = l;
            _mediator = m;
        }

        public async Task<IActionResult> List()
        {
            var response = await _mediator.Send(new BoardListRequest());
            SetLayoutToFluid();
            return View(BoardList, response);
        }

        public async Task<IActionResult> Archive(Guid id) 
        {
            if (ModelState.IsValid) 
            {
                await _mediator.Send(new ArchiveBoardRequest{ Slug = id });
                var response = await _mediator.Send(new BoardListRequest());
                SetLayoutToFluid();
                return View(BoardList, response);
            }
            return BadRequest();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
