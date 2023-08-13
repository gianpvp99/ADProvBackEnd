using ADProv_Parconsil.Interface;
using ADProv_Parconsil.Models.Parameters;
using ADProv_Parconsil.Models.Result;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ADProv_Parconsil.Models.Data
{
    public class DAsistencia:IAsistencia
    {
        private IConfiguration _configuration;
        public DAsistencia(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(PAsistencia pAsistencia)
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("sp_asistencias_empleado_contratista_add", cn))
                {
                    int response = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_empleado", pAsistencia.id_empleado);
                    cmd.Parameters.AddWithValue("@tipo_registro", pAsistencia.tipo_registro);
                    cmd.Parameters.AddWithValue("@id_user", pAsistencia.id_user);
                    cmd.Parameters.AddWithValue("@observacion", pAsistencia.observacion);
                    cn.Open();
                    response = cmd.ExecuteNonQuery();
                    cn.Close();
                    return response;
                   
                }
            }
        }

        public async Task<List<EAsistencia>> List(PAsistenciaList pAsistenciaList)
        {
            using (SqlConnection cn=new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd=new SqlCommand("sp_asistencias_empleado_contratista_list", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fechainicio", pAsistenciaList.fechainicio);
                    cmd.Parameters.AddWithValue("@fechaFin", pAsistenciaList.fechaFin);
                    cmd.Parameters.AddWithValue("@idCliente", pAsistenciaList.idCliente);
                    cmd.Parameters.AddWithValue("@text", pAsistenciaList.text);
                    cmd.Parameters.AddWithValue("@Page", pAsistenciaList.Page);
                    cmd.Parameters.AddWithValue("@PageSize", pAsistenciaList.PageSize);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<EAsistencia> list = new List<EAsistencia>();
                        while (dr.Read())
                        {
                            list.Add(new EAsistencia()
                            {
                                id = dr.GetInt32("id"),
                                IdEmpleado = dr.GetInt32("IdEmpleado"),
                                nombre = dr.GetString("nombre"),
                                IdColumna = dr.GetInt32("IdColumna"),
                                Descripcion = dr.GetString("Descripcion"),
                                fecha_registro = dr.GetDateTime("fecha_hora_registro"),
                                hora_registro = dr.GetDateTime("fecha_hora_registro"),
                                IdUsuario = dr.GetInt32("IdUsuario"),
                                Usuario = dr.GetString("Usuario")

                            });
                        }
                        return list;
                    }
                }
            }
        }
    }
}
