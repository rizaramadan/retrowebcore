using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace retrowebcore.Controllers
{
    public class CardController : BaseController<CardController>
    {
        public CardController(ILogger<CardController> l, IMediator m) : base(l, m) { }

        public IActionResult Index() => View();

        
    }
}
