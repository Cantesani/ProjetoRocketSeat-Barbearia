using Barbearia.Domain.Repositories;

namespace Barbearia.Application.UseCases.Usuario.Delete;

public class DeleteUsuarioUseCase : IDeleteUsuarioUseCase
{
    private readonly IUsuarioWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUsuarioUseCase(IUsuarioWriteOnlyRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        await _repository.Delete(id);
        await _unitOfWork.Commit();
    }
}