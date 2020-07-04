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
    #region create new card
    public class CreateNewCard : IRequest<Card>
    {
        public string BoardSlug { get; set; }
        public CardType Type { get; set; }
        public Guid Slug { get; set; }
        public string TypeStr { get; set; }

        public bool ValidateSlug()
        {
            var canParseSLug = Guid.TryParse(BoardSlug, out Guid result);
            var canParseType = Enum.TryParse<CardType>(TypeStr, out CardType cardType);
            if (canParseSLug && canParseType)
            {
                Slug = result;
                Type = cardType;
                return true;
            }
            return false;
        }
    }

    public class CreateNewCardHandler : BoardHandlerBase, IRequestHandler<CreateNewCard, Card>
    {
        public CreateNewCardHandler(AppDbContext c) : base(c) { }
        public async Task<Card> Handle(CreateNewCard req, CancellationToken ct)
        {
            if (!req.ValidateSlug())
                return null;

            var board = await _context.Boards
                    .Include(x => x.Cards)
                    .FirstOrDefaultAsync(x => x.Slug == req.Slug);

            if (board == null)
                return null;

            var order = board.Cards.Count(x => x.CardType == req.Type);
            var card = new Card 
            { 
                BoardId = board.Id, 
                CardType = req.Type,
                SortOrder = order
            };
            board.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }
    }
    #endregion
}
