using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Infrastructure.DataAccess
{
    internal class BarbeariaDbContext: DbContext
    {
        public BarbeariaDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Faturamento> faturamento { get; set; }
        public DbSet<Usuario> usuario { get; set; } 
    }
}
