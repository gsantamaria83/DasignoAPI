using Dasigno.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dasigno.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<ResponseDto<bool?>> AgregarUsuario(UsuarioRequestDto usuario);
        Task<ResponseDto<bool?>> ModificarUsuario(UsuarioRequestDto usuario);
        Task<ResponseDto<bool?>> EliminarUsuario(string IdUsuario);
        Task<ResponseDto<UsuarioResponseDto>> SeleccionarUsuarioPorId(string IdUsuario);
        Task<ResponseDto<List<UsuarioResponseDto>>> SeleccionarUsuarioPorPrimerNombreApellido(string primerNombre, string primerApellido, int pageNumber, int pageSize);
    }
}
