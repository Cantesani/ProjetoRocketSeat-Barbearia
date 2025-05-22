using Barbearia.Communication.Request;
using Barbearia.Exception;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.UseCases
{
    public class FaturamentoValidation: AbstractValidator<RequestFaturamentoJson>
    {
        public FaturamentoValidation()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage(ResourceErrorMessages.TITULO_OBRIGATORIO);
            RuleFor(x => x.Valor).GreaterThan(0).WithMessage(ResourceErrorMessages.VALOR_PRECISA_SER_MAIOR_QUE_ZERO);
            RuleFor(x => x.Data).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.DATA_PRECISA_SER_ANTERIOR_A_ATUAL);
            RuleFor(x => x.TipoPagto).IsInEnum().WithMessage(ResourceErrorMessages.TIPO_PAGTO_INVALIDO);
        }
    }
}
