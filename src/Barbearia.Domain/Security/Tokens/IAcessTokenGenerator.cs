using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Security.Tokens;

public interface IAcessTokenGenerator
{
    public string Generate(Usuario usuario);
}