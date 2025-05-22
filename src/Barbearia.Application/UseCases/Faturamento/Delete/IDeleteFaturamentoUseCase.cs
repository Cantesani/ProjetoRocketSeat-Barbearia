using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.UseCases.Faturamento.Delete
{
    public interface IDeleteFaturamentoUseCase
    {
        public Task Execute(long Id);
    }
}
