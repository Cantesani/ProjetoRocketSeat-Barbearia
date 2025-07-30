using AutoMapper;
using Barbearia.Communication.Request.Usuario;
using Barbearia.Domain.Repositories;
using Barbearia.Exception.ExceptionBase;

namespace Barbearia.Application.UseCases.Usuario.Update;

public class UpdateUsuarioUseCase : IUpdateUsuarioUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUsuarioUpdateOnlyRepository _repository;

    public UpdateUsuarioUseCase(IUnitOfWork unitOfWork, IMapper mapper, IUsuarioUpdateOnlyRepository repository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = repository;
    }
    public async Task Execute(long id, RequestUsuarioJson request)
    {
        Validate(request);
        var usuario = await _repository.GetUsuarioById(id);
        
        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        
        _repository.Update(usuario);
        await _unitOfWork.Commit();

    }

    public void Validate(RequestUsuarioJson request)
    {
        var validator = new UpdateUsuarioValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorValidacaoException(errorMessages);
        }
    }
}