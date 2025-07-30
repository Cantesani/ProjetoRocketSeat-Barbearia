using AutoMapper;
using Barbearia.Application.UseCases.Usuario;
using Barbearia.Communication.Request.Usuario;
using Barbearia.Communication.Response.Usuario;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.Repositories.Criptografia;
using Barbearia.Domain.Security.Tokens;
using Barbearia.Exception;
using Barbearia.Exception.ExceptionBase;
using FluentValidation.Results;

namespace Barbearia.Application.UseCases.Uuario.Register;

public class RegisterUsuarioUseCase : IRegisterUsuarioUseCase
{
    private readonly IMapper _imapper;
    private readonly IUsuarioWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuarioReadOnlyRepository _repositoryReadOnly;
    private readonly IPasswordCriptografia _passwordCriptografia;
    private readonly IAcessTokenGenerator _tokenGenerator;

    public RegisterUsuarioUseCase(IMapper imapper,
        IUsuarioWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IUsuarioReadOnlyRepository repositoryReadOnly,
        IPasswordCriptografia passwordCriptografia,
        IAcessTokenGenerator tokenGenerator)
    {
        _imapper = imapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _repositoryReadOnly = repositoryReadOnly;
        _passwordCriptografia =  passwordCriptografia;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseRegisterUsuarioJson> Execute(RequestRegisterUsuarioJson request)
    {
        await Validate(request);

        var usuario = _imapper.Map<Domain.Entities.Usuario>(request);
        usuario.Senha = _passwordCriptografia.Encrypt(request.Senha);
        
        usuario.UserIdentifier = Guid.NewGuid();

        await _repository.Add(usuario);
        await _unitOfWork.Commit();

        return new ResponseRegisterUsuarioJson
        {
            Nome = usuario.Nome,
            Token = _tokenGenerator.Generate(usuario)
        };
    }


    public async Task Validate(RequestRegisterUsuarioJson request)
    {
        var result = new RegisterUsuarioValidator().Validate(request);
        var existeEmail = await _repositoryReadOnly.ExisteUsuarioComEmail(request.Email);

        if (existeEmail)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_JA_EXISTE));

        if (!result.IsValid)
        {
            var errorrMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorValidacaoException(errorrMessage);
        }
    }
}