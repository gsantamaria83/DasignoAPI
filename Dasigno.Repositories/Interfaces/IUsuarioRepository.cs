using Dasigno.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dasigno.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<ResponseDto<bool?>> InsertarUsuario(UsuarioRequestDto usuario);
        Task<ResponseDto<bool?>> ModificarUsuario(UsuarioRequestDto usuario);
        Task<ResponseDto<bool?>> EliminarUsuario(string IdUsuario);
        Task<ResponseDto<UsuarioResponseDto>> SeleccionarUsuarioPorId(string idUsuario);
        Task<ResponseDto<List<UsuarioResponseDto>>> SeleccionarUsuarioPorPrimerNombrePrimerApellido(string primerNombre, string primerApellido, int pageNumber, int pageSize);
    }
}
