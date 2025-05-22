using Barbearia.Communication.Request;
using Barbearia.Communication.Response;

namespace Barbearia.Application.UseCases.Faturamento.Create
{
    public interface ICreateFaturamentoUseCase
    {
        public Task<ResponseFaturamentoJson> Execute(RequestFaturamentoJson request);
    }
}
