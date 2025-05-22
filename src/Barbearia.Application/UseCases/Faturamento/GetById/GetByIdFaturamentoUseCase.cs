using AutoMapper;
using Barbearia.Communication.Response;
using Barbearia.Domain.Repositories;
using Barbearia.Exception;
using Barbearia.Exception.ExceptionBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.UseCases.Faturamento.GetById
{
    public class GetByIdFaturamentoUseCase : IGetByIdFaturamentoUseCase
    {
        private readonly IFaturamentoReadOnlyRepository _repository;
        private readonly IMapper _imapper;

        public GetByIdFaturamentoUseCase(IFaturamentoReadOnlyRepository repository
                                        ,IMapper imapper)
        {
            _repository = repository;
            _imapper = imapper; 
        }
        public async Task<ResponseFaturamentoJson> Execute(long Id)
        {
            var result = await _repository.GetById(Id);

            if (result is null)
                throw new NotFoundException(ResourceErrorMessages.FATURAMENTO_NAO_ENCONTRADO);

            return _imapper.Map<ResponseFaturamentoJson>(result);
        }
    }
}
