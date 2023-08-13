using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Parameters
{
    public class PContratistaUpdate
    {
        public int IdCliente { get; set; }
        public int IdContratista { get; set; }
        public int IdClienteGrupo { get; set; }
        public int IdUsuarioAud { get; set; }
        public string Correo { get; set; }
        public string ContratistaUser { get; set; }
    }
    
    public class PEmpleadoUpdate
    {
        public int IdCliente { get; set; }
        public int IdClienteProyecto { get; set; }
        public int IdEmpleado { get; set; }
        public int IdRegimen { get; set; }
        public int IdUsuarioAud { get; set; }
    }

    public class PContratistaProyecto
    {
        public int IdProyectoContratista { get; set; }
        public int IdContratista { get; set; }
        public int IdProyecto { get; set; }
        public int IdUsuarioAud { get; set; }
    }
}
