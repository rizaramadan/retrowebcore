using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retrowebcore.Models
{
    public enum CardType 
    {
        Unknown = 0,
        Liked = 1,
        Lacked = 2,
        Learned = 3,
        LongedFor = 4
    }

    public class Card : IIdName, IAuditable, ISoftDeletable
    {
        public Card() => Slug = Guid.NewGuid();
        public long Id { get; set; }
        public long BoardId { get; set; }
        public Board Board { get; set; }
        public string Name { get; set; }
        public Guid? Slug { get; set; }
        public CardType CardType { get ; set; }
        public List<long> LikerId { get; set; } = new List<long>();
        public List<long> RelatedCardId { get; set; } = new List<long>();
        public List<Comment> comments { get; set; } = new List<Comment>();
        public int? SortOrder { get; set; }
        public long Creator { get; set; }
        public DateTime Created { get; set; }
        public long Updator { get; set; }
        public DateTime Updated { get; set; }
        public long? Deletor { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
