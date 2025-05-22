using Barbearia.Application.UseCases.Faturamento.Create;
using Barbearia.Application.UseCases.Faturamento.Delete;
using Barbearia.Application.UseCases.Faturamento.GetAll;
using Barbearia.Application.UseCases.Faturamento.GetById;
using Barbearia.Application.UseCases.Faturamento.Update;
using Barbearia.Communication.Request;
using Barbearia.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturamentoController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseFaturamentoJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
                                                [FromBody] RequestFaturamentoJson request
                                               , [FromServices] ICreateFaturamentoUseCase useCase)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseLstFaturamentoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllFaturamentos([FromServices] IGetAllFaturamentoUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Faturamentos.Any())
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(ResponseFaturamentoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdFaturamento([FromRoute] long Id
                                                           , [FromServices] IGetByIdFaturamentoUseCase useCase)
        {
            var response = await useCase.Execute(Id);

            return Ok(response);
        }

        [HttpPut]
        [Route("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Update([FromRoute] long Id
                                               , [FromBody] RequestFaturamentoJson request
                                               , [FromServices] IUpdateFaturamentoUseCase useCase)
        {
            await useCase.Execute(Id, request);
            return Ok();
        }


        [HttpDelete]
        [Route("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long Id
                                               , [FromServices] IDeleteFaturamentoUseCase useCase)
        {
            await useCase.Execute(Id);
            return NoContent();
        }

    }
}
