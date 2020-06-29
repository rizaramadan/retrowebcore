using Microsoft.AspNetCore.Mvc;
using retrowebcore.Models;
using retrowebcore.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retrowebcore.Views.ViewComponents
{
    public class CardViewComponent : ViewComponentBase
    {
        public CardViewComponent(AppDbContext c) : base(c) { }

        public async Task<IViewComponentResult> InvokeAsync(Card card)
        {
            return View(card);
        }

    }
}
