using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ADProv_Parconsil.Models.Result;
using System.Data;

namespace ADProv_Parconsil.Models.Parameters
{
    public class PSustentoHomologacion
    {
        public int IdCliente { get; set; }
        public int IdContratista { get; set; }
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public int IdRubro { get; set; }
        public string PaginaWeb { get; set; }
        public string FechaConstitucion { get; set; }
        public string Telefono { get; set; }

        public string PersonaNombres { get; set; }
        public int PersonaIdTipoDoc { get; set; }
        public string PersonaDoc { get; set; }
        public string PersonaNacyRes { get; set; }
        public string PersonaDom { get; set; }
        public string PersonaPuesto { get; set; }
        public string PersonaTelefono { get; set; }
        public string PersonaCorreo { get; set; }

        public int IdUsuarioAud { get; set; }

        public List<EContratistaDocumento> ContratistaDocumento { get; set; }
        [JsonIgnore]

        public DataTable TContratistaDocumento { get; set; }
    }

    public class PSustentoHomEmpleado
    {
        public int IdCliente { get; set; }
        public int IdContratista { get; set; }
        public string RazonSocial { get; set; }
        public int IdEmpleado { get; set; }
        public string Empleado { get; set; }
        public int IdUsuarioAud { get; set; }

        public List<EEmpleadoDocumento> EmpleadoDocumento { get; set; }
        [JsonIgnore]

        public DataTable TEmpleadoDocumento { get; set; }
    }

    public class EEmpleadoDocumento
    {
        public int IdCtrHomEmpleado { get; set; }
        public int IdHomologacion { get; set; }
        public string NombreDocumento { get; set; }
        public string Archivo { get; set; }
        public string Base64 { get; set; }
        public decimal TamanioArchivo { get; set; }
        public string TipoExtension { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public string DriveId { get; set; }
    }

    public class Carpeta
    {
        public int IdCliente { get; set; }
        public string RazonSocial { get; set; }

        public int IdEmpleado { get; set; }
        public string Empleado { get; set;}
    }
}
