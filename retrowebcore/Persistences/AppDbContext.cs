using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using retrowebcore.Models;

namespace retrowebcore.Persistences
{
    public partial class AppDbContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
    {
        public DbSet<Board> Boards { get; set; }
    }
}
