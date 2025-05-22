using System.Net;

namespace Barbearia.Exception.ExceptionBase
{
    public class NotFoundException : FaturamentoException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErros()
        {
            return [Message];
        }
    }
}
