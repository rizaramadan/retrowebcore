using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace retrowebcore.Controllers
{
    public class BaseController<T> : Controller
    {
        const string BoardList = nameof(BoardList);

        protected readonly ILogger<T> _logger;
        protected readonly IMediator _mediator;

        public BaseController(ILogger<T> l, IMediator m)
        {
            _logger = l;
            _mediator = m;
        }

        protected void SetLayoutToCentered() => 
            ViewData[R.RootContainerClass] = "container";
    }
}
