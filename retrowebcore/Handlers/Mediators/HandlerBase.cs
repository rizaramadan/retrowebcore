using retrowebcore.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retrowebcore.Handlers.Mediators
{
    public class HandlerBase
    {
        protected readonly AppDbContext _context;
        public HandlerBase(AppDbContext c)
        {
            _context = c;
        }
    }
}
