using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retrowebcore.Models
{
    public interface IAuditable
    {
        ulong Creator { get; set; }
        DateTime Created { get; set; }
        ulong Updator { get; set; }
        DateTime Updated { get; set; }
    }

    public interface ISoftDeletable
    { 
        ulong Deletor { get; set; }
        DateTime? DeletedAt  { get; set; }
    }
}
