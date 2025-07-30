using Barbearia.Domain.Repositories.Criptografia;
using BC = BCrypt.Net.BCrypt;

namespace Barbearia.Infrastructure.Security;

internal class BCrypt: IPasswordCriptografia
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, string hashPassword)
    {
        return BC.Verify(password, hashPassword);
    }
    
}