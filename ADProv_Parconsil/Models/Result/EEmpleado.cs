using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class EEmpleado
    {
        public int IdCliente { get; set; }
        public int IdContratista { get; set; }
        public int IdEstado { get; set; }
        public string Search { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        
        public int IdEmpleado { get; set; }
        public int IdProyecto { get; set; }
        public string FechaInicioProyecto { get; set; }
        public int IdRegimen { get; set; }
        public string RcContratista { get; set; }
        public string RsContratista { get; set; }        
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FullName { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public int IdNacionalidad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string IdUbigeo { get; set; }
        public string Estado { get; set; }
        public int DocumentosPendientes { get; set; }
        public int DocumentosAceptados { get; set; }

        public int RegimenGeneral { get; set; }
        public int RegimenMype { get; set; }
        public int RegimenAgrario { get; set; }
        public int ConstruccionCivil { get; set; }

        public int PorcentajeRGeneral { get; set; }
        public int PorcentajeRMype { get; set; }
        public int PorcentajeRAgrario { get; set; }
        public int PorcentajeCCivil { get; set; }

        public int TotalElements { get; set; }

        public string cadenaQr { get; set; }
        
        public string foto { get; set; }
    }

    public class EEmpleadoD
    {
        public int IdEmpleado { get; set; }
        public int IdUsuarioAud { get; set; }
    }

    public class EEmpleadoDocuPresentados
    {
        public int IdCliente { get; set; }
        public int IdContratista { get; set; }
        public int IdEmpleado { get; set; }
        public int IdEstado { get; set; }
        public string Search { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
     

        public List<EEmpleadoDocumentoPresentado> EmpleadoDocumento { get; set; }
    }

    public class EEmpleadoDocumentoPresentado
    {
        public int IdCtrHomEmpleado { get; set; }
        public int IdHomologacion { get; set; }
        public int IdCliente { get; set; }
        public int IdContratista { get; set; }
        public int IdEmpleado { get; set; }
        public int IdDocumentoName { get; set; }
        public string NombreDocumento { get; set; }        
        public string Base64 { get; set; }
        public string Archivo { get; set; }
        public decimal TamanioArchivo { get; set; }
        public string TipoExtension { get; set; }
        public string RutaArchivo { get; set; }

        public int IdPeriodicidad { get; set; }
        public string Periodicidad { get; set; }
        public string FechaVencimiento { get; set; }
        public string DriveId { get; set; }
        public string Observacion { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public int DocumentosTotales { get; set; }
        public int DocumentosSubidos { get; set; }
        public int Porcentaje { get; set; }                
        public int TotalElements { get; set; }       
    }

    public class Qr
    {
        public string qr { get; set; }
    }

}
