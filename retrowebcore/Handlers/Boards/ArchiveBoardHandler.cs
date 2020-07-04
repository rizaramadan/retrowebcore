using MediatR;
using Microsoft.EntityFrameworkCore;
using retrowebcore.Persistences;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace retrowebcore.Handlers.Boards
{
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
}
