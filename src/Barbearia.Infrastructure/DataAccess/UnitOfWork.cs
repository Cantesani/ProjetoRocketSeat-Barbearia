using Barbearia.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Infrastructure.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly BarbeariaDbContext _dbContext;
        public UnitOfWork(BarbeariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}
