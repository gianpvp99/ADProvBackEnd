using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ADProv_Parconsil.Models.Result;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ADProv_Parconsil.Models.Parameters
{
    public class PContratistaCliente
    {
        public int IdContratista { get; set; }
        public int IdCliente { get; set; }
        public string ContratistaUser { get; set; }
        public string Clave { get; set; }
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaCierre { get; set; }
        public int IdUsuarioAud { get; set; }

        public string Caducidad { get; set; }
        public List<ContratistaCliente_Enlace> contratistaClienteEnlace { get; set; }

        [JsonIgnore]
        public DataTable TContratistaCliente_Enlace { get; set; }


        public class ContratistaCliente_Enlace
        {
            public int IdCtrCliente { get; set; }
            public int IdCliente { get; set; }
            public int IdContratista { get; set; }
            public int IdClienteGrupo { get; set; }
        }
    }    
}
