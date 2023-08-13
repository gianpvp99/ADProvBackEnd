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
    public class PClienteDocumento
    {
        public int IdCliente { get; set; }
        public int IdClienteGrupo { get; set; }
        public int IdClienteProyecto { get; set; }

        public List<DocumentoHomologacionEmpresa> TDocumentoHomologacionE { get; set; }
        public List<DocumentoHomEmpresaCabecera> TDocumentoHomEmpresaCabecera { get; set; }

        public List<DocumentoHomologacionEmpleado> TDocumentoHomEmpresa { get; set; }

        public List<DocumentosEmpresa> TDocumentoEmpresa { get; set; }
        public List<DocumentosEmpleado> TDocumentoEmpleado { get; set; }

        [JsonIgnore]
        public DataTable THomologacionE { get; set; }
        public DataTable THomEmpresaCabecera { get; set; }

        public DataTable THomEmpleado { get; set; }

        public DataTable TEmpresa { get; set; }
        public DataTable TEmpleado { get; set; }

        public class DocumentoHomologacionEmpresa
        {
            public int IdHomologacion { get; set; }
            public int IdCliente { get; set; }
            public int SumaPuntaje { get; set; }
            public int IdDocumentoName { get; set; }
            public int IdMatriz { get; set; }
            public int IdCriticidad { get; set; }
            public int IdRegimen { get; set; }
            public bool Estado { get; set; }
        }

        public class DocumentoHomEmpresaCabecera
        {
            public int IdHomEmpresaCabecera { get; set; }
            public int IdCliente { get; set; }
            public int Puntaje { get; set; }
            public int IdMatriz { get; set; }
            public int IdDocumentoName { get; set; }
        }

        public class DocumentoHomologacionEmpleado
        {
            public int IdHomEmpleado { get; set; }
            public int IdCliente { get; set; }
            public int IdDocumentoName { get; set; }
            public int IdPeriodicidad { get; set; }
            public int IdCriticidad { get; set; }

            public bool IdRegimenGeneral { get; set; }
            public bool IdRegimenMype { get; set; }
            public bool IdRegimenAgrario { get; set; }
            public bool IdConstruccionCivil { get; set; }

            public bool Estado { get; set; }
        }

        public class DocumentosEmpresa
        {
            public int IdDocEmpresa { get; set; }
            public int IdCliente { get; set; }
            public int IdDocumentoName { get; set; }
            public bool Estado { get; set; }
        }

        public class DocumentosEmpleado
        {
            public int IdDocEmpleado { get; set; }
            public int IdCliente { get; set; }
            public int IdDocumentoName { get; set; }
            public bool Estado { get; set; }
        }

        //public List<SolicitudCabSustento> SolicitudCabSustento { get; set; }
        //[JsonIgnore]

        //public DataTable TSolicitudCabSustento { get; set; }
    }

    public class PClienteFecha
    {
        public int IdCtrHomEmpleado { get; set; }
        public string FechaVenc { get; set; }
        public int IdUsuarioAud { get; set; }
    }
}
