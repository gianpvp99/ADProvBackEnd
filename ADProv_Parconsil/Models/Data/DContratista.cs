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

namespace ADProv_Parconsil.Models.Data
{
    public class DContratista
    {
        private IConfiguration _configuration;
        public DContratista(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<EContratista> EmpresaDocPresentados_List(EContratista e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("hom.EmpresaDocPresentados_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", e_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdContratista", e_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@IdEstado", e_Params.IdEstado);
                    cmd.Parameters.AddWithValue("@Search", e_Params.Search);
                    cmd.Parameters.AddWithValue("@PageIndex", e_Params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", e_Params.PageSize);

                    EContratista response = new EContratista();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response.ContratistaDocumento = new List<EContratistaDocumento>();
                        while (await reader.ReadAsync())
                        {
                            response.ContratistaDocumento.Add(new EContratistaDocumento
                            {
                                IdCtrHomologacion = reader.GetInt32("IdCtrHomologacion"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                IdContratista = reader.GetInt32("IdContratista"),
                                IdDocumentoName = reader.GetInt32("IdDocumentoName"),
                                NombreDocumento = reader.GetString("NombreDocumento"),
                                SumaPuntaje = reader.GetInt32("SumaPuntaje"),
                                Archivo = reader.GetString("Archivo"),
                                TipoExtension = reader.GetString("TipoExtension"),
                                DriveId = reader.GetString("DriveId"),
                                Observacion = reader.GetString("Observacion"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Estado = reader.GetString("Estado"),
                                DocumentosSubidos = reader.GetInt32("DocumentosSubidos"),
                                Porcentaje = reader.GetInt32("Porcentaje"),
                                TotalElements = reader.GetInt32("TotalElements")

                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<EContratista> ContratistaCliente_List(EContratista e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("ctr.ContratistaCliente_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", e_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdContratista", e_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@IdEstado", e_Params.IdEstado);
                    cmd.Parameters.AddWithValue("@Search", e_Params.Search);
                    cmd.Parameters.AddWithValue("@PageIndex", e_Params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", e_Params.PageSize);

                    EContratista response = new EContratista();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response.ContratistaCliente = new List<EContratistaCliente>();
                        while (await reader.ReadAsync())
                        {
                            response.ContratistaCliente.Add(new EContratistaCliente
                            {
                                Active = reader.GetBoolean("Active"),
                                IdCtrCliente = reader.GetInt32("IdCtrCliente"),
                                IdContratista = reader.GetInt32("IdContratista"),
                                CodContratista = reader.GetString("CodContratista"),
                                IdClienteGrupo = reader.GetInt32("IdClienteGrupo"),
                                NombreGrupo = reader.GetString("NombreGrupo"),
                                IdProyecto = reader.GetInt32("IdProyecto"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                FechaIngreso = reader.GetString("FechaIngreso"),
                                FechaCierre = reader.GetString("FechaCierre"),
                                RucCliente = reader.GetString("RucCliente"),
                                RsCliente = reader.GetString("RsCliente"),
                                RUC = reader.GetString("RUC"),
                                RazonSocial = reader.GetString("RazonSocial"),
                                Direccion = reader.GetString("Direccion"),
                                Telefono = reader.GetString("Telefono"),
                                Correo = reader.GetString("Correo"),

                                PersonaNombres = reader.GetString("PersonaNombres"),
                                PersonaIdTipoDoc = reader.GetInt32("PersonaIdTipoDoc"),
                                PersonaDoc = reader.GetString("PersonaDoc"),
                                PersonaNacyRes = reader.GetString("PersonaNacyRes"),
                                PersonaDom = reader.GetString("PersonaDom"),
                                PersonaPuesto = reader.GetString("PersonaPuesto"),
                                PersonaTelefono = reader.GetString("PersonaTelefono"),
                                PersonaCorreo = reader.GetString("PersonaCorreo"),

                                IdRubro = reader.GetInt32("IdRubro"),
                                IdRegimen = reader.GetString("IdRegimen"),
                                IdUbigeo = reader.GetString("IdUbigeo"),

                                Provincia = reader.GetString("Provincia"),
                                Distrito = reader.GetString("Distrito"),
                                PaginaWeb = reader.GetString("PaginaWeb"),
                                FechaConstitucion = reader.GetString("FechaConstitucion"),

                                DocumentosTotales = reader.GetInt32("DocumentosTotales"),
                                DocumentosPendientes = reader.GetInt32("DocumentosPendientes"),
                                DocumentosAceptados = reader.GetInt32("DocumentosAceptados"),
                                Porcentaje = reader.GetInt32("Porcentaje"),

                                IdEstado = reader.GetInt32("IdEstado"),
                                Estado = reader.GetString("Estado"),
                                TotalElements = reader.GetInt32("TotalElements")
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<EContratistaCliente>> Contratista_List(EContratista e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("ctr.Contratista_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdContratista", e_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@IdEstado", e_Params.IdEstado);
                    cmd.Parameters.AddWithValue("@Search", e_Params.Search);
                    cmd.Parameters.AddWithValue("@PageIndex", e_Params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", e_Params.PageSize);

                    List<EContratistaCliente> response = new List<EContratistaCliente>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EContratistaCliente>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EContratistaCliente
                            {
                                IdContratista = reader.GetInt32("IdContratista"),
                                ContratistaUser = reader.GetString("ContratistaUser"),
                                Clave = reader.GetString("Clave"),
                                RUC = reader.GetString("RUC"),
                                RazonSocial = reader.GetString("RazonSocial"),
                                Direccion = reader.GetString("Direccion"),
                                Provincia = reader.GetString("Provincia"),
                                Distrito = reader.GetString("Distrito"),
                                PaginaWeb = reader.GetString("PaginaWeb"),
                                FechaConstitucion = reader.GetString("FechaConstitucion"),
                                Telefono = reader.GetString("Telefono"),
                                Correo = reader.GetString("Correo"),
                                FechaIngreso = reader.GetString("FechaIngreso"),
                                FechaCierre = reader.GetString("FechaCierre"),

                                PersonaNombres = reader.GetString("PersonaNombres"),
                                PersonaIdTipoDoc = reader.GetInt32("PersonaIdTipoDoc"),
                                PersonaDoc = reader.GetString("PersonaDoc"),
                                PersonaNacyRes = reader.GetString("PersonaNacyRes"),
                                PersonaDom = reader.GetString("PersonaDom"),
                                PersonaPuesto = reader.GetString("PersonaPuesto"),
                                PersonaTelefono = reader.GetString("PersonaTelefono"),
                                PersonaCorreo = reader.GetString("PersonaCorreo"),

                                IdUbigeo = reader.GetString("IdUbigeo"),
                                IdRegimen = reader.GetString("IdRegimen"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Estado = reader.GetString("Estado"),
                                TotalElements = reader.GetInt32("TotalElements")
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<EContratistaCliente>> Contratista_Dropdown(EContratista e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.Contratista_Dropdown", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEstado", e_Params.IdEstado);
                    cmd.Parameters.AddWithValue("@Search", e_Params.Search);
                    cmd.Parameters.AddWithValue("@PageIndex", e_Params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", e_Params.PageSize);

                    List<EContratistaCliente> response = new List<EContratistaCliente>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EContratistaCliente>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EContratistaCliente
                            {
                                IdContratista = reader.GetInt32("IdContratista"),
                                RUC = reader.GetString("RUC"),
                                RazonSocial = reader.GetString("RazonSocial"),
                                Direccion = reader.GetString("Direccion"),
                                Telefono = reader.GetString("Telefono"),
                                Correo = reader.GetString("Correo"),
                                IdUbigeo = reader.GetString("IdUbigeo"),
                                IdRegimen = reader.GetString("IdRegimen"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Estado = reader.GetString("Estado"),
                                TotalElements = reader.GetInt32("TotalElements")
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<EMessage> Contratista_InsertUpdate(PContratista p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.Contratista_InsertUpdate", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdContratista", p_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@RUC", p_Params.RUC);
                    cmd.Parameters.AddWithValue("@RazonSocial", p_Params.RazonSocial);
                    cmd.Parameters.AddWithValue("@Direccion", p_Params.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", p_Params.Telefono);
                    cmd.Parameters.AddWithValue("@IdRegimen", p_Params.IdRegimen);
                    cmd.Parameters.AddWithValue("@IdUbigeo", p_Params.IdUbigeo);
                    cmd.Parameters.AddWithValue("@Correo", p_Params.Correo);
                    cmd.Parameters.AddWithValue("@IdUsuarioAud", p_Params.IdUsuarioAud);
                    //cmd.Parameters.AddWithValue("@TSolicitudCabSustento", p_Params.TSolicitudCabSustento);

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

        public async Task<EMessage> ContratistaSustento_InsertUpdate(PSustentoHomologacion p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("hom.ContratistaSustento_InsertUpdate", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", p_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdContratista", p_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@RUC", p_Params.RUC);
                    cmd.Parameters.AddWithValue("@RazonSocial", p_Params.RazonSocial);
                    cmd.Parameters.AddWithValue("@Direccion", p_Params.Direccion);
                    cmd.Parameters.AddWithValue("@Provincia", p_Params.Provincia);
                    cmd.Parameters.AddWithValue("@Distrito", p_Params.Distrito);
                    cmd.Parameters.AddWithValue("@IdRubro", p_Params.IdRubro);
                    cmd.Parameters.AddWithValue("@PaginaWeb", p_Params.PaginaWeb);
                    cmd.Parameters.AddWithValue("@FechaConstitucion", p_Params.FechaConstitucion);
                    cmd.Parameters.AddWithValue("@Telefono", p_Params.Telefono);

                    cmd.Parameters.AddWithValue("@PersonaNombres", p_Params.PersonaNombres);
                    cmd.Parameters.AddWithValue("@PersonaIdTipoDoc", p_Params.PersonaIdTipoDoc);
                    cmd.Parameters.AddWithValue("@PersonaDoc", p_Params.PersonaDoc);
                    cmd.Parameters.AddWithValue("@PersonaNacyRes", p_Params.PersonaNacyRes);
                    cmd.Parameters.AddWithValue("@PersonaDom", p_Params.PersonaDom);
                    cmd.Parameters.AddWithValue("@PersonaPuesto", p_Params.PersonaPuesto);
                    cmd.Parameters.AddWithValue("@PersonaTelefono", p_Params.PersonaTelefono);
                    cmd.Parameters.AddWithValue("@PersonaCorreo", p_Params.PersonaCorreo);

                    cmd.Parameters.AddWithValue("@IdUsuarioAud", p_Params.IdUsuarioAud);
                    cmd.Parameters.AddWithValue("@TContratistaDocumento", p_Params.TContratistaDocumento);

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

        public async Task<EMessage> EmpleadoSustento_InsertUpdate(PSustentoHomEmpleado p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("hom.EmpleadoSustento_InsertUpdate", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", p_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdContratista", p_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@IdEmpleado", p_Params.IdEmpleado);
                    cmd.Parameters.AddWithValue("@IdUsuarioAud", p_Params.IdUsuarioAud);
                    cmd.Parameters.AddWithValue("@TEmpleadoDocumento", p_Params.TEmpleadoDocumento);

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
        public async Task<EMessage> ContratistaCliente_InsertUpdate(PContratistaCliente p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("ctr.ContratistaCliente_InsertUpdate", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdContratista", p_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@ContratistaUser", p_Params.ContratistaUser);
                    cmd.Parameters.AddWithValue("@Clave", p_Params.Clave);
                    cmd.Parameters.AddWithValue("@RUC", p_Params.RUC);
                    cmd.Parameters.AddWithValue("@RazonSocial", p_Params.RazonSocial);
                    cmd.Parameters.AddWithValue("@Direccion", p_Params.Direccion);
                    cmd.Parameters.AddWithValue("@Correo", p_Params.Correo);
                    cmd.Parameters.AddWithValue("@FechaIngreso", p_Params.FechaIngreso);
                    cmd.Parameters.AddWithValue("@FechaCierre", p_Params.FechaCierre);
                    cmd.Parameters.AddWithValue("@IdUsuarioAud", p_Params.IdUsuarioAud);
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@TContratistaCliente_Enlace",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "ContratistaCliente_Enlace",
                        Value = p_Params.TContratistaCliente_Enlace
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

        public async Task<EMessage> Contratista_Delete(int IdContratista, int IdUsuarioAud)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("ctr.Contratista_Delete", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdContratista", IdContratista);
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

        public async Task<List<GruposContratista>> ContratistaGrupo_List(GruposContratista e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("ctr.ContratistaGrupo_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", e_Params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdContratista", e_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@PageIndex", e_Params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", e_Params.PageSize);

                    List<GruposContratista> response = new List<GruposContratista>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<GruposContratista>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new GruposContratista
                            {
                                IdContratista = reader.GetInt32("IdContratista"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                IdClienteGrupo = reader.GetInt32("IdClienteGrupo"),
                                NombreGrupo = reader.GetString("NombreGrupo"),
                                FlagEliminado = reader.GetBoolean("FlagEliminado"),
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

        public async Task<List<GruposProyecto>> ContratistaProyecto_List(GruposProyecto e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("ctr.ContratistaProyecto_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                    
                    cmd.Parameters.AddWithValue("@IdContratista", e_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@IdProyecto", e_Params.IdProyecto);

                    List<GruposProyecto> response = new List<GruposProyecto>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<GruposProyecto>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new GruposProyecto
                            {
                                IdProyectoContratista = reader.GetInt32("IdProyectoContratista"),
                                IdContratista = reader.GetInt32("IdContratista"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                IdProyecto = reader.GetInt32("IdProyecto"),
                                Proyecto = reader.GetString("Proyecto"),
                                Responsable = reader.GetString("Responsable"),
                            });
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<EClienteSector>> Sector_List(EClienteSector e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("cli.Sector_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEstado", e_Params.IdEstado);
                    cmd.Parameters.AddWithValue("@Search", e_Params.Search);
                    cmd.Parameters.AddWithValue("@PageIndex", e_Params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", e_Params.PageSize);

                    List<EClienteSector> response = new List<EClienteSector>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EClienteSector>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EClienteSector
                            {
                                IdSector = reader.GetInt32("IdSector"),
                                Sector = reader.GetString("Sector"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Estado = reader.GetString("Estado"),
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

        public async Task<EMessage> Sector_InsertUpdate(PClienteSector p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("cli.Sector_InsertUpdate", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdSector", p_Params.IdSector);
                    cmd.Parameters.AddWithValue("@Sector", p_Params.Sector);
                    cmd.Parameters.AddWithValue("@IdEstado", p_Params.IdEstado);
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

        public async Task<EMessage> Sector_Delete(int IdSector, int IdUsuarioAud)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("cli.Sector_Delete", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdSector", IdSector);
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
