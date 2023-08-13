using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class ETablaMaestra
    {
        public int IdTabla { get; set; }
        public int IdColumna { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
    }
}
