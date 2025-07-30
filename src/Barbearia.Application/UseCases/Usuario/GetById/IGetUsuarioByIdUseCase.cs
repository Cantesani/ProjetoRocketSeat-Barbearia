using Barbearia.Communication.Response.Usuario;

namespace Barbearia.Application.UseCases.Usuario.GetById;

public interface IGetUsuarioByIdUseCase
{
    public Task<ResponseUsuarioJson> Execute(long id);

}