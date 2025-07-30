using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories;

public interface IUsuarioUpdateOnlyRepository
{
    public Task<Usuario> GetUsuarioById(long id);
    
    public void Update(Usuario usuario);
}