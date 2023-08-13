using System;

namespace ADProv_Parconsil.Models.Parameters
{
    public class PRole
    {
          public int id { get; set; }
          public string nombre { get; set; }
          public int idTipoTablaUsuario { get; set; }
          public int user { get; set; }
        
    }
    public class PRoleAcces
    {
        public int id { get; set; }
        public int idAccess { get; set; }
        public int idRol { get; set; }
        public int user { get; set; }
    }
}
