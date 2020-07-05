using AgileObjects.AgileMapper;
using MediatR;
using Newtonsoft.Json;
using retrowebcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace retrowebcore.Handlers.Presenters
{
    /// <summary>
    /// This class is created so we could remove Card's dependency to Json. Becuase of the linkage nature of card,
    /// that is card belong to board that in turns have list of cards and each card belong to board so on and so forth, 
    /// we need to ignore it by creating a particular handler for it.
    /// </summary>
    public class CardToJsonRequest : IRequest<string>
    {
        public Card Card { get; set; }
        public CardToJsonRequest(Card c) => Card = c;
    }

    class CardJsonModel : Card
    {
        //TODO: remove JsonIgnore from this file
        [JsonIgnore]
        public new Board Board { get; set; }
    }

    public class CardToJSonHandler : IRequestHandler<CardToJsonRequest, string>
    {
        public Task<string> Handle(CardToJsonRequest req, CancellationToken ct)
        {
            CardJsonModel card = TransformCardToJsonModel(req.Card);
            var jsonCard = JsonConvert.SerializeObject(card);
            return Task.FromResult(jsonCard);
        }

        private static CardJsonModel TransformCardToJsonModel(Card card)
        {
            return Mapper
                .Map(card)
                .ToANew<CardJsonModel>(
                    cfg => cfg.IgnoreSource(c => c.Board) //ignore board to cut off the cyclic reference
                );
        }
    }
}
