using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using retrowebcore.Handlers.Mediators;
using retrowebcore.Models;

namespace retrowebcore.Controllers
{
    public class BoardController : Controller
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
            ViewData["RootContainerClass"] = "container-fluid";
            return View(BoardList, response);
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
