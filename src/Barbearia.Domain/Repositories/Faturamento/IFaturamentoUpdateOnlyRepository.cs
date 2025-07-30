using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
    public interface IFaturamentoUpdateOnlyRepository
    {
        public Task<Faturamento?> GetById(long Id);
        public void Update(Faturamento faturamento);
    }
}
