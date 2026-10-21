using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppUtbildning.Models
{
    public class RegisterDbContext : IdentityDbContext
    {
        public RegisterDbContext(DbContextOptions<RegisterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Register> Registers { get; set; }
    }
}

// Efter failade försök, fick jag hjälp av en kompis, men använda mina egna varibler