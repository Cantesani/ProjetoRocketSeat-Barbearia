using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Barbearia.Infrastructure.Repositories
{
    internal class FaturamentoRepository : IFaturamentoReadOnlyRepository, IFaturamentoUpdateOnlyRepository, IFaturamentoWriteOnlyRepository
    {

        private readonly BarbeariaDbContext _dbContext;

        public FaturamentoRepository(BarbeariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Faturamento faturamento)
        {
            await _dbContext.faturamento.AddAsync(faturamento);
        }

        public async Task<List<Faturamento>> GetAll()
        {
            return await _dbContext.faturamento.AsNoTracking().ToListAsync();
        }

        async Task<Faturamento?> IFaturamentoReadOnlyRepository.GetById(long Id)
        {
            return await _dbContext.faturamento.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }

        async Task<Faturamento?> IFaturamentoUpdateOnlyRepository.GetById(long Id)
        {
            return await _dbContext.faturamento.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public void Update(Faturamento faturamento)
        {
            _dbContext.faturamento.Update(faturamento);
        }

        public async Task<bool> Delete(long Id)
        {
            var faturamento = await _dbContext.faturamento.FirstOrDefaultAsync(x => x.Id == Id);
            if(faturamento is null)
                return false;

            _dbContext.faturamento.Remove(faturamento);
            return true;
        }

    }
}
