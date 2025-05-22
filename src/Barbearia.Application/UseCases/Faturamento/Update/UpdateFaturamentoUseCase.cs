using AutoMapper;
using Barbearia.Communication.Request;
using Barbearia.Communication.Response;
using Barbearia.Domain.Repositories;
using Barbearia.Exception;
using Barbearia.Exception.ExceptionBase;
using System.ComponentModel.DataAnnotations;

namespace Barbearia.Application.UseCases.Faturamento.Update
{
    public class UpdateFaturamentoUseCase : IUpdateFaturamentoUseCase
    {
        private readonly IFaturamentoUpdateOnlyRepository _repository;
        private readonly IMapper _imapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFaturamentoUseCase(IFaturamentoUpdateOnlyRepository repository
                                       ,IMapper imapper
                                       , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _imapper = imapper;
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(long Id, RequestFaturamentoJson request)
        {
            Validate(request);

            var faturamento = await _repository.GetById(Id);

            if (faturamento is null)
                throw new NotFoundException(ResourceErrorMessages.FATURAMENTO_NAO_ENCONTRADO);

            _imapper.Map(request, faturamento);
            _repository.Update(faturamento);

            await _unitOfWork.Commit();
        }

        public void Validate(RequestFaturamentoJson request)
        {
            var validator = new FaturamentoValidation();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorValidacaoException(errorMessages);
            }
        }

    }
}
