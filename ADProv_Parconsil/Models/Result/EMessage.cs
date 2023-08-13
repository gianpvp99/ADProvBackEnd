using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class EMessage
    {
        public enum TipoRetorno2
        {
            Error = 0,
            Satisfactorio = 1,
            Validacion = 2
        }

        public TipoRetorno2 Tipo { get; set; }
        public int Ok { get; set; }
        public string Message { get; set; }
        public string Mensaje { get; set; }
    }
}
