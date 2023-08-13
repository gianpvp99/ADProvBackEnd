using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class EDocumento
    {
        public int IdEstado { get; set; }
        public string Search { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int IdDocumento { get; set; }
        public string CodDocumento { get; set; }
        public int IdMatriz { get; set; }
        public string Matriz { get; set; }
        public string NombreDocumento { get; set; }
        public string TipoExtension { get; set; }
        public bool HomEmpresa { get; set; }
        public bool HomTrabajador { get; set; }
        public bool Empresa { get; set; }
        public bool Trabajador { get; set; }

        public int TotalHomEmpresa { get; set; }
        public int TotalHomTrabajador { get; set; }
        public int TotalEmpresa { get; set; }
        public int TotalTrabajador { get; set; }

        public int TotalElements { get; set; }
    }
}
