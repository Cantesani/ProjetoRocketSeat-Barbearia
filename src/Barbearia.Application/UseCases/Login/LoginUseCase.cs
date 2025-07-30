using Barbearia.Communication.Request;
using Barbearia.Communication.Response.Usuario;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.Repositories.Criptografia;
using Barbearia.Domain.Security.Tokens;
using Barbearia.Exception.ExceptionBase;

namespace Barbearia.Application.UseCases.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUsuarioReadOnlyRepository _repository;
    private readonly IPasswordCriptografia _passwordCriptografia;
    private readonly IAcessTokenGenerator _tokenGenerator;

    public LoginUseCase(IUsuarioReadOnlyRepository repository, IPasswordCriptografia passwordCriptografia,  IAcessTokenGenerator tokenGenerator)
    {
        _repository = repository;
        _passwordCriptografia = passwordCriptografia;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseRegisterUsuarioJson> Execute(RequestLoginJson request)
    {
        var usuario = await _repository.GetUsuarioByEmail(request.Email);

        if (usuario is null)
            throw new InvalidLoginException();

        var senhaValida = _passwordCriptografia.Verify(request.Senha, usuario.Senha);

        if (!senhaValida)
            throw new InvalidLoginException();

        return new ResponseRegisterUsuarioJson
        {
            Nome = usuario.Nome,
            Token = _tokenGenerator.Generate(usuario)
        };
    }
}