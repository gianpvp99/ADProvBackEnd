using ADProv_Parconsil.Models.Parameters;
using ADProv_Parconsil.Models.Result;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Interface
{
    public interface IAsistencia
    {
        Task<int> Add(PAsistencia pAsistencia);
        Task<List<EAsistencia>> List(PAsistenciaList pAsistenciaList);
    }
}
