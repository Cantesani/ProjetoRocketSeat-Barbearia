using Barbearia.Communication.Request.Usuario;

namespace Barbearia.Application.UseCases.Usuario.Update;

public interface IUpdateUsuarioUseCase
{
    public Task Execute(long id, RequestUsuarioJson request);
}