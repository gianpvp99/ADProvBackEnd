using ADProv_Parconsil.Models.Parameters;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Org.BouncyCastle.Asn1.Cms;
using System;

namespace ADProv_Parconsil.Models.Result
{
    public class EAsistencia
    {
        public int id { get; set; }
        public int IdEmpleado { get; set; }
        public string nombre { get; set; }
        public int IdColumna { get; set; }
        public string Descripcion { get; set; }
        public DateTime fecha_registro { get; set; }
        public DateTime hora_registro { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
    }
}
