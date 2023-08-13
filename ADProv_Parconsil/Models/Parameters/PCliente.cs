using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ADProv_Parconsil.Models.Result;

namespace ADProv_Parconsil.Models.Parameters
{
    public class PCliente
    {
        public int IdCliente { get; set; }        
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string IdUbigeo { get; set; }
        public int PuntajeMinimo { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int IdEstado { get; set; }
        public int IdUsuarioAud { get; set; }
        public string Codigo { get; set; }

        public List<ClienteProyecto> TablaClienteProyecto { get; set; }
        public List<ClienteGrupo> TablaClienteGrupo { get; set; }
        //public List<DocumentoHomologacionEmpresa> TDocumentoHomologacionE { get; set; }
        //public List<DocumentosEmpresa> TDocumentoEmpresa { get; set; }
        //public List<DocumentosEmpleado> TDocumentoEmpleado { get; set; }

        [JsonIgnore]
        public DataTable TClienteProyecto { get; set; }
        public DataTable TClienteGrupos { get; set; }        
        //public DataTable THomologacionE { get; set; }
        //public DataTable TEmpresa { get; set; }
        //public DataTable TEmpleado { get; set; }

        public class ClienteProyecto
        {
            public int IdFila { get; set; }
            public int IdProyecto { get; set; }
            public string Proyecto { get; set; }
            public string Responsable { get; set; }
        }

        public class ClienteGrupo
        {
            public int IdFila { get; set; }
            public int IdClienteGrupo { get; set; }
            public string NombreGrupo { get; set; }            
            public bool FlagEliminado { get; set; }
        }          

        //public class DocumentoHomologacionEmpresa
        //{
        //    public int IdHomologacion { get; set; }
        //    public int IdCliente { get; set; }
        //    public int IdDocumentoName { get; set; }
        //    public int IdPeriodicidad { get; set; }
        //    public int IdCriticidad { get; set; }
        //    public int IdRegimen { get; set; }
        //    public bool Estado { get; set; }
        //}

        //public class DocumentosEmpresa
        //{
        //    public int IdDocEmpresa { get; set; }
        //    public int IdCliente { get; set; }
        //    public int IdDocumentoName { get; set; }
        //    public bool Estado { get; set; }
        //}

        //public class DocumentosEmpleado
        //{
        //    public int IdDocEmpleado { get; set; }
        //    public int IdCliente { get; set; }
        //    public int IdDocumentoName { get; set; }
        //    public bool Estado { get; set; }
        //}

        //public List<SolicitudCabSustento> SolicitudCabSustento { get; set; }
        //[JsonIgnore]

        //public DataTable TSolicitudCabSustento { get; set; }
    }

    public class PClienteSector
    {
        public int IdSector { get; set; }
        public string Sector { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public int IdUsuarioAud { get; set; }
    }


    public class PClienteImagenQr
    {
        public int idEmpleado { get; set; }
        public string documentEmpleado { get; set; }
        public string foto { get; set; }
    }
}
