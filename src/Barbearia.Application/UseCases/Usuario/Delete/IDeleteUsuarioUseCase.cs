namespace Barbearia.Application.UseCases.Usuario.Delete;

public interface IDeleteUsuarioUseCase
{
    public Task Execute(long id);

}