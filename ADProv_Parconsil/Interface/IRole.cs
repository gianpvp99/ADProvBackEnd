using ADProv_Parconsil.Models.Parameters;
using ADProv_Parconsil.Models.Result;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Interface
{
    public interface IRole
    {

        public Task<List<ETipoUser>> getTipoUser();

        public Task<int> addRole(PRole role);
        public Task<int> updateRole(PRole role);
        public Task<List<ERole>> getRole(int option, string search, int page, int pageSize);

        public Task<int> UpdateStateRole(int idRole , int idUser);
        public Task<int> UpdateLogDeleteRole(int idRole, int idUser);

        public Task<int> addRoleAccess(PRoleAcces roleAccess);
        public Task<List<ERoleAccess>> getRoleAccesos(int roleId);
        public Task<List<childMenu>> getRoleAccesosList(int roleId);

        public Task<List<ListAccesoForRol>> getRoleAccesosListModal(int roleId);

        public Task<List<EAccess>> getAccess(int idTipoUser);
        public Task<int> deleteAccessRole(int idAccessRole);
        public Task<List<ERole>> getRoleForTypeUser(int idTyeUser);

    }
}
