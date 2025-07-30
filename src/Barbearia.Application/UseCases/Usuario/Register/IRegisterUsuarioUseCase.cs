using Barbearia.Communication.Request.Usuario;
using Barbearia.Communication.Response.Usuario;

namespace Barbearia.Application.UseCases.Uuario.Register;

public interface IRegisterUsuarioUseCase
{
    public Task<ResponseRegisterUsuarioJson> Execute(RequestRegisterUsuarioJson request);
}