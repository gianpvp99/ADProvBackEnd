using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ADProv_Parconsil.Models.Result;

namespace ADProv_Parconsil.Models.Parameters
{
    public class PContratista
    {
        public int IdContratista { get; set; }        
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int IdRegimen { get; set; }
        public string IdUbigeo { get; set; }
        public int IdUsuarioAud { get; set; }
        
    }    
}
