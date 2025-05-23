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

        public async Task<List<Faturamento>> GetByMes(DateOnly mes)
        {
            var dataInicial = new DateTime(year: mes.Year, month: mes.Month, day: 1).Date;
            var diasMes = DateTime.DaysInMonth(year: mes.Year, month: mes.Month);
            var dataFinal = new DateTime(year: mes.Year, month: mes.Month, day: diasMes, hour: 23, minute: 59, second: 59);

            return await _dbContext.faturamento.AsNoTracking()
                                         .Where(x=>x.Data >= dataInicial &&
                                                   x.Data <= dataFinal)
                                         .OrderBy(x=>x.Data)
                                         .ThenBy(x=>x.Titulo)
                                         .ToListAsync();

        }

    }
}
