using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.UseCases.Faturamento.Relatorios.Excel
{
    public interface IGerarExcelUseCase
    {
        public Task<byte[]> Execute(DateOnly mes);
    }
}
