using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retrowebcore.Models
{
    public class Board : IAuditable
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public ulong Creator { get; set; }
        public DateTime Created { get; set; }
        public ulong Updator { get; set; }
        public DateTime Updated { get; set; }
    }
}
