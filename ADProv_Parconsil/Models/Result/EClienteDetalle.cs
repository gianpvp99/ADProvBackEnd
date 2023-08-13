using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class EClienteDetalle
    {
        public bool Active { get; set; }

        public int IdCliente { get; set; }        
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string IdUbigeo { get; set; }
        public int PuntajeMinimo { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool DocsConfigurados { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }

        public string Nombres { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaModificacion { get; set; }

        public int TotalElements { get; set; }

        public List<EContratistaDocumento> ContratistaDocumento { get; set; }
    }
}
