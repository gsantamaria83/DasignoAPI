using Dapper;
using Dasigno.Models.Dtos;
using Dasigno.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dasigno.Repositories.Services
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _configuration;
        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ResponseDto<bool?>> InsertarUsuario(UsuarioRequestDto usuario)
        {
            if (usuario == null)
            {
                object obj = new object();
                ResponseDto<bool?> response = new ResponseDto<bool?>(false, "Usuario se encuentra nulo. Por favor validar ", null);
                return response;
            }

            var errores = usuario.Validar();
            string mensajeError = "";

            foreach (var error in errores)
            {
                mensajeError = mensajeError + ". " + error.ErrorMessage;
            }

            if (errores.Count > 0)
            {
                object obj = new object();
                ResponseDto<bool?> response = new ResponseDto<bool?>(false, "Han ocurrido errores en validacion. " +  mensajeError, null);
                return response;
            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                    {
                        string mensaje = "";
                        bool status = false;
                        string idUsuario = "";

                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@PrimerNombre", value: usuario.PrimerNombre, DbType.String, ParameterDirection.Input, size: 50);
                        parameters.Add("@SegundoNombre", value: usuario.SegundoNombre, DbType.String, ParameterDirection.Input, size: 50);
                        parameters.Add("@PrimerApellido", value: usuario.PrimerApellido, DbType.String, ParameterDirection.Input, size: 50);
                        parameters.Add("@SegundoApellido", value: usuario.SegundoApellido, DbType.String, ParameterDirection.Input, size: 50);
                        parameters.Add("@FechaNacimiento", value: usuario.FechaNacimiento, DbType.DateTime, ParameterDirection.Input);
                        parameters.Add("@Sueldo", value: usuario.Sueldo, DbType.Decimal, ParameterDirection.Input);
                        parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                        parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);
                        parameters.Add("@IdUsuario", value: idUsuario, DbType.String, ParameterDirection.Output);

                        var result = await connection.ExecuteAsync("DasInsertarUsuario", parameters, commandType: CommandType.StoredProcedure);
                        mensaje = parameters.Get<string>("@Mensaje");
                        status = parameters.Get<bool>("@Status");
                        idUsuario = parameters.Get<string>("@IdUsuario");
                        UsuarioResponseDto usuarioResponse = new UsuarioResponseDto
                        {
                            IdUsuario = idUsuario
                        };

                        ResponseDto<bool?> response = new ResponseDto<bool?>(true, mensaje, null);
                        return response;

                    }
                }
                catch (Exception ex)
                {
                    object obj = new object();
                    ResponseDto<bool?> response = new ResponseDto<bool?>(false, ex.Message, null);
                    return response;
                }
            }

            
        }

        public async Task<ResponseDto<bool?>> ModificarUsuario(UsuarioRequestDto usuario)
        {
            if (usuario == null)
            {
                object obj = new object();
                ResponseDto<bool?> response = new ResponseDto<bool?>(false, "Usuario se encuentra nulo. Por favor validar ", null);
                return response;
            }

            var errores = usuario.Validar();
            string mensajeError = "";

            foreach (var error in errores)
            {
                mensajeError = mensajeError + ". " + error.ErrorMessage;
            }

            if (errores.Count > 0)
            {
                object obj = new object();
                ResponseDto<bool?> response = new ResponseDto<bool?>(false, "Han ocurrido errores en validacion. " + mensajeError, null);
                return response;
            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                    {
                        string mensaje = "";
                        bool status = false;
                        string idUsuario = "";

                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@IdUsuarioModif", value: usuario.IdUsuario, DbType.String, ParameterDirection.Input);
                        parameters.Add("@PrimerNombre", value: usuario.PrimerNombre, DbType.String, ParameterDirection.Input, size: 50);
                        parameters.Add("@SegundoNombre", value: usuario.SegundoNombre, DbType.String, ParameterDirection.Input, size: 50);
                        parameters.Add("@PrimerApellido", value: usuario.PrimerApellido, DbType.String, ParameterDirection.Input, size: 50);
                        parameters.Add("@SegundoApellido", value: usuario.SegundoApellido, DbType.String, ParameterDirection.Input, size: 50);
                        parameters.Add("@FechaNacimiento", value: usuario.FechaNacimiento, DbType.DateTime, ParameterDirection.Input);
                        parameters.Add("@Sueldo", value: usuario.Sueldo, DbType.Decimal, ParameterDirection.Input);
                        parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                        parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);
                        parameters.Add("@IdUsuario", value: idUsuario, DbType.String, ParameterDirection.Output);

                        var result = await connection.ExecuteAsync("DasModificarUsuario", parameters, commandType: CommandType.StoredProcedure);
                        mensaje = parameters.Get<string>("@Mensaje");
                        status = parameters.Get<bool>("@Status");
                        idUsuario = parameters.Get<string>("@IdUsuario");
                        UsuarioResponseDto usuarioResponse = new UsuarioResponseDto
                        {
                            IdUsuario = idUsuario
                        };

                        ResponseDto<bool?> response = new ResponseDto<bool?>(true, mensaje, null);
                        return response;

                    }
                }
                catch (Exception ex)
                {
                    object obj = new object();
                    ResponseDto<bool?> response = new ResponseDto<bool?>(false, ex.Message, null);
                    return response;
                }
            }
        }
        public async Task<ResponseDto<bool?>> EliminarUsuario(string IdUsuario)
        {
            if (string.IsNullOrEmpty(IdUsuario))
            {
                object obj = new object();
                ResponseDto<bool?> response = new ResponseDto<bool?>(false, "Id de Usuario se encuentra vacio o nulo. Por favor validar");
                return response;
            }

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    string mensaje = "";
                    bool status = false;
                    string idUsuario = "";

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@IdUsuario", value: IdUsuario, DbType.String, ParameterDirection.Input);
                    parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                    parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);

                    var result = await connection.ExecuteAsync("DasEliminarUsuario", parameters, commandType: CommandType.StoredProcedure);
                    mensaje = parameters.Get<string>("@Mensaje");
                    status = parameters.Get<bool>("@Status");
                    UsuarioResponseDto usuarioResponse = new UsuarioResponseDto
                    {
                        IdUsuario = idUsuario
                    };

                    ResponseDto<bool?> response = new ResponseDto<bool?>(true, mensaje);
                    return response;

                }
            }
            catch (Exception ex)
            {
                object obj = new object();
                ResponseDto<bool?> response = new ResponseDto<bool?>(false, ex.Message);
                return response;
            }
        }

        public async Task<ResponseDto<UsuarioResponseDto>> SeleccionarUsuarioPorId(string idUsuario)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    string mensaje = "";
                    bool status = false;
                    UsuarioResponseDto usuario = new UsuarioResponseDto();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@IdUsuario", value: idUsuario, DbType.String, ParameterDirection.Input, size: 50);
                    parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                    parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);

                    var result = await connection.QueryAsync<UsuarioResponseDto>("DasSeleccionarUsuarioPorId", parameters, commandType: CommandType.StoredProcedure);

                    if (result != null)
                    {
                        usuario = (UsuarioResponseDto) result.FirstOrDefault();
                    }


                    mensaje = parameters.Get<string>("@Mensaje");
                    status = parameters.Get<bool>("@Status");
                    ResponseDto<UsuarioResponseDto> response = new ResponseDto<UsuarioResponseDto>(true, mensaje, usuario);
                    return response;

                }
            }
            catch (Exception)
            {
                ResponseDto<UsuarioResponseDto> response = new ResponseDto<UsuarioResponseDto>(false, "Ha ocurrido un error en seleccionar usuario por id");
                return response;
            }
        }

        public async Task<ResponseDto<List<UsuarioResponseDto>>> SeleccionarUsuarioPorPrimerNombrePrimerApellido(string primerNombre, string primerApellido, int pageNumber, int pageSize)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    string mensaje = "";
                    bool status = false;
                    List<UsuarioResponseDto> usuario = new List<UsuarioResponseDto>();

                    if (string.IsNullOrEmpty(primerNombre))
                    {
                        primerNombre = "";
                    }
                    if (string.IsNullOrEmpty(primerApellido))
                    {
                        primerApellido = "";
                    }

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@PrimerNombre", value: primerNombre, DbType.String, ParameterDirection.Input, size: 50);
                    parameters.Add("@PrimerApellido", value: primerApellido, DbType.String, ParameterDirection.Input, size: 50);
                    parameters.Add("@PageNumber", value: pageNumber, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@PageSize", value: pageSize, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                    parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);

                    var result = await connection.QueryAsync<UsuarioResponseDto>("DasSeleccionarUsuarioPorPrimerNombrePrimerApellido", parameters, commandType: CommandType.StoredProcedure);

                    if (result != null)
                    {
                        usuario = result.ToList();
                    }


                    mensaje = parameters.Get<string>("@Mensaje");
                    status = parameters.Get<bool>("@Status");
                    ResponseDto<List<UsuarioResponseDto>> response = new ResponseDto<List<UsuarioResponseDto>>(true, mensaje, usuario);
                    return response;

                }
            }
            catch (Exception)
            {
                ResponseDto<List<UsuarioResponseDto>> response = new ResponseDto<List<UsuarioResponseDto>>(false, "Ha ocurrido un error en seleccionar usuario por id");
                return response;
            }
        }
    }
}
