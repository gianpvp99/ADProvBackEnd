using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class EContratista
    {
        public int IdContratista { get; set; }
        public int IdCliente { get; set; }
        public int IdEstado { get; set; }
        public string Search { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public List<EContratistaCliente> ContratistaCliente { get; set; }
        public List<EContratistaDocumento> ContratistaDocumento { get; set; }
    }

    public class EContratistaCliente
    {
        public bool Active { get; set; }
        public int IdCtrCliente { get; set; }
        public int IdContratista { get; set; }
        public string CodContratista { get; set; }
        public int IdClienteGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public int IdProyecto { get; set; }
        public int IdCliente { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaCierre { get; set; }

        public string ContratistaUser { get; set; }
        public string Clave { get; set; }
        public string RucCliente { get; set; }        
        public string RsCliente { get; set; }
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }

        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string PaginaWeb { get; set; }
        public string FechaConstitucion { get; set; }       

        public string Telefono { get; set; }
        public string Correo { get; set; }

        public string PersonaNombres { get; set; }
        public int PersonaIdTipoDoc { get; set; }
        public string PersonaDoc { get; set; }
        public string PersonaNacyRes { get; set; }
        public string PersonaDom { get; set; }
        public string PersonaPuesto { get; set; }
        public string PersonaTelefono { get; set; }
        public string PersonaCorreo { get; set; }
      
        public int IdRubro { get; set; }
        public string IdRegimen { get; set; }
        public string IdUbigeo { get; set; }

        public int DocumentosTotales { get; set; }
        public int DocumentosPendientes { get; set; }
        public int DocumentosAceptados { get; set; }

        public int Porcentaje { get; set; }

        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public int TotalElements { get; set; }

        public List<EContratistaDocumento> ContratistaDocumento { get; set; }
    }
}
