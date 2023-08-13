using ADProv_Parconsil.Models.Result;
using ADProv_Parconsil.Models.Parameters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ADProv_Parconsil.Models.Result.EMessage;

namespace ADProv_Parconsil.Models.Data
{
    public class DCliente
    {
        private IConfiguration _configuration;
        public DCliente(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<EDocumento>> Documento_List(EDocumento e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.Documento_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEstado", e_Params.IdEstado);
                    cmd.Parameters.AddWithValue("@Search", e_Params.Search);
                    cmd.Parameters.AddWithValue("@PageIndex", e_Params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", e_Params.PageSize);

                    List<EDocumento> response = new List<EDocumento>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EDocumento>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EDocumento
                            {
                                IdDocumento = reader.GetInt32("IdDocumento"),
                                CodDocumento = reader.GetString("CodDocumento"),
                                IdMatriz = reader.GetInt32("IdMatriz"),
                                Matriz = reader.GetString("Matriz"),
                                NombreDocumento = reader.GetString("NombreDocumento"),
                                TipoExtension = reader.GetString("TipoExtension"),
                                HomEmpresa = reader.GetBoolean("HomEmpresa"),
                                HomTrabajador = reader.GetBoolean("HomTrabajador"),
                                Empresa = reader.GetBoolean("Empresa"),
                                Trabajador = reader.GetBoolean("Trabajador"),

                                TotalHomEmpresa = reader.GetInt32("TotalHomEmpresa"),
                                TotalHomTrabajador = reader.GetInt32("TotalHomTrabajador"),
                                TotalEmpresa = reader.GetInt32("TotalEmpresa"),
                                TotalTrabajador = reader.GetInt32("TotalTrabajador"),


                                TotalElements = reader.GetInt32("TotalElements")
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<EClienteGrupoProyecto> ClienteProyectoGrupo_List(EClienteGrupoProyecto e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("cli.ClienteProyectoGrupo_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", e_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@PageIndex", e_Params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", e_Params.PageSize);

                    EClienteGrupoProyecto response = new EClienteGrupoProyecto();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response.ClienteGrupos = new List<EClienteGrupo>();
                        while (await reader.ReadAsync())
                        {
                            response.ClienteGrupos.Add(new EClienteGrupo()
                            {
                                IdClienteGrupo = reader.GetInt32("IdClienteGrupo"),
                                NombreGrupo = reader.GetString("NombreGrupo"),
                                DocumentoConfiguradoGrupo = reader.GetInt32("DocumentoConfiguradoGrupo"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                FlagEliminado = reader.GetBoolean("FlagEliminado"),
                                UsuarioCreacion = reader.GetString("UsuarioCreacion"),
                                FechaCreacion = reader.GetString("FechaCreacion"),
                                TotalElements = reader.GetInt32("TotalElements")
                            });
                        }

                        if (await reader.NextResultAsync())
                        {
                            response.ClienteProyecto = new List<EClienteProyecto>();
                            while (await reader.ReadAsync())
                            {
                                response.ClienteProyecto.Add(new EClienteProyecto()
                                {
                                    IdProyecto = reader.GetInt32("IdProyecto"),
                                    IdCliente = reader.GetInt32("IdCliente"),
                                    Proyecto = reader.GetString("Proyecto"),
                                    DocumentoConfigurado = reader.GetInt32("DocumentoConfigurado"),
                                    Responsable = reader.GetString("Responsable"),
                                    FlagEliminado = reader.GetBoolean("FlagEliminado"),
                                    UsuarioCreacion = reader.GetString("UsuarioCreacion"),
                                    FechaCreacion = reader.GetString("FechaCreacion"),
                                    TotalElements = reader.GetInt32("TotalElements")
                                });
                            }
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<ECliente> Cliente_List(ECliente e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("cli.Cliente_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", e_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdEstado", e_Params.IdEstado);
                    cmd.Parameters.AddWithValue("@Search", e_Params.Search);
                    cmd.Parameters.AddWithValue("@PageIndex", e_Params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", e_Params.PageSize);

                    ECliente response = new ECliente();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response.Clientes = new List<EClienteDetalle>();
                        while (await reader.ReadAsync())
                        {
                            response.Clientes.Add(new EClienteDetalle
                            {
                                IdCliente = reader.GetInt32("IdCliente"),
                                RUC = reader.GetString("RUC"),
                                RazonSocial = reader.GetString("RazonSocial"),
                                Direccion = reader.GetString("Direccion"),
                                IdUbigeo = reader.GetString("IdUbigeo"),
                                PuntajeMinimo = reader.GetInt32("PuntajeMinimo"),
                                Telefono = reader.GetString("Telefono"),
                                Correo = reader.GetString("Correo"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Estado = reader.GetString("Estado"),

                                Nombres = reader.GetString("Nombres"),
                                UsuarioCreacion = reader.GetString("UsuarioCreacion"),
                                FechaCreacion = reader.GetString("FechaCreacion"),
                                UsuarioModificacion = reader.GetString("UsuarioModificacion"),
                                FechaModificacion = reader.GetString("FechaModificacion"),

                                TotalElements = reader.GetInt32("TotalElements")
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<ECliente> Cliente_Dropdown()
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("cli.Cliente_Dropdown", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    ECliente response = new ECliente();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response.Clientes = new List<EClienteDetalle>();
                        while (await reader.ReadAsync())
                        {
                            response.Clientes.Add(new EClienteDetalle
                            {
                                Active = reader.GetBoolean("Active"),
                                DocsConfigurados = reader.GetBoolean("DocsConfigurados"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                RUC = reader.GetString("RUC"),
                                RazonSocial = reader.GetString("RazonSocial"),
                                Direccion = reader.GetString("Direccion"),
                                Telefono = reader.GetString("Telefono"),
                                Correo = reader.GetString("Correo"),

                                Nombres = reader.GetString("Nombres"),
                                UsuarioCreacion = reader.GetString("UsuarioCreacion"),
                                FechaCreacion = reader.GetString("FechaCreacion"),
                                UsuarioModificacion = reader.GetString("UsuarioModificacion"),
                                FechaModificacion = reader.GetString("FechaModificacion"),

                                TotalElements = reader.GetInt32("TotalElements")
                            });
                        }
                    }
                    return response;
                }
            }
        }


        public async Task<EMessage> Cliente_InsertUpdate(PCliente p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("cli.Cliente_InsertUpdate", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", p_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@RUC", p_Params.RUC);
                    cmd.Parameters.AddWithValue("@RazonSocial", p_Params.RazonSocial);
                    cmd.Parameters.AddWithValue("@Direccion", p_Params.Direccion);
                    cmd.Parameters.AddWithValue("@IdUbigeo", p_Params.IdUbigeo);
                    cmd.Parameters.AddWithValue("@PuntajeMinimo", p_Params.PuntajeMinimo);
                    cmd.Parameters.AddWithValue("@Telefono", p_Params.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", p_Params.Correo);
                    cmd.Parameters.AddWithValue("@IdEstado", p_Params.IdEstado);
                    cmd.Parameters.AddWithValue("@IdUsuarioAud", p_Params.IdUsuarioAud);

                    //cmd.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "@TDocumentoHomologacionE",
                    //    SqlDbType = SqlDbType.Structured,
                    //    TypeName = "doc.DocumentoHomologacionE",
                    //    Value = p_Params.THomologacionE
                    //});
                    //cmd.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "@TDocumentoEmpresa",
                    //    SqlDbType = SqlDbType.Structured,
                    //    TypeName = "doc.DocumentoEmpresa",
                    //    Value = p_Params.TEmpresa
                    //});
                    //cmd.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "@TDocumentoEmpleado",
                    //    SqlDbType = SqlDbType.Structured,
                    //    TypeName = "doc.DocumentoEmpleado",
                    //    Value = p_Params.TEmpleado
                    //});
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@TbClienteGrupo",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "TClienteGrupo",
                        Value = p_Params.TClienteGrupos
                    });
                    cmd.Parameters.AddWithValue("@TbClienteProyecto", p_Params.TClienteProyecto);

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

        public async Task<EMessage> ContratistaDocumentos_Insert(PClienteDocumento p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("cli.ContratistaDocumentos_Insert", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", p_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdClienteGrupo", p_Params.IdClienteGrupo);
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@TDocumentoHomologacionE",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "doc.DocumentoHomologacionE",
                        Value = p_Params.THomologacionE
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@THomEmpresaCabecera",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "doc.HomEmpresaCabecera",
                        Value = p_Params.THomEmpresaCabecera
                    });

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

        public async Task<EMessage> EmpleadosDocumentos_Insert(PClienteDocumento p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("cli.EmpleadosDocumentos_Insert", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", p_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdClienteProyecto", p_Params.IdClienteProyecto);

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@TDocumentoHomEmpleado",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "doc.DocumentoHomEmpleado",
                        Value = p_Params.THomEmpleado
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@TDocumentoEmpresa",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "doc.DocumentoEmpresa",
                        Value = p_Params.TEmpresa
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@TDocumentoEmpleado",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "doc.DocumentoEmpleado",
                        Value = p_Params.TEmpleado
                    });

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

        public async Task<EMessage> EmpleadoClienteHom_Update(PClienteFecha p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("hom.EmpleadoClienteHom_Update", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCtrHomEmpleado", p_Params.IdCtrHomEmpleado);
                    cmd.Parameters.AddWithValue("@FechaVenc", p_Params.FechaVenc);
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

        public async Task<EMessage> Cliente_Delete(int IdCliente, int IdUsuarioAud)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.Cliente_Delete", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", IdCliente);
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

        public async Task<EMessage> ContratistaClienteHom_Delete(int IdCtrHomologacion, int IdCliente, int IdContratista, int IdEstado, string Observacion, int IdUsuarioAud)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("hom.ContratistaClienteHom_Delete", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCtrHomologacion", IdCtrHomologacion);
                    cmd.Parameters.AddWithValue("@IdCliente", IdCliente);
                    cmd.Parameters.AddWithValue("@IdContratista", IdContratista);
                    cmd.Parameters.AddWithValue("@IdEstado", IdEstado);
                    cmd.Parameters.AddWithValue("@Observacion", Observacion);
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

        public async Task<EMessage> EmpleadoClienteHom_Delete(int IdCtrHomEmpleado, int IdCliente, int IdContratista, int IdEstado, string Observacion, int IdUsuarioAud)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("hom.EmpleadoClienteHom_Delete", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCtrHomEmpleado", IdCtrHomEmpleado);
                    cmd.Parameters.AddWithValue("@IdCliente", IdCliente);
                    cmd.Parameters.AddWithValue("@IdContratista", IdContratista);
                    cmd.Parameters.AddWithValue("@IdEstado", IdEstado);
                    cmd.Parameters.AddWithValue("@Observacion", Observacion);
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

        public async Task<List<EDocumentosEmpresa>> EmpresaMatriz_Get(EDocumentosEmpresa e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.EmpresaMatriz_Get", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", e_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdClienteGrupo", e_Params.IdClienteGrupo);

                    List<EDocumentosEmpresa> response = new List<EDocumentosEmpresa>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EDocumentosEmpresa>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EDocumentosEmpresa
                            {
                                IdHomologacion = reader.GetInt32("IdHomologacion"),
                                IdCtrHomologacion = reader.GetInt32("IdCtrHomologacion"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                Puntaje = reader.GetInt32("Puntaje"),
                                SumaPuntaje = reader.GetInt32("SumaPuntaje"),
                                IdMatriz = reader.GetInt32("IdMatriz"),
                                Matriz = reader.GetString("Matriz"),
                                CodDocumento = reader.GetString("CodDocumento"),
                                IdDocumentoName = reader.GetInt32("IdDocumentoName"),
                                DocumentoName = reader.GetString("DocumentoName"),
                                TipoExtension = reader.GetString("TipoExtension"),
                                //IdPeriodicidad = reader.GetInt32("IdPeriodicidad"),
                                //Periodicidad = reader.GetString("Periodicidad"),
                                IdCriticidad = reader.GetInt32("IdCriticidad"),
                                Criticidad = reader.GetString("Criticidad"),
                                IdRegimen = reader.GetInt32("IdRegimen"),
                                Estado = reader.GetBoolean("Estado"),
                                Regimen = reader.GetString("Regimen"),
                                Upload = reader.GetBoolean("Upload"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Archivo = reader.GetString("Archivo"),
                                TamanioArchivo = reader.GetDecimal("TamanioArchivo"),
                                Observacion = reader.GetString("Observacion")
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<EDocumentosEmpleado>> EmpleadoMatriz_Get(EDocumentosEmpleado e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.EmpleadoMatriz_Get", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", e_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdClienteProyecto", e_Params.IdClienteProyecto);

                    List<EDocumentosEmpleado> response = new List<EDocumentosEmpleado>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EDocumentosEmpleado>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EDocumentosEmpleado
                            {
                                IdHomEmpleado = reader.GetInt32("IdHomEmpleado"),
                                IdCtrHomEmpleado = reader.GetInt32("IdCtrHomEmpleado"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                IdMatriz = reader.GetInt32("IdMatriz"),
                                Matriz = reader.GetString("Matriz"),
                                CodDocumento = reader.GetString("CodDocumento"),
                                IdDocumentoName = reader.GetInt32("IdDocumentoName"),
                                DocumentoName = reader.GetString("DocumentoName"),
                                TipoExtension = reader.GetString("TipoExtension"),
                                IdPeriodicidad = reader.GetInt32("IdPeriodicidad"),
                                Periodicidad = reader.GetString("Periodicidad"),
                                IdCriticidad = reader.GetInt32("IdCriticidad"),
                                Criticidad = reader.GetString("Criticidad"),
                                IdRegimenGeneral = reader.GetBoolean("IdRegimenGeneral"),
                                IdRegimenMype = reader.GetBoolean("IdRegimenMype"),
                                IdRegimenAgrario = reader.GetBoolean("IdRegimenAgrario"),
                                IdConstruccionCivil = reader.GetBoolean("IdConstruccionCivil"),
                                Regimen = reader.GetString("Regimen"),
                                Estado = reader.GetBoolean("Estado"),
                                Upload = reader.GetBoolean("Upload"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Archivo = reader.GetString("Archivo"),
                                TamanioArchivo = reader.GetDecimal("TamanioArchivo"),
                                Observacion = reader.GetString("Observacion")
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<EDocumentosEmpresa>> DocumentoHomEmpresa_List(EDocumentosEmpresa e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.DocumentoHomEmpresa_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", e_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdClienteGrupo", e_Params.IdClienteGrupo);
                    cmd.Parameters.AddWithValue("@IdContratista", e_Params.IdContratista);

                    List<EDocumentosEmpresa> response = new List<EDocumentosEmpresa>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EDocumentosEmpresa>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EDocumentosEmpresa
                            {
                                IdHomologacion = reader.GetInt32("IdHomologacion"),
                                IdCtrHomologacion = reader.GetInt32("IdCtrHomologacion"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                Puntaje = reader.GetInt32("Puntaje"),
                                SumaPuntaje = reader.GetInt32("SumaPuntaje"),
                                IdMatriz = reader.GetInt32("IdMatriz"),
                                Matriz = reader.GetString("Matriz"),
                                CodDocumento = reader.GetString("CodDocumento"),
                                IdDocumentoName = reader.GetInt32("IdDocumentoName"),
                                DocumentoName = reader.GetString("DocumentoName"),
                                TipoExtension = reader.GetString("TipoExtension"),
                                Aplica = reader.GetBoolean("Aplica"),                                
                                IdCriticidad = reader.GetInt32("IdCriticidad"),
                                Criticidad = reader.GetString("Criticidad"),
                                IdRegimen = reader.GetInt32("IdRegimen"),
                                Estado = reader.GetBoolean("Estado"),
                                Regimen = reader.GetString("Regimen"),
                                Upload = reader.GetBoolean("Upload"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Archivo = reader.GetString("Archivo"),
                                TamanioArchivo = reader.GetDecimal("TamanioArchivo"),
                                Observacion = reader.GetString("Observacion")
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<EDocumentosEmpleado>> DocumentoHomEmpleado_List(EDocumentosEmpleado e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.DocumentoHomEmpleado_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", e_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdProyecto", e_Params.IdProyecto);
                    cmd.Parameters.AddWithValue("@IdEmpleado", e_Params.IdEmpleado);

                    cmd.Parameters.AddWithValue("@IdRegimenGeneral", e_Params.IdRegimenGeneralParameter);
                    cmd.Parameters.AddWithValue("@IdRegimenMype", e_Params.IdRegimenMypeParameter);
                    cmd.Parameters.AddWithValue("@IdRegimenAgrario", e_Params.IdRegimenAgrarioParameter);
                    cmd.Parameters.AddWithValue("@IdConstruccionCivil", e_Params.IdConstruccionCivilParameter);

                    List<EDocumentosEmpleado> response = new List<EDocumentosEmpleado>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EDocumentosEmpleado>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EDocumentosEmpleado
                            {
                                IdHomEmpleado = reader.GetInt32("IdHomEmpleado"),
                                IdCtrHomEmpleado = reader.GetInt32("IdCtrHomEmpleado"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                IdMatriz = reader.GetInt32("IdMatriz"),
                                Matriz = reader.GetString("Matriz"),
                                CodDocumento = reader.GetString("CodDocumento"),
                                IdDocumentoName = reader.GetInt32("IdDocumentoName"),
                                DocumentoName = reader.GetString("DocumentoName"),
                                TipoExtension = reader.GetString("TipoExtension"),
                                Aplica = reader.GetBoolean("Aplica"),
                                //IdPeriodicidad = reader.GetInt32("IdPeriodicidad"),
                                //Periodicidad = reader.GetString("Periodicidad"),
                                IdCriticidad = reader.GetInt32("IdCriticidad"),
                                Criticidad = reader.GetString("Criticidad"),
                                IdRegimenGeneral = reader.GetBoolean("IdRegimenGeneral"),
                                IdRegimenMype = reader.GetBoolean("IdRegimenMype"),
                                IdRegimenAgrario = reader.GetBoolean("IdRegimenAgrario"),
                                IdConstruccionCivil = reader.GetBoolean("IdConstruccionCivil"),
                                Estado = reader.GetBoolean("Estado"),
                                Regimen = reader.GetString("Regimen"),
                                Upload = reader.GetBoolean("Upload"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Archivo = reader.GetString("Archivo"),
                                TamanioArchivo = reader.GetDecimal("TamanioArchivo"),
                                Observacion = reader.GetString("Observacion")
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<EDocumentosEmpresa>> DocumentoEmpresa_Get(EDocumentosEmpresa e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.DocumentoEmpresa_Get", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDocEmpresa", e_Params.IdDocEmpresa);

                    List<EDocumentosEmpresa> response = new List<EDocumentosEmpresa>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EDocumentosEmpresa>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EDocumentosEmpresa
                            {
                                IdDocEmpresa = reader.GetInt32("IdDocEmpresa"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                IdPeriodicidad = reader.GetInt32("IdPeriodicidad"),
                                Periodicidad = reader.GetString("Periodicidad"),
                                IdCriticidad = reader.GetInt32("IdCriticidad"),
                                Criticidad = reader.GetString("Criticidad"),
                                IdDocumentoName = reader.GetInt32("IdDocumentoName"),
                                DocumentoName = reader.GetString("DocumentoName"),
                                //Estado = reader.GetBoolean("Estado")
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<EDocumentosEmpresa>> DocumentoEmpleado_Get(EDocumentosEmpresa e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.DocumentoEmpleado_Get", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDocEmpleado", e_Params.IdDocEmpleado);

                    List<EDocumentosEmpresa> response = new List<EDocumentosEmpresa>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EDocumentosEmpresa>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EDocumentosEmpresa
                            {
                                IdDocEmpleado = reader.GetInt32("IdDocEmpleado"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                IdPeriodicidad = reader.GetInt32("IdPeriodicidad"),
                                Periodicidad = reader.GetString("Periodicidad"),
                                IdCriticidad = reader.GetInt32("IdCriticidad"),
                                Criticidad = reader.GetString("Criticidad"),
                                IdDocumentoName = reader.GetInt32("IdDocumentoName"),
                                DocumentoName = reader.GetString("DocumentoName"),
                                //Estado = reader.GetBoolean("Estado")
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<EPuntajeFinal>> PuntajeFinal_List(EDocumentosEmpresa e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("hom.PuntajeFinal_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", e_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdClienteGrupo", e_Params.IdClienteGrupo);
                    cmd.Parameters.AddWithValue("@IdContratista", e_Params.IdContratista);
                    List<EPuntajeFinal> response = new List<EPuntajeFinal>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EPuntajeFinal>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EPuntajeFinal
                            {
                                Maximo = reader.GetString("MATRIZ"),
                                PuntajeMinimo = reader.GetInt32("PuntajeMinimo"),
                                RazonSocial = reader.GetString("RazonSocial"),
                                RucContratista = reader.GetString("RucContratista"),
                                RsContratista = reader.GetString("RsContratista"),
                                FechaVencimiento = reader.GetString("FechaVencimiento"),
                                Generales = reader.GetInt32("GENERALES"),
                                SeguySalud = reader.GetInt32("SEGUYSALUD"),
                                Laborales = reader.GetInt32("LABORALES"),
                                Ambiental = reader.GetInt32("AMBIENTAL"),
                                Compliance = reader.GetInt32("COMPLIANCE"),
                                Responsabilidad = reader.GetInt32("RESPONSABILIDAD"),
                                Financiera = reader.GetInt32("FINANCIERA"),
                                Comercial = reader.GetInt32("COMERCIAL"),
                                Calidad = reader.GetInt32("CALIDAD"),
                                Otros = reader.GetInt32("OTROS"),
                            });
                        }
                    }
                    return response;
                }
            }
        }


        public async Task<EMessage> EmpresaDocumento_InsertUpdate(PNewDocCliente p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("doc.EmpresaDocumento_InsertUpdate", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdHomologacion", p_Params.IdHomologacion);
                    cmd.Parameters.AddWithValue("@IdHomEmpresaCabecera", p_Params.IdHomEmpresaCabecera);
                    cmd.Parameters.AddWithValue("@IdCliente", p_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdClienteGrupo", p_Params.IdClienteGrupo);
                    cmd.Parameters.AddWithValue("@IdCriticidad", p_Params.IdCriticidad);
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
        public async Task<EMessage> ContratistaCliente_UpdateEstado(PContratistaUpdate p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("hom.ContratistaCliente_UpdateEstado", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", p_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdContratista", p_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@IdUsuarioAud", p_Params.IdUsuarioAud);
                    cmd.Parameters.AddWithValue("@IdClienteGrupo", p_Params.IdClienteGrupo);

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

        public async Task<EMessage> ContratistaEmpleado_UpdateEstado(PEmpleadoUpdate p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("hom.ContratistaEmpleado_UpdateEstado", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", p_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdClienteProyecto", p_Params.IdClienteProyecto);
                    cmd.Parameters.AddWithValue("@IdEmpleado", p_Params.IdEmpleado);
                    cmd.Parameters.AddWithValue("@IdRegimen", p_Params.IdRegimen);
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


        public async Task<EMessage> ContratistaProyecto_InsertUpdate(PContratistaProyecto p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("hom.ContratistaProyecto_InsertUpdate", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProyectoContratista", p_Params.IdProyectoContratista);
                    cmd.Parameters.AddWithValue("@IdContratista", p_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@IdProyecto", p_Params.IdProyecto);
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


    }
}
