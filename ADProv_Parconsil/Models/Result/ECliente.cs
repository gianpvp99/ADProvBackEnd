using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class ECliente
    {
        public string Search { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int IdCliente { get; set; }
        public int IdEstado { get; set; }   

        public List<EClienteDetalle> Clientes { get; set; }        
    }

    public class EClienteGrupoProyecto
    {
        public int IdCliente { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        
        public List<EClienteProyecto> ClienteProyecto { get; set; }
        public List<EClienteGrupo> ClienteGrupos { get; set; }
    }

    public class EClienteProyecto
    {
        public int IdCliente { get; set; }        
        public int IdProyecto { get; set; }
        public string Proyecto { get; set; }
        public int DocumentoConfigurado { get; set; }
        public string Responsable { get; set; }
        public bool FlagEliminado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public int TotalElements { get; set; }
    }

    public class EClienteGrupo
    {
        public int IdCliente { get; set; }       
        public int IdClienteGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public int DocumentoConfiguradoGrupo { get; set; }
        public bool FlagEliminado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public int TotalElements { get; set; }        
    }

    public class EClienteSector
    {
        public string Search { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public int IdSector { get; set; }
        public string Sector { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaModificacion { get; set; }
        public int TotalElements { get; set; }
    }
}
