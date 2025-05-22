using System.Net;

namespace Barbearia.Exception.ExceptionBase
{
    public class ErrorValidacaoException: FaturamentoException
    {
        private readonly List<string> _errors;
        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public ErrorValidacaoException(List<string> errorMessages) : base(string.Empty)
        {
            _errors = errorMessages;
        }

        public override List<string> GetErros()
        {
            return _errors;
        }
    }
}
