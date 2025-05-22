using AutoMapper;
using Barbearia.Domain.Repositories;
using Barbearia.Exception;
using Barbearia.Exception.ExceptionBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.UseCases.Faturamento.Delete
{
    public class DeleteFaturamentoUseCase : IDeleteFaturamentoUseCase
    {
        private readonly IFaturamentoWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFaturamentoUseCase(IFaturamentoWriteOnlyRepository repository
                                       , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(long Id)
        {
            var result = await _repository.Delete(Id);
            if (result is false)
                throw new NotFoundException(ResourceErrorMessages.FATURAMENTO_NAO_ENCONTRADO);

            await _unitOfWork.Commit();

        }


    }
}
