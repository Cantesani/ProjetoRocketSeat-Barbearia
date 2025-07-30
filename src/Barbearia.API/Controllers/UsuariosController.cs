using Barbearia.Application.UseCases.Usuario.Delete;
using Barbearia.Application.UseCases.Usuario.GetById;
using Barbearia.Application.UseCases.Usuario.Update;
using Barbearia.Application.UseCases.Uuario.Register;
using Barbearia.Communication.Request.Usuario;
using Barbearia.Communication.Response;
using Barbearia.Communication.Response.Usuario;
using Barbearia.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RequestRegisterUsuarioJson), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registrar([FromBody] RequestRegisterUsuarioJson request,
            [FromServices] IRegisterUsuarioUseCase useCase)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] long id,
            [FromServices] IDeleteUsuarioUseCase useCase)
        {
            await useCase.Execute(id);

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseUsuarioJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsuarioById([FromRoute] long id,
            [FromServices] IGetUsuarioByIdUseCase useCase)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromRoute] long id,
            [FromBody] RequestUsuarioJson request,
            [FromServices] IUpdateUsuarioUseCase useCase)
        {
            await useCase.Execute(id, request);
            
            return NoContent();
        }
    }
}