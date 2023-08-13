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
    public class DDocumento
    {
        private IConfiguration _configuration;
        public DDocumento(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<EMessage> Documento_InsertUpdate(PDocumento p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.Documento_InsertUpdate", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDocumento", p_Params.IdDocumento);
                    cmd.Parameters.AddWithValue("@IdMatriz", p_Params.IdMatriz);
                    cmd.Parameters.AddWithValue("@NombreDocumento", p_Params.NombreDocumento);
                    cmd.Parameters.AddWithValue("@TipoExtension", p_Params.TipoExtension);
                    cmd.Parameters.AddWithValue("@HomEmpresa", p_Params.HomEmpresa);
                    cmd.Parameters.AddWithValue("@HomTrabajador", p_Params.HomTrabajador);
                    cmd.Parameters.AddWithValue("@Empresa", p_Params.Empresa);
                    cmd.Parameters.AddWithValue("@Trabajador", p_Params.Trabajador);
                    cmd.Parameters.AddWithValue("@IdUsuarioAud", p_Params.IdUsuarioAud);

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


        public async Task<EMessage> DocumentosHomologacion_InsertUpdate(PDocumentosXCliente p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.DocumentosHomologacion_InsertUpdate", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdHomologacion", p_Params.IdHomologacion);
                    //cmd.Parameters.AddWithValue("@IdClienteGrupo", p_Params.IdClienteGrupo);
                    cmd.Parameters.AddWithValue("@IdCliente", p_Params.IdCliente);
                    //cmd.Parameters.AddWithValue("@Puntaje", p_Params.Puntaje);
                    cmd.Parameters.AddWithValue("@SumaPuntaje", p_Params.SumaPuntaje);
                    cmd.Parameters.AddWithValue("@IdMatriz", p_Params.IdMatriz);
                    //cmd.Parameters.AddWithValue("@IdDocumentoName", p_Params.IdDocumentoName);
                    cmd.Parameters.AddWithValue("@IdCriticidad", p_Params.IdCriticidad);
                    //cmd.Parameters.AddWithValue("@IdRegimen", p_Params.IdRegimen);
                    cmd.Parameters.AddWithValue("@Estado", p_Params.Estado);

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

        public async Task<EMessage> Documento_Delete(int IdDocumento, int IdUsuarioAud)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.Documento_Delete", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDocumento", IdDocumento);
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


        public async Task<EMessage> HomologacionEmpresa_Delete(int IdCtrHomologacion)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.HomologacionEmpresa_Delete", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCtrHomologacion", IdCtrHomologacion);                    
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
