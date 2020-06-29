using Microsoft.AspNetCore.Mvc;
using retrowebcore.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retrowebcore.Views.ViewComponents
{
    public abstract class ViewComponentBase : ViewComponent
    {
        protected readonly AppDbContext _context;
        public ViewComponentBase(AppDbContext c) => _context = c;
    }
}
