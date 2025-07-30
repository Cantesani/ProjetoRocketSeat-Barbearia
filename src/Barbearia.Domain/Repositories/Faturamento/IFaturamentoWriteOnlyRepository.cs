using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
    public interface IFaturamentoWriteOnlyRepository
    {
        public Task Add(Faturamento faturamento);
        public Task<bool> Delete(long Id);

    }
}

