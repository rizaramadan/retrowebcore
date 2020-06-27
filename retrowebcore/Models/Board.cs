using System;

namespace retrowebcore.Models
{
    public class Board : IAuditable, ISoftDeletable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? Slug { get; set; }
        public long Creator { get; set; }
        public DateTime Created { get; set; }
        public long Updator { get; set; }
        public DateTime Updated { get; set; }
        public long? Deletor { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
