using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADProv_Parconsil.Models.Result;
using ADProv_Parconsil.Models.Parameters;
using static ADProv_Parconsil.Models.Result.EMessage;

namespace ADProv_Parconsil.Models.Data
{
    public class DSecurity
    {
        private IConfiguration _configuration;
        public DSecurity(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ESecurity> Login(ESecurity _params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("seg.Usuario_Autentication", sql))
                {
                    //cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ruc", _params.Ruc);
                    cmd.Parameters.AddWithValue("@Usuario", _params.Usuario);
                    cmd.Parameters.AddWithValue("@Clave", _params.Clave);
                    cmd.Parameters.AddWithValue("@IdSignInType", _params.IdSignInType);
                    ESecurity response = new ESecurity();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new ESecurity();
                        if (await reader.ReadAsync())
                        {
                            response = new ESecurity()
                            {
                                ErrorMessage = reader.GetString("ErrorMessage"),
                                IdUsuario = reader.GetInt32("IdUsuario"),
                                
                                idRol = reader.GetInt32("idRol"),
                                nameRol = reader.GetString("nameRol"),
                                idUserTablaUsuario = reader.GetInt32("idUserTablaUsuario"),
                                nametablaUser = reader.GetString("nametablaUser"),
                                IdTipoTablaUsuario = reader.GetInt32("IdTipoTablaUsuario"),
                                User_cliente_contra = reader.GetString("User_cliente_contra"),

                                Usuario = reader.GetString("Usuario"),
                                Clave = reader.GetString("Clave"),
                                Nombres = reader.GetString("Nombres"),
                                ApellidoPaterno = reader.GetString("ApellidoPaterno"),
                                ApellidoMaterno = reader.GetString("ApellidoMaterno"),
                                Email = reader.GetString("Email"),
                                Telefono = reader.GetString("Telefono"),
                                IdPerfil = reader.GetInt32("IdPerfil"),
                                Perfil = reader.GetString("Perfil"),
                                Perfil2 = reader.GetString("Perfil2"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                IdContratista = reader.GetInt32("IdContratista"),
                                IdEmpleado = reader.GetInt32("IdEmpleado"),
                                Ruc = reader.GetString("Ruc"),
                                RazonSocial = reader.GetString("RazonSocial"),
                                NroDocumento = reader.GetString("NroDocumento")
                            };
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<EMessage> Updatepassword(dynamic _params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("seg.Changepassword", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NewPassword", _params.NewPassword);
                    cmd.Parameters.AddWithValue("@Correo", _params.Correo);

                    EMessage response = null;

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new EMessage();
                        while (await reader.ReadAsync())
                        {
                            response = new EMessage
                            {
                                Ok = reader.GetInt32("Ok"),
                                Message = reader.GetString("Message")
                            };
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<ESecurity>> Usuario_List(ESecurity _params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("seg.Usuario_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEstado", _params.IdEstado);
                    cmd.Parameters.AddWithValue("@Search", _params.Search);
                    cmd.Parameters.AddWithValue("@PageIndex", _params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", _params.PageSize);

                    List<ESecurity> response = null;

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<ESecurity>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new ESecurity
                            {
                                IdUsuario = reader.GetInt32("IdUsuario"),
                                Usuario = reader.GetString("Usuario"),
                                Clave = reader.GetString("Clave"),
                                Email = reader.GetString("Email"),
                                Telefono = reader.GetString("Telefono"),
                                Nombres = reader.GetString("Nombres"),
                                ApellidoPaterno = reader.GetString("ApellidoPaterno"),
                                ApellidoMaterno = reader.GetString("ApellidoMaterno"),
                                FullName = reader.GetString("FullName"),
                                IdTablaUsuario = reader.GetInt32("IdTablaUsuario"),
                                IdPerfil = reader.GetInt32("IdPerfil"),
                                Perfil = reader.GetString("Perfil"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Estado = reader.GetString("Estado"),
                                TotalElements = reader.GetInt32("TotalElements"),
                                idRol=reader.GetInt32("idRol"),
                                IdTipoTablaUsuario = reader.GetInt32("IdTipoTablaUsuario"),
                                idUserTablaUsuario = reader.GetInt32("idUserTablaUsuario"),
                                IdTipoTablaUsuarioName= reader.GetString("IdTipoTablaUsuarioName"),
                                idRolName= reader.GetString("idRolName"),
                                idUserTablaUsuarioName= reader.GetString("idUserTablaUsuarioName"),
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<EMessage> Usuario_InsertUpdate(PUsuario p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("seg.Usuario_InsertUpdate", sql))
                {
                    p_Params.idUsertablausuario = p_Params.idUsertablausuario==null ? 0 : p_Params.idUsertablausuario;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", p_Params.IdUsuario);
                    cmd.Parameters.AddWithValue("@Usuario", p_Params.Usuario);
                    cmd.Parameters.AddWithValue("@Clave", p_Params.Clave);
                    cmd.Parameters.AddWithValue("@Email", p_Params.Email);
                    cmd.Parameters.AddWithValue("@Telefono", p_Params.Telefono);
                    cmd.Parameters.AddWithValue("@Nombres", p_Params.Nombres);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", p_Params.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", p_Params.ApellidoMaterno);                                        
                    cmd.Parameters.AddWithValue("@IdEstado", p_Params.IdEstado);
                    cmd.Parameters.AddWithValue("@IdUsuarioAud", p_Params.IdUsuarioAud);

                    cmd.Parameters.AddWithValue("@IdTipoTablaUsuario", p_Params.IdTipoTablaUsuario);
                    cmd.Parameters.AddWithValue("@IdRol", p_Params.IdRol);
                    cmd.Parameters.AddWithValue("@idUsertablausuario", p_Params.idUsertablausuario);
                    EMessage response = null;

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new EMessage();
                        while (await reader.ReadAsync())
                        {
                            response = new EMessage
                            {
                                Ok = reader.GetInt32("Ok"),
                                Message = reader.GetString("Message")
                            };
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<EMessage> Usuario_Delete(int IdUsuario, int IdUsuarioAud)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("seg.Usuario_Delete", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    cmd.Parameters.AddWithValue("@IdUsuarioAud", IdUsuarioAud);
                    EMessage response = new EMessage();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new EMessage();
                        while (await reader.ReadAsync())
                        {
                            response = new EMessage
                            {
                                Ok = reader.GetInt32("Ok"),
                                Message = reader.GetString("Message")
                            };
                        }
                    }

                    return response;
                }
            }
        }

    }
}
