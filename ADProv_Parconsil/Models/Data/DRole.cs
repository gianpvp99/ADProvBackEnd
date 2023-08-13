using ADProv_Parconsil.Interface;
using ADProv_Parconsil.Models.Parameters;
using ADProv_Parconsil.Models.Result;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Tsp;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing.Printing;
using System.IO;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Data
{
    public class DRole : IRole
    {
        private IConfiguration _configuration;
        public DRole(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> addRole(PRole role)
        {
            using (SqlConnection cn=new SqlConnection(_configuration.GetConnectionString("db"))) 
            {
                using(SqlCommand cmd=new SqlCommand("sp_rol_add", cn))
                {
                    int response = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre",role.nombre);
                    cmd.Parameters.AddWithValue("@idTipoTablaUsuario", role.idTipoTablaUsuario);
                    cmd.Parameters.AddWithValue("@user", role.user);
                    cn.Open();
                    response= cmd.ExecuteNonQuery();
                    cn.Close();
                    return response;
                    
                }
            }
        }

        public async Task<int> addRoleAccess(PRoleAcces roleAcces)
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("sp_seguridad_rol_accesos_add", cn))
                {

                    int validateRoleAccess = search_RoleAccess_idacces_role(roleAcces.idAccess, roleAcces.idRol);

                    if (validateRoleAccess > 0)
                    {
                        return 2; //El Role Access ya existe
                    }
                    else
                    {
                        int response = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idAcceso", roleAcces.idAccess);
                        cmd.Parameters.AddWithValue("@idRol", roleAcces.idRol);
                        cmd.Parameters.AddWithValue("@user", roleAcces.user);
                        cn.Open();
                        response = cmd.ExecuteNonQuery();
                        cn.Close();
                        return response;
                    }

                   

                }
            }
        }

        public async Task<int> deleteAccessRole(int idAccessRole)
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("sp_seguridad_rol_accesos_delete", cn))
                {
                    int response = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idAccessRole);
                    cn.Open();
                    response = cmd.ExecuteNonQuery();
                    cn.Close();
                    return response;
                }
            }
        }

        public async Task<List<EAccess>> getAccess(int idTipoUser)
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_seguridad_acceso_list", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tabla_tipo_user", idTipoUser);
                  

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<EAccess> listAccess = new List<EAccess>();
                        while (reader.Read())
                        {
                            EAccess Access = new EAccess();
                            Access.idTablaTipoUser = reader.GetInt32("idTablaTipoUser");
                            Access.nombreTipoUser = reader.GetString("nombreTipoUser");
                            Access.idModulo = reader.GetInt32("idModulo");
                            Access.nombreModulo = reader.GetString("nombreModulo");
                            Access.uriModulo = reader.GetString("uriModulo");
                            Access.idAcceso = reader.GetInt32("idAcceso");
                            Access.nombreAcceso = reader.GetString("nombreAcceso");
                            Access.uriAcceso = reader.GetString("uriAcceso");

                            listAccess.Add(Access);
                        }
                        cn.Close();
                        return listAccess;
                    }
                }
            }
        }

        public async Task<List<ERole>> getRole(int option, string search, int page, int pageSize)
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                cn.Open ();
                using (SqlCommand cmd = new SqlCommand("sp_rol_list", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@option", option);
                    cmd.Parameters.AddWithValue("@searchString", search);
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);

                    using (SqlDataReader reader = cmd.ExecuteReader()) { 
                        List<ERole> listRole = new List<ERole>();
                        while (reader.Read())
                        {
                            ERole role = new ERole();
                            role.idRol = reader.GetInt32("idRol");
                            role.nombreRol = reader.GetString("nombreRol");
                            role.stateRol = reader.GetBoolean("stateRol");
                            role.idTipoTabla = reader.GetInt32("idTipoTabla");
                            role.nombreTipoTabla = reader.GetString("nombreTipoTabla");

                            listRole.Add(role);
                        }
                        cn.Close();
                        return listRole;
                    }
                }
            }
        }
        bool isLastIteration = false;
        public async Task<List<ERoleAccess>> getRoleAccesos(int roleId)
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_seguridad_rol_accesos_list", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@rol", roleId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        int moduloId = 0;
                        List<ERoleAccess> listRoleAccess = new List<ERoleAccess>();

                        ERoleAccess roleAccess = new ERoleAccess();
                        while (reader.Read())
                        {

                            if (reader.GetInt32("moduloId") == moduloId)
                            {

                                childMenu childMenu = new childMenu();

                                //childMenu.AccesoId = reader.GetInt32("AccesoId");
                                childMenu.id = reader.GetString("AccesoIdNombre");
                                childMenu.title = reader.GetString("AccesoTitle");
                                childMenu.type = reader.GetString("AccesoType");
                                childMenu.icon = reader.GetString("AccesoIcon");
                                childMenu.disabled = reader.GetBoolean("AccesoDisabled");
                                childMenu.hidden = reader.GetBoolean("Accesohidden");
                                childMenu.url = reader.GetString("AccesoUrl");
                                roleAccess.children.Add(childMenu);
                            }
                            else
                            {
                                roleAccess =new ERoleAccess();
                                //roleAccess.idTipoUser = reader.GetInt32("idTipoUser");
                                //roleAccess.nombreTipoUser = reader.GetString("nombreTipoUser");
                                //roleAccess.idRolAcceso = reader.GetInt32("idRolAcceso");
                                //roleAccess.idRol = reader.GetInt32("idRol");
                                //roleAccess.rolNombre = reader.GetString("rolNombre");

                                //roleAccess.moduloId = reader.GetInt32("moduloId");
                                roleAccess.id = reader.GetString("moduloIdNombre");
                                roleAccess.title = reader.GetString("moduloTitle");
                                roleAccess.type = reader.GetString("moduloType");
                                roleAccess.icon = reader.GetString("moduloIcon");
                                roleAccess.disabled = reader.GetBoolean("moduloDisabled");
                                roleAccess.hidden = reader.GetBoolean("modulohidden");
                                roleAccess.url = reader.GetString("moduloUrl");

                                roleAccess.children = new List<childMenu>()
                                {
                                    new childMenu()
                                    {
                                        //AccesoId = reader.GetInt32("AccesoId"),
                                        id = reader.GetString("AccesoIdNombre"),
                                        title = reader.GetString("AccesoTitle"),
                                        type = reader.GetString("AccesoType"),
                                        icon = reader.GetString("AccesoIcon"),
                                        disabled = reader.GetBoolean("AccesoDisabled"),
                                        hidden = reader.GetBoolean("Accesohidden"),
                                        url = reader.GetString("AccesoUrl"),

                                    }
                                };
                                moduloId = (reader.GetInt32("moduloId"));
                                listRoleAccess.Add(roleAccess);
                            }
                            
                        }
                        cn.Close();
                        return listRoleAccess;
                    }
                }
            }
        }

        public async Task<List<ERole>> getRoleForTypeUser(int idTyeUser)
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_seguridad_rol_tipoTablaList", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idTipoTabla", idTyeUser);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<ERole> listRole = new List<ERole>();
                        while (reader.Read())
                        {
                            ERole role = new ERole();
                            role.idRol = reader.GetInt32("idRol");
                            role.nombreRol = reader.GetString("nombreRol");
                            role.stateRol = reader.GetBoolean("stateRol");
                            role.idTipoTabla = reader.GetInt32("idTipoTabla");
                            role.nombreTipoTabla = reader.GetString("nombreTipoTabla");

                            listRole.Add(role);
                        }
                        cn.Close();
                        return listRole;
                    }
                }
            }

        }
            public async Task<List<ETipoUser>> getTipoUser()
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_tabla_tipo_user_list", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<ETipoUser> listTipoUser = new List<ETipoUser>();
                        while (reader.Read())
                        {
                            ETipoUser tipoUser = new ETipoUser();
                            tipoUser.id = reader.GetInt32("id");
                            tipoUser.nombre = reader.GetString("nombre");
                            tipoUser.state = reader.GetBoolean("state");

                            listTipoUser.Add(tipoUser);
                        }
                        cn.Close();
                        return listTipoUser;
                    }
                }
            }
        }

        public async Task<int> UpdateLogDeleteRole(int idRole, int idUser)
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("sp_rol_update_logDelete", cn))
                {
                    int response = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idRole);
                    cmd.Parameters.AddWithValue("@user", idUser);
                    cn.Open();
                    response = cmd.ExecuteNonQuery();
                    cn.Close();
                    return response;

                }
            }
        }

        public async Task<int> updateRole(PRole role)
        {
            using (SqlConnection cn=new SqlConnection(_configuration.GetConnectionString("db")))
            {
                
                using (SqlCommand cmd = new SqlCommand("sp_rol_update", cn))
                {
                    int response = 0;
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", role.id);
                    cmd.Parameters.AddWithValue("@nombre", role.nombre);
                    cmd.Parameters.AddWithValue("@user", role.user);
                    cn.Open();
                    response =cmd.ExecuteNonQuery();
                    cn.Close();
                    return response;
                    
                }
            }
        }

        public async Task<int> UpdateStateRole(int idRole, int idUser)
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("sp_rol_update_state", cn))
                {
                    int response = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idRole);
                    cmd.Parameters.AddWithValue("@user", idUser);
                    cn.Open();
                    response = cmd.ExecuteNonQuery();
                    cn.Close();
                    return response;

                }
            }
        }

        public async Task<List<childMenu>> getRoleAccesosList(int roleId)
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_seguridad_rol_accesos_list", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@rol", roleId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int moduloId = 0;
                        List<childMenu> listAcces = new List<childMenu>();
                        while (reader.Read())
                        {
                            childMenu access = new childMenu();
                            //access.id = reader.GetInt32("AccesoId");
                            access.id = reader.GetString("AccesoIdNombre");
                            access.title = reader.GetString("AccesoTitle");
                            access.type = reader.GetString("AccesoType");
                            access.icon = reader.GetString("AccesoIcon");
                            access.disabled = reader.GetBoolean("AccesoDisabled");
                            access.hidden = reader.GetBoolean("Accesohidden");
                            access.url = reader.GetString("AccesoUrl");
                            listAcces.Add(access);
                        }
                        cn.Close();
                        return listAcces;
                    }
                }
            }
        }

        public async Task<List<ListAccesoForRol>> getRoleAccesosListModal(int roleId)
        {
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_seguridad_rol_accesos_list", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@rol", roleId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int moduloId = 0;
                        List<ListAccesoForRol> listAcces = new List<ListAccesoForRol>();
                        while (reader.Read())
                        {
                            ListAccesoForRol access = new ListAccesoForRol();
                            //access.id = reader.GetInt32("AccesoId");
                            access.id = reader.GetInt32("idRolAcceso");
                            access.title = reader.GetString("AccesoIdNombre");
                           
                            listAcces.Add(access);
                        }
                        cn.Close();
                        return listAcces;
                    }
                }
            }
        }

        public int search_RoleAccess_idacces_role(int idAccess, int IdRole)
        {
            int cont = 0;
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("db")))
            {
                using (SqlCommand cmd = new SqlCommand("search_RoleAccess_idacces_role", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idAccess", idAccess);
                    cmd.Parameters.AddWithValue("@idRole", IdRole);
                    EMessage response = new EMessage();

                    sql.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        response = new EMessage();
                        if (reader.Read())
                        {
                            cont = reader.GetInt32("cant");
                        }
                    }

                    return cont;
                }
            }
        }

    }
}
