using MediatR;
using Microsoft.EntityFrameworkCore;
using retrowebcore.Models;
using retrowebcore.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace retrowebcore.Handlers.Mediators
{
    public class BoardHandlerBase
    {
        protected readonly AppDbContext _context;
        public BoardHandlerBase(AppDbContext c)
        {
            _context = c;
        }
    }


    #region BoardListRequest
    public class BoardListRequest : IRequest<BoardListResponse> 
    {
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 100;
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

    public class BoardListResponse 
    {
        public bool HasPrev { get; set; }
        public bool HasNext { get; set; }
        public List<Board> Data { get; set; }
    }
    #endregion

    #region CreateBoardRequest
    public class CreateBoardRequest : IRequest<Board> 
    {
        public string Squad { get; set; }
        public string Sprint { get; set; }
    }

    public class CreateBoardHandler : BoardHandlerBase, IRequestHandler<CreateBoardRequest, Board>
    {
        public CreateBoardHandler(AppDbContext c): base(c) { }
        public async Task<Board> Handle(CreateBoardRequest request, CancellationToken ct)
        {
            var newBoard = new Board 
            { 
                Name = request.Squad, 
                Description = request.Sprint, 
                Slug = Guid.NewGuid()
            };
            await _context.AddAsync(newBoard);
            await _context.SaveChangesAsync(ct);
            return newBoard;
        }
    }
    #endregion

    #region ArchiveBoardRequest
    public class ArchiveBoardRequest : IRequest
    {
        public Guid Slug { get; set; }
    }

    public class ArchiveBoardHandler : BoardHandlerBase, IRequestHandler<ArchiveBoardRequest>
    {
        public ArchiveBoardHandler(AppDbContext c) : base(c) { }
        public async Task<Unit> Handle(ArchiveBoardRequest request, CancellationToken cancellationToken)
        {
            var board = await _context.Boards.FirstOrDefaultAsync(x => x.Slug == request.Slug);
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
            return default;
        }
    }
    #endregion

    #region View Board
    public class ViewBoardRequest : IRequest<Board>
    { 
        public Guid Slug { get; set; }
    }

    public class ViewBoardHandler : BoardHandlerBase, IRequestHandler<ViewBoardRequest, Board> 
    {
        public ViewBoardHandler(AppDbContext c) : base(c) { }

        public async Task<Board> Handle(ViewBoardRequest request, CancellationToken ct)
        {
            var board = await _context.Boards.FirstOrDefaultAsync(x => x.Slug == request.Slug);
            return board;
        }
    }
    #endregion
}
