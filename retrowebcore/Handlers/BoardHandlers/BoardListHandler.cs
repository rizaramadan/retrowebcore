using MediatR;
using Microsoft.EntityFrameworkCore;
using retrowebcore.Models;
using retrowebcore.Persistences;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace retrowebcore.Handlers.Mediators
{
    public class BoardListRequest : IRequest<BoardListResponse>
    {
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 100;
    }
    public class BoardListResponse
    {
        public bool HasPrev { get; set; }
        public bool HasNext { get; set; }
        public List<Board> Data { get; set; }
    }
    public class BoardListHandler : BoardHandlerBase, IRequestHandler<BoardListRequest, BoardListResponse>
    {
        static readonly string BoardListHandlerQuery = nameof(BoardListHandlerQuery);
        public BoardListHandler(AppDbContext c): base(c) { }

        public async Task<BoardListResponse> Handle(BoardListRequest r, CancellationToken ct)
        {
            var data = await _context.Boards
                .TagWith(BoardListHandlerQuery)
                .Where(x => x.Deletor == null && x.DeletedAt == null)
                .OrderByDescending(x => x.Created)
                .Take(r.Limit)
                .Skip(r.Offset)
                .ToListAsync();
            var hasMore = false;
            var hasPrev = false;
            if (data.Count > 0)
            {
                var soonest = data.First().Created;
                var oldest = data.Last().Created;
                hasMore = await _context.Boards.AnyAsync(x => x.Created < oldest);
                hasPrev = await _context.Boards.AnyAsync(x => x.Created > soonest);
            }

            return new BoardListResponse { HasPrev = hasPrev, HasNext = hasMore, Data = data };
        }
    }
    
}
