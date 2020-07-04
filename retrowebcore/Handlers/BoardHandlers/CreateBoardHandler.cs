using MediatR;
using retrowebcore.Models;
using retrowebcore.Persistences;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace retrowebcore.Handlers.Mediators
{
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
}
