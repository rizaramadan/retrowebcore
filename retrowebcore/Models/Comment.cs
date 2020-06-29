using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retrowebcore.Models
{
    public class Comment : IIdName, IAuditable, ISoftDeletable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Guid? Slug { get; set; }
        public long CardId { get; set; }
        public Card Card { get; set; }
        public long Creator { get; set; }
        public DateTime Created { get; set; }
        public long Updator { get; set; }
        public DateTime Updated { get; set; }
        public long? Deletor { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
