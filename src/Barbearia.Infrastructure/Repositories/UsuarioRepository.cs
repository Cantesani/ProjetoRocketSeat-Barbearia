using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Infrastructure.Repositories;

internal class UsuarioRepository: IUsuarioWriteOnlyRepository, IUsuarioReadOnlyRepository, IUsuarioUpdateOnlyRepository
{
    private readonly BarbeariaDbContext  _dbContext;

    public UsuarioRepository(BarbeariaDbContext  dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Usuario usuario)
    {
       await _dbContext.usuario.AddAsync(usuario); 
    }

    public async Task<bool> ExisteUsuarioComEmail(string email)
    {
        return await _dbContext.usuario.AsNoTracking().AnyAsync(x=>x.Email.Equals(email));
    }

    public async Task Delete(long Id)
    {
        var usuario = await _dbContext.usuario.AsNoTracking().FirstAsync(x=>x.Id ==  Id);
        _dbContext.usuario.Remove(usuario);
    }

    async Task<Usuario?> IUsuarioReadOnlyRepository.GetUsuarioById(long Id)
    {
        return await _dbContext.usuario.AsNoTracking().FirstOrDefaultAsync(x=>x.Id ==  Id);
    }
    
    async Task<Usuario?> IUsuarioUpdateOnlyRepository.GetUsuarioById(long Id)
    {
        return await _dbContext.usuario.FirstOrDefaultAsync(x=>x.Id ==  Id);
    }

    public void Update(Usuario usuario)
    {
        _dbContext.usuario.Update(usuario);
    }

    public async Task<Usuario?> GetUsuarioByEmail(string email)
    {
        return await _dbContext.usuario.AsNoTracking().FirstOrDefaultAsync(x=>x.Email.Equals(email));
    }
    
}