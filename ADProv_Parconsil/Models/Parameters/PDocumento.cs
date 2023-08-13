using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Parameters
{
    public class PDocumento
    {
        public int IdDocumento { get; set; }
        public int IdMatriz { get; set; }
        public string NombreDocumento { get; set; }
        public string TipoExtension { get; set; }
        public bool HomEmpresa { get; set; }
        public bool HomTrabajador { get; set; }
        public bool Empresa { get; set; }
        public bool Trabajador { get; set; }
        public int IdUsuarioAud { get; set; }
    }

    public class PDocumentosXCliente
    {
        public int IdHomologacion { get; set; }
        public int IdClienteGrupo { get; set; }
        public int IdCliente { get; set; }
        public int Puntaje { get; set; }
        public int SumaPuntaje { get; set; }
        public int IdMatriz { get; set; }
        public int IdDocumentoName { get; set; }
        public int IdCriticidad { get; set; }
        public int IdRegimen { get; set; }
        public int Estado { get; set; }
    }

    public class PNewDocCliente
    {
        public int IdHomologacion { get; set; }
        public int IdHomEmpresaCabecera { get; set; }
        public int IdCliente { get; set; }
        public int IdClienteGrupo { get; set; }
        public int IdCriticidad { get; set; }        
        public int IdDocumento { get; set; }
        public int IdMatriz { get; set; }
        public string NombreDocumento { get; set; }
        public string TipoExtension { get; set; }
        public bool HomEmpresa { get; set; }
        public bool HomTrabajador { get; set; }
        public bool Empresa { get; set; }
        public bool Trabajador { get; set; }
        public int IdUsuarioAud { get; set; }
    }   
}
