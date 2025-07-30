using Barbearia.Communication.Request;
using Barbearia.Communication.Response.Usuario;

namespace Barbearia.Application.UseCases.Login;

public interface ILoginUseCase
{
    public Task<ResponseRegisterUsuarioJson> Execute(RequestLoginJson request);
}