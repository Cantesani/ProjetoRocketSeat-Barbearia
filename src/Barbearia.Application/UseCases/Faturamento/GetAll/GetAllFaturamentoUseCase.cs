using AutoMapper;
using Barbearia.Communication.Response;
using Barbearia.Domain.Repositories;

namespace Barbearia.Application.UseCases.Faturamento.GetAll
{
    public class GetAllFaturamentoUseCase: IGetAllFaturamentoUseCase
    {

        private readonly IFaturamentoReadOnlyRepository _repository;
        private readonly IMapper _imapper;

        public GetAllFaturamentoUseCase(IFaturamentoReadOnlyRepository repository
                                       ,IMapper imapper)
        {
            _repository = repository;
            _imapper = imapper;
            
        }
        public async Task<ResponseLstFaturamentoJson> Execute()
        {
            var result =  await _repository.GetAll();

            return new ResponseLstFaturamentoJson
            {
                Faturamentos = _imapper.Map<List<ResponseShortFaturamentoJson>>(result)
            };
        }
    }
}
