using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class EContratistaDocumento
    {
        public int IdCtrHomologacion { get; set; }
        public int IdHomologacion { get; set; }
        public int IdCliente { get; set; }
        public int IdContratista { get; set; }
        public int IdDocumentoName { get; set; }
        public string NombreDocumento { get; set; }
        public int SumaPuntaje { get; set; }
        public string Base64 { get; set; }
        public string Archivo { get; set; }
        public decimal TamanioArchivo { get; set; }
        public string TipoExtension { get; set; }
        public string RutaArchivo { get; set; }
        public string Observacion { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public int DocumentosTotales { get; set; }
        public int DocumentosSubidos { get; set; }
        public int Porcentaje { get; set; }

        public string DriveId { get; set; }
        public int TotalElements { get; set; }
    }

    public class GruposContratista
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int IdContratista { get; set; }
        public int IdCliente { get; set; }
        public int IdClienteGrupo { get; set; }
        public string NombreGrupo { get; set; }        
        public bool FlagEliminado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaModificacion { get; set; }
        public int TotalElements { get; set; }
    }

    public class GruposProyecto
    {
        public int IdProyectoContratista { get; set; }
        public int IdContratista { get; set; }
        public int IdCliente { get; set; }
        public int IdProyecto { get; set; }
        public string Proyecto { get; set; }        
        public string Responsable { get; set; }        
    }
}
