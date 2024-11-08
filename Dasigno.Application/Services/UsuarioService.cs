using Dasigno.Application.Interfaces;
using Dasigno.Models.Dtos;
using Dasigno.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dasigno.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }
        public async Task<ResponseDto<bool?>> AgregarUsuario(UsuarioRequestDto usuario)
        {
            ResponseDto<bool?> response = await _repo.InsertarUsuario(usuario);
            return response;
        }

        public async Task<ResponseDto<bool?>> ModificarUsuario(UsuarioRequestDto usuario)
        {
            ResponseDto<bool?> response = await _repo.ModificarUsuario(usuario);
            return response;
        }

        public async Task<ResponseDto<bool?>> EliminarUsuario(string IdUsuario)
        {
            ResponseDto<bool?> response = await _repo.EliminarUsuario(IdUsuario);
            return response;
        }
        public async Task<ResponseDto<UsuarioResponseDto>> SeleccionarUsuarioPorId(string IdUsuario)
        {
            ResponseDto<UsuarioResponseDto> response = await _repo.SeleccionarUsuarioPorId(IdUsuario);
            return response;
        }

        public async Task<ResponseDto<List<UsuarioResponseDto>>> SeleccionarUsuarioPorPrimerNombreApellido(string primerNombre, string primerApellido, int pageNumber, int pageSize)
        {
            ResponseDto<List<UsuarioResponseDto>> response = await _repo.SeleccionarUsuarioPorPrimerNombrePrimerApellido(primerNombre,primerApellido, pageNumber, pageSize);
            return response;
        }
    }
}
