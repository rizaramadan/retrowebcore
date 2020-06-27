using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace retrowebcore.Controllers
{
    public class BaseController : Controller
    {
        protected void SetLayoutToFluid() => ViewData[R.RootContainerClass] = "container-fluid";
    }
}
