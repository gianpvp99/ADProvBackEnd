using System;

namespace ADProv_Parconsil.Models.Parameters
{
    public class PAsistencia
    {

        public int id_empleado { get; set; }
        public int tipo_registro { get; set; }
        public int id_user { get; set; }
        public string observacion { get; set; }

    }
    public class PAsistenciaList
    {

        public DateTime fechainicio { get; set; }
        public DateTime fechaFin { get; set; }
        public int idCliente { get; set; }
        public string text { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}
