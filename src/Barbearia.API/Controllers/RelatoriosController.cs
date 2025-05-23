using Barbearia.Application.UseCases.Faturamento.Relatorios.Excel;
using Barbearia.Application.UseCases.Faturamento.Relatorios.Pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Barbearia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        [HttpGet("Excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExcel([FromHeader] DateOnly mes
                                                 ,[FromServices] IGerarExcelUseCase useCase)
        {
            byte[] file = await useCase.Execute(mes);

            if (file.Length > 0)
            {
                var nomeArquivo = "Reports_" + mes.ToString() + ".xlsx";

                return File(file, MediaTypeNames.Application.Octet, nomeArquivo);
            }

            return NoContent();
        }

        [HttpGet("Pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPdf([FromHeader] DateOnly mes
                                               ,[FromServices] IGerarPdfUseCase useCase)
        {
            byte[] file = await useCase.Execute(mes);

            if (file.Length > 0)
            {
                var nomeArquivo = "Reports_" + mes.ToString() + ".pdf";

                return File(file, MediaTypeNames.Application.Pdf, nomeArquivo);
            }
                        
            return NoContent();
        }

    }




}
