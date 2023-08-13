using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ADProv_Parconsil.Models.Result;

namespace ADProv_Parconsil.Models.Parameters
{
    public class PEmpleado
    {
        public int IdEmpleado { get; set; }
        public int IdContratista { get; set; }
        public int IdProyecto { get; set; }
        public string FechaInicioProyecto { get; set; }
        public int IdRegimen { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string NroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string IdUbigeo { get; set; }
        public int IdUsuarioAud { get; set; }

        //public List<EmpleadoContratista_Enlace> empleadoContratistaEnlace { get; set; }

        //[JsonIgnore]
        //public DataTable TEmpleadoContratista_Enlace { get; set; }


        //public class EmpleadoContratista_Enlace
        //{
        //    public int IdEmpContratista { get; set; }
        //    public int IdEmpleado { get; set; }
        //    public int IdCliente { get; set; }
        //    public int IdContratista { get; set; }
        //    public int IdProyecto { get; set; }
        //}
    }
}
