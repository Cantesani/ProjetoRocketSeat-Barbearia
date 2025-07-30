using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
    public interface IUsuarioWriteOnlyRepository
    {
        public Task Add(Usuario usuario);
        public Task Delete(long Id);
    }
}