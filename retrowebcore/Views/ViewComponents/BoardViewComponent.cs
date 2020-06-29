using Microsoft.AspNetCore.Mvc;
using retrowebcore.Models;
using retrowebcore.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retrowebcore.Views.ViewComponents
{
    public class BoardViewComponent : ViewComponentBase
    {
        public BoardViewComponent(AppDbContext c) : base(c) { }

        public async Task<IViewComponentResult> InvokeAsync(Board board) 
        {
            return View(board);
        }
        
    }
}
