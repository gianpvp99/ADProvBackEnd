using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Parameters
{
    public class PUsuario
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }                
        public int IdEstado { get; set; }
        public int IdUsuarioAud { get; set; }

        public int IdTipoTablaUsuario { get; set; }
        public int IdRol { get; set; }
        public int idUsertablausuario { get; set; }

    }
}
