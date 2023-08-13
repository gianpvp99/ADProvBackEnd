using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class EPuntajeFinal
    {        
        public string Maximo { get; set; }
        public int PuntajeMinimo { get; set; }
        public string RazonSocial { get; set; }
        public string RucContratista { get; set; }
        public string RsContratista { get; set; }

        public string FechaVencimiento { get; set; }
        public int Generales { get; set; }
        public int SeguySalud { get; set; }
        public int Laborales { get; set; }
        public int Ambiental { get; set; }
        public int Compliance { get; set; }
        public int Responsabilidad { get; set; }
        public int Financiera { get; set; }
        public int Comercial { get; set; }
        public int Calidad { get; set; }
        public int Otros { get; set; }
    }
}
