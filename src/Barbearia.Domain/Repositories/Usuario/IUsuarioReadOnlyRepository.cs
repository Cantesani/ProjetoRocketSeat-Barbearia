using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories;

public interface IUsuarioReadOnlyRepository
{
    public Task<bool> ExisteUsuarioComEmail(string email);
    public Task<Usuario?> GetUsuarioById(long Id);
    public Task<Usuario?> GetUsuarioByEmail(string email);
    
}