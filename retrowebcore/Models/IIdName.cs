using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retrowebcore.Models
{
    public interface IIdName
    {
        ulong Id { get; set; }
        string Name { get; set; }
    }
}
