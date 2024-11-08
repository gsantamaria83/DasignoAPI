using Dasigno.Application.Interfaces;
using Dasigno.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DasignoAPI.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost("CrearUsuario")]
        public async Task<ActionResult<ResponseDto<bool?>>> CrearUsuario([FromBody] UsuarioRequestDto usuarioIngreso)
        {
            return Ok(await _service.AgregarUsuario(usuarioIngreso));

        }
        [HttpPost("ModificarUsuario")]
        public async Task<ActionResult<ResponseDto<bool?>>> ModificarUsuario([FromBody] UsuarioRequestDto usuarioIngreso)
        {
            return Ok(await _service.ModificarUsuario(usuarioIngreso));

        }
        [HttpPost("EliminarUsuario")]
        public async Task<ActionResult<ResponseDto<bool?>>> EliminarUsuario([FromQuery] string IdUsuario)
        {
            return Ok(await _service.EliminarUsuario(IdUsuario));

        }
        [HttpGet("SeleccionarUsuarioPorId")]
        public async Task<ActionResult<ResponseDto<UsuarioResponseDto>>> SeleccionarUsuarioPorId([FromQuery] string IdUsuario)
        {
            return Ok(await _service.SeleccionarUsuarioPorId(IdUsuario));

        }
        [HttpGet("SeleccionarUsuarioPorPrimerNombreApellido")]
        public async Task<ActionResult<ResponseDto<UsuarioResponseDto>>> SeleccionarUsuarioPorPrimerNombreApellido([FromQuery] string primerNombre, string primerApellido, int pageNumber, int pageSize)
        {
            return Ok(await _service.SeleccionarUsuarioPorPrimerNombreApellido(primerNombre, primerApellido, pageNumber, pageSize));
        }
    }
}
