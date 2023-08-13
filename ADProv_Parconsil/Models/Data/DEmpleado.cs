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
using Microsoft.AspNetCore.Mvc;

namespace ADProv_Parconsil.Models.Data
{
    public class DEmpleado
    {
        private IConfiguration _configuration;
        public DEmpleado(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<EEmpleado>> Empleado_List(EEmpleado _params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("emp.Empleado_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", _params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdProyecto", _params.IdProyecto);
                    cmd.Parameters.AddWithValue("@IdContratista", _params.IdContratista);
                    cmd.Parameters.AddWithValue("@IdEstado", _params.IdEstado);
                    cmd.Parameters.AddWithValue("@Search", _params.Search);
                    cmd.Parameters.AddWithValue("@PageIndex", _params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", _params.PageSize);

                    List<EEmpleado> response = null;

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EEmpleado>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new EEmpleado
                            {
                                IdEmpleado = reader.GetInt32("IdEmpleado"),
                                IdProyecto = reader.GetInt32("IdProyecto"),
                                FechaInicioProyecto = reader.GetString("FechaInicioProyecto"),
                                IdRegimen = reader.GetInt32("IdRegimen"),
                                
                                IdContratista = reader.GetInt32("IdContratista"),
                                RcContratista = reader.GetString("RcContratista"),
                                RsContratista = reader.GetString("RsContratista"),                                
                                Nombres = reader.GetString("Nombres"),
                                ApellidoPaterno = reader.GetString("ApellidoPaterno"),
                                ApellidoMaterno = reader.GetString("ApellidoMaterno"),
                                FullName = reader.GetString("FullName"),
                                //IdTipoDocumento = reader.GetInt32("IdTipoDocumento"),                                
                                NroDocumento = reader.GetString("NroDocumento"),
                                //IdNacionalidad = reader.GetInt32("IdNacionalidad"),
                                Direccion = reader.GetString("Direccion"),
                                Telefono = reader.GetString("Telefono"),
                                IdUbigeo = reader.GetString("IdUbigeo"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Estado = reader.GetString("Estado"),
                                DocumentosPendientes = reader.GetInt32("DocumentosPendientes"),
                                DocumentosAceptados = reader.GetInt32("DocumentosAceptados"),
                                RegimenGeneral = reader.GetInt32("RegimenGeneral"),
                                RegimenMype = reader.GetInt32("RegimenMype"),
                                RegimenAgrario = reader.GetInt32("RegimenAgrario"),
                                ConstruccionCivil = reader.GetInt32("ConstruccionCivil"),

                                PorcentajeRGeneral = reader.GetInt32("PorcentajeRGeneral"),
                                PorcentajeRMype = reader.GetInt32("PorcentajeRMype"),
                                PorcentajeRAgrario = reader.GetInt32("PorcentajeRAgrario"),
                                PorcentajeCCivil = reader.GetInt32("PorcentajeCCivil"),
                                
                                TotalElements = reader.GetInt32("TotalElements"),
                                cadenaQr = reader.GetString("cadenaQr"),
                                foto=reader.GetString("Foto")

                            });
                        }
                    }
                    return response;
                }
            }
        }


