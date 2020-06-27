using System;

namespace retrowebcore.Models
{
    public interface IAuditable
    {
        long Creator { get; set; }
        DateTime Created { get; set; }
        long Updator { get; set; }
        DateTime Updated { get; set; }
    }

    public interface ISoftDeletable
    { 
        long? Deletor { get; set; }
        DateTime? DeletedAt  { get; set; }
    }
}
