using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
    public interface IFaturamentoReadOnlyRepository
    {
        public Task<List<Faturamento>> GetAll();
        public Task<Faturamento?> GetById(long Id);
        public Task<List<Faturamento>> GetByMes(DateOnly mes);
    }
}
