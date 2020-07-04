using MediatR;
using Microsoft.EntityFrameworkCore;
using retrowebcore.Models;
using retrowebcore.Persistences;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace retrowebcore.Handlers.Mediators
{
    #region View Board
    public class ViewBoardRequest : IRequest<BoardDetail>
    { 
        public Guid Slug { get; set; }
    }

    public class ViewBoardHandler : BoardHandlerBase, IRequestHandler<ViewBoardRequest, BoardDetail> 
    {
        const string ViewBoardQuery = nameof(ViewBoardQuery);
        public ViewBoardHandler(AppDbContext c) : base(c) { }

        public async Task<BoardDetail> Handle(ViewBoardRequest request, CancellationToken ct)
        {
            var board = await _context.Boards
                .TagWith(ViewBoardQuery)
                .Include(x => x.Cards)
                .FirstOrDefaultAsync(x => x.Slug == request.Slug);
            return new BoardDetail(board);
        }
    }
    #endregion
}
