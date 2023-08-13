using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;

namespace ADProv_Parconsil.Models.Result
{
    public class ERole
    {
        public int idRol { get; set; }
        public string nombreRol { get; set; }
        public bool stateRol { get; set; }
        public int idTipoTabla { get; set; }
        public string nombreTipoTabla { get; set; }
    }

    public class ListAccesoForRol
    {
        //public int AccesoId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
    }

        public class childMenu
    {
        //public int AccesoId { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
        public bool hidden { get; set; }
        public string url { get; set; }
    }
    public class ERoleAccess
    {
        public ERoleAccess() {
            this.children = new List<childMenu>();
        }
        //public int idTipoUser { get; set; }
        //public string nombreTipoUser { get; set; }
        //public int idRolAcceso { get; set; }
        //public int idRol { get; set; }
        //public string rolNombre { get; set; }

        //public int moduloId { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
        public bool disabled { get; set; }
        public bool hidden { get; set; }
        public string url { get; set; }

        public List<childMenu> children { get; set; }
    }

    public class ETipoUser
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public bool state { get; set; }
    }

    public class EAccess
    {
        public int idTablaTipoUser { get; set; }
        public string nombreTipoUser { get; set; }
        public int idModulo { get; set; }
        public string nombreModulo { get; set; }
        public string uriModulo { get; set; }
        public int idAcceso { get; set; }
        public string nombreAcceso { get; set; }
        public string uriAcceso { get; set; }

    }
    public class ERoleAccessList
    {
        public int rolId { get; set; }
        public string rolNombre { get; set; }
        public int accessId { get; set; }
        public string accessNombre { get; set; }
        public string accessUri { get; set; }
        public bool state { get; set; }
    }
}
