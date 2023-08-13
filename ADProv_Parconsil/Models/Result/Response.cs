using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class Response
    {
        public bool Ok { get; set; } = false;
        public string Message { get; set; } = "";
        public dynamic Data { get; set; }
    }
}
