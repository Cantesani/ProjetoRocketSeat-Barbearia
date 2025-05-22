using Barbearia.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.UseCases.Faturamento.GetAll
{
    public interface IGetAllFaturamentoUseCase
    {
        public Task<ResponseLstFaturamentoJson> Execute();
    }
}