        public async Task<List<EEmpleado>> Empleado_List_document(int IdCliente,string NroDocumento)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("emp.Empleado_List_dni", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", IdCliente);
                    cmd.Parameters.AddWithValue("@document", NroDocumento);
                   
                    List<EEmpleado> response = null;

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<EEmpleado>();
                        if (await reader.ReadAsync())
                        {
                            response.Add(new EEmpleado
                            {
                                IdEmpleado = reader.GetInt32("IdEmpleado"),
                                IdProyecto = reader.GetInt32("IdProyecto"),
                                FechaInicioProyecto = reader.GetString("FechaInicioProyecto"),
                                IdRegimen = reader.GetInt32("IdRegimen"),

                                IdContratista = reader.GetInt32("IdContratista"),
                                RcContratista = reader.GetString("RcContratista"),
                                RsContratista = reader.GetString("RsContratista"),
                                Nombres = reader.GetString("Nombres"),
                                ApellidoPaterno = reader.GetString("ApellidoPaterno"),
                                ApellidoMaterno = reader.GetString("ApellidoMaterno"),
                                FullName = reader.GetString("FullName"),
                                //IdTipoDocumento = reader.GetInt32("IdTipoDocumento"),                                
                                NroDocumento = reader.GetString("NroDocumento"),
                                //IdNacionalidad = reader.GetInt32("IdNacionalidad"),
                                Direccion = reader.GetString("Direccion"),
                                Telefono = reader.GetString("Telefono"),
                                IdUbigeo = reader.GetString("IdUbigeo"),
                                IdEstado = reader.GetInt32("IdEstado"),
                                Estado = reader.GetString("Estado"),
                                DocumentosPendientes = reader.GetInt32("DocumentosPendientes"),
                                DocumentosAceptados = reader.GetInt32("DocumentosAceptados"),
                                RegimenGeneral = reader.GetInt32("RegimenGeneral"),
                                RegimenMype = reader.GetInt32("RegimenMype"),
                                RegimenAgrario = reader.GetInt32("RegimenAgrario"),
                                ConstruccionCivil = reader.GetInt32("ConstruccionCivil"),

                                PorcentajeRGeneral = reader.GetInt32("PorcentajeRGeneral"),
                                PorcentajeRMype = reader.GetInt32("PorcentajeRMype"),
                                PorcentajeRAgrario = reader.GetInt32("PorcentajeRAgrario"),
                                PorcentajeCCivil = reader.GetInt32("PorcentajeCCivil"),

                                TotalElements = reader.GetInt32("TotalElements"),

                                cadenaQr = reader.GetString("cadenaQr"),
                                foto = reader.GetString("Foto")

                            });
                        }
                    }
                    return response;
                }
            }
        }


        public async Task<EMessage> Empleado_InsertUpdate(PEmpleado p_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("emp.Empleado_InsertUpdate", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEmpleado", p_Params.IdEmpleado);
                    cmd.Parameters.AddWithValue("@IdContratista", p_Params.IdContratista);
                    cmd.Parameters.AddWithValue("@IdProyecto", p_Params.IdProyecto);
                    cmd.Parameters.AddWithValue("@FechaInicioProyecto", p_Params.FechaInicioProyecto);
                    cmd.Parameters.AddWithValue("@IdRegimen", p_Params.IdRegimen);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", p_Params.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", p_Params.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Nombres", p_Params.Nombres);
                    cmd.Parameters.AddWithValue("@NroDocumento", p_Params.NroDocumento);
                    cmd.Parameters.AddWithValue("@Direccion", p_Params.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", p_Params.Telefono);
                    cmd.Parameters.AddWithValue("@IdUbigeo", p_Params.IdUbigeo);
                    //cmd.Parameters.AddWithValue("@IdUsuarioAud", p_Params.IdUsuarioAud);
                    //cmd.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "@TEmpleadoContratista_Enlace",
                    //    SqlDbType = SqlDbType.Structured,
                    //    TypeName = "EmpleadoContratista_Enlace",
                    //    Value = p_Params.TEmpleadoContratista_Enlace
                    //});

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

        public async Task<EMessage> Empleado_Delete(EEmpleadoD _params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("emp.Empleado_Delete", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEmpleado", _params.IdEmpleado);
                    cmd.Parameters.AddWithValue("@IdUsuarioAud", _params.IdUsuarioAud);
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

        public async Task<EEmpleadoDocuPresentados> Empleado_DocPresentados_List(EEmpleadoDocuPresentados _params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("emp.Empleado_DocPresentados_List", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", _params.IdCliente);
                    cmd.Parameters.AddWithValue("@IdContratista", _params.IdContratista);
                    cmd.Parameters.AddWithValue("@IdEmpleado", _params.IdEmpleado);
                    cmd.Parameters.AddWithValue("@IdEstado", _params.IdEstado);
                    cmd.Parameters.AddWithValue("@Search", _params.Search);
                    cmd.Parameters.AddWithValue("@PageIndex", _params.PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", _params.PageSize);

                    EEmpleadoDocuPresentados response = new EEmpleadoDocuPresentados();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response.EmpleadoDocumento = new List<EEmpleadoDocumentoPresentado>();
                        while (await reader.ReadAsync())
                        {
                            response.EmpleadoDocumento.Add(new EEmpleadoDocumentoPresentado
                            {
                                IdCtrHomEmpleado = reader.GetInt32("IdCtrHomEmpleado"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                IdContratista = reader.GetInt32("IdContratista"),
                                IdEmpleado = reader.GetInt32("IdEmpleado"),
                                IdDocumentoName = reader.GetInt32("IdDocumentoName"),
                                NombreDocumento = reader.GetString("NombreDocumento"),
                                Archivo = reader.GetString("Archivo"),
                                TipoExtension = reader.GetString("TipoExtension"),
                                IdPeriodicidad = reader.GetInt32("IdPeriodicidad"),
                                Periodicidad = reader.GetString("Periodicidad"),
                                FechaVencimiento = reader.GetString("FechaVencimiento"),

                                DriveId = reader.GetString("DriveId"),
                                Observacion = reader.GetString("Observacion"),                                
                                IdEstado = reader.GetInt32("IdEstado"),
                                Estado = reader.GetString("Estado"),
                                DocumentosTotales = reader.GetInt32("DocumentosTotales"),
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

        public async Task<int> generate_cadena_qr(PClienteImagenQr pClienteImagenQr)
        {
            using (SqlConnection cn= new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd=new SqlCommand("generate_cadena_qr", cn))
                {
                    int result = 0;
                    //prod
                    //string cadenaQr = "https://sistema.adprov.app/web/#/dashboard/clientes/validacion-qr?document="+documentEmpleado;

                    //dev
                    string cadenaQr = "http://localhost:4200/#/dashboard/clientes/validacion-qr?document="+ pClienteImagenQr.documentEmpleado;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idEmpleado", pClienteImagenQr.idEmpleado);
                    cmd.Parameters.AddWithValue("@documentEmpleado", pClienteImagenQr.documentEmpleado);
                    cmd.Parameters.AddWithValue("@cadenaQR", cadenaQr);
                    cmd.Parameters.AddWithValue("@Foto", pClienteImagenQr.foto);
                    cn.Open();
                    result=cmd.ExecuteNonQuery();
                    cn.Close();
                    return result;
                }
            }
        }

    }
}
