using System.Net;

namespace Barbearia.Exception.ExceptionBase;

public class InvalidLoginException : FaturamentoException
{
    public InvalidLoginException() : base(ResourceErrorMessages.EMAIL_OU_SENHA_INVALIDOS)
    {
    }
    
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErros()
    {
        return [Message];
    }
}