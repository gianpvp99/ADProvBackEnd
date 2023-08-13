using ADProv_Parconsil.Models.Result;
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
    public class DTablaMaestra
    {
        private IConfiguration _configuration;
        public DTablaMaestra(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<ETablaMaestra>> TablaMaestra_Dropdown(ETablaMaestra e_Params)
        {
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.TablaMaestra_Dropdown", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdTabla", e_Params.IdTabla);

                    List<ETablaMaestra> response = new List<ETablaMaestra>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = new List<ETablaMaestra>();
                        while (await reader.ReadAsync())
                        {
                            response.Add(new ETablaMaestra
                            {
                                IdTabla = reader.GetInt32("IdTabla"),
                                IdColumna = reader.GetInt32("IdColumna"),
                                Valor = reader.GetString("Valor"),
                                Descripcion = reader.GetString("Descripcion"),
                            });
                        }
                    }
                    return response;
                }
            }
        }

    }
}
