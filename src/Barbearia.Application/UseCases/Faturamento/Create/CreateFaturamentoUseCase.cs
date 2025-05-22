using AutoMapper;
using Barbearia.Communication.Request;
using Barbearia.Communication.Response;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Enums;
using Barbearia.Domain.Repositories;
using Barbearia.Exception.ExceptionBase;

namespace Barbearia.Application.UseCases.Faturamento.Create
{
    public class CreateFaturamentoUseCase : ICreateFaturamentoUseCase
    {

        private readonly IFaturamentoWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _imapper;

        public CreateFaturamentoUseCase(IFaturamentoWriteOnlyRepository repository
                                       ,IUnitOfWork unitOfWork
                                       ,IMapper imapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _imapper = imapper;
        }
        public async Task<ResponseFaturamentoJson> Execute(RequestFaturamentoJson request)
        {
            Validate(request);

            var entity = _imapper.Map<Domain.Entities.Faturamento>(request);

            await _repository.Add(entity);
            await _unitOfWork.Commit();

            return _imapper.Map<ResponseFaturamentoJson>(entity);
        }


        public void Validate(RequestFaturamentoJson request)
        {
            var validator = new FaturamentoValidation();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x=>x.ErrorMessage).ToList();
                throw new ErrorValidacaoException(errorMessages);
            }
        }
    }
}
