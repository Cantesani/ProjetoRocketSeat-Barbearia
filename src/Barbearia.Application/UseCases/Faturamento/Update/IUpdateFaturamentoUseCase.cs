using Barbearia.Communication.Request;
using Barbearia.Communication.Response;

namespace Barbearia.Application.UseCases.Faturamento.Update
{
    public interface IUpdateFaturamentoUseCase
    {
        public Task Execute(long Id, RequestFaturamentoJson request);
    }
}
