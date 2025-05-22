using Barbearia.Communication.Response;
using Barbearia.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Barbearia.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is FaturamentoException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnknowError(context);
            }
        }

        private void HandleProjectException(ExceptionContext context)
        {
            var faturamentoException = (FaturamentoException)context.Exception;
            var errorResponse = new ResponseErrorJson(faturamentoException.GetErros());

            context.HttpContext.Response.StatusCode = faturamentoException.StatusCode;
            context.Result = new ObjectResult(errorResponse);
        }

        private void ThrowUnknowError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson("Unknow error");

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
