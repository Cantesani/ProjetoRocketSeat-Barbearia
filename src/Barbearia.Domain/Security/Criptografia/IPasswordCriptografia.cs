namespace Barbearia.Domain.Repositories.Criptografia;

public interface IPasswordCriptografia
{
    public string Encrypt(string password);
    public bool Verify(string password, string hashPassword);

}