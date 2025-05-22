using Barbearia.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.UseCases.Faturamento.GetById
{
    public interface IGetByIdFaturamentoUseCase
    {
        public Task<ResponseFaturamentoJson> Execute(long Id);
    }
}
