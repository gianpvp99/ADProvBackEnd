using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class EDocumentosEmpresa
    {
        public int IdHomologacion { get; set; }
        public int IdDocEmpresa { get; set; }
        public int IdDocEmpleado { get; set; }

        public int IdCtrHomologacion { get; set; }
        public int IdCliente { get; set; }
        public int IdContratista { get; set; }
        public int IdClienteGrupo { get; set; }
        public int Puntaje { get; set; }
        public int SumaPuntaje { get; set; }
        public int IdMatriz { get; set; }
        public string Matriz { get; set; }
        public string CodDocumento { get; set; }
        public int IdDocumentoName { get; set; }
        public string DocumentoName { get; set; }
        public string TipoExtension { get; set; }
        public bool Aplica { get; set; }
        public int IdPeriodicidad { get; set; }
        public string Periodicidad { get; set; }
        public int IdCriticidad { get; set; }
        public string Criticidad { get; set; }
        public int IdRegimen { get; set; }
        public string Regimen { get; set; }
        public bool Estado { get; set; }
        public bool Upload { get; set; }
        public int IdEstado { get; set; }
        public string Archivo { get; set; }
        public string Observacion { get; set; }
        public decimal TamanioArchivo { get; set; }
    }

    public class EDocumentosEmpleado
    {
        public int IdHomEmpleado { get; set; }
        public int IdCtrHomEmpleado { get; set; }

        public int IdCliente { get; set; }
        public int IdClienteProyecto { get; set; }
        public int IdProyecto { get; set; }
        public int IdEmpleado { get; set; }
        public int IdMatriz { get; set; }
        public string Matriz { get; set; }
        public string CodDocumento { get; set; }
        public int IdDocumentoName { get; set; }
        public string DocumentoName { get; set; }
        public string TipoExtension { get; set; }
        public bool Aplica { get; set; }
        public int IdPeriodicidad { get; set; }
        public string Periodicidad { get; set; }
        public int IdCriticidad { get; set; }
        public string Criticidad { get; set; }
        public bool IdRegimenGeneral { get; set; }
        public bool IdRegimenMype { get; set; }
        public bool IdRegimenAgrario { get; set; }
        public bool IdConstruccionCivil { get; set; }

        public int IdRegimenGeneralParameter { get; set; }
        public int IdRegimenMypeParameter { get; set; }
        public int IdRegimenAgrarioParameter { get; set; }
        public int IdConstruccionCivilParameter { get; set; }

        public string Regimen { get; set; }
        public bool Estado { get; set; }
        public bool Upload { get; set; }
        public int IdEstado { get; set; }
        public string Archivo { get; set; }
        public string Observacion { get; set; }
        public decimal TamanioArchivo { get; set; }
    }

}
