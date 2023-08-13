using ClosedXML.Excel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADProv_Parconsil.Models.Data;
using ADProv_Parconsil.Models.Parameters;
using ADProv_Parconsil.Models.Result;

namespace ADProv_Parconsil.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/Security")]
    [ApiExplorerSettings(GroupName = "Security")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SecurityController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("login")]
        [HttpGet]
        public async Task<ActionResult<ESecurity>> Login(string Ruc, string Usuario, string Clave, int IdSignInType)
        {
            try
            {
                return await new DSecurity(_configuration).Login(new ESecurity
                {
                    Ruc = Ruc ?? "",
                    Usuario = Usuario ?? "",
                    Clave = Clave ?? "",
                    IdSignInType = IdSignInType
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Usuario_List")]
        [HttpGet]
        public async Task<ActionResult<List<ESecurity>>> Usuario_List(int IdEstado, string Search, int PageIndex, int PageSize)
        {
            try
            {
                return await new DSecurity(_configuration).Usuario_List(new ESecurity
                {
                    IdEstado = IdEstado,
                    Search = Search ?? "",
                    PageIndex = PageIndex,
                    PageSize = PageSize
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Usuario_InsertUpdate")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> Usuario_InsertUpdate([FromBody] PUsuario p_Params)
        {          
            try
            {
                return await new DSecurity(_configuration).Usuario_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        [Route("Usuario_Delete")]
        [HttpDelete]
        public async Task<ActionResult<EMessage>> Usuario_Delete(int IdUsuario, int IdUsuarioAud)
        {
            try
            {
                return await new DSecurity(_configuration).Usuario_Delete(IdUsuario, IdUsuarioAud);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Role_list")]
        [HttpGet]
        public async Task<ActionResult<ERole>> getRole(int option, string search, int page, int pageSize)
        {
            try
            {
                if (search == null)
                {
                    search = "";
                }
                return Ok(await new DRole(_configuration).getRole(option,search,page, pageSize));
            }
            catch (Exception)
            {

                return BadRequest();
            }
           
        }

        [Route("Access_list")]
        [HttpGet]
        public async Task<ActionResult<ERole>> getAccess(int idTipoUser)
        {
            try
            {
                return Ok(await new DRole(_configuration).getAccess(idTipoUser));
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [Route("Role_add")]
        [HttpPost]
        public async Task<ActionResult<ERole>> addRole([FromBody] PRole role)
        {
            try
            {
                return Ok(await new DRole(_configuration).addRole(role));
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [Route("Role_update")]
        [HttpPost]
        public async Task<ActionResult<ERole>> updateRole([FromBody] PRole role)
        {
            try
            {
                return Ok(await new DRole(_configuration).updateRole(role));
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [Route("Role_LogDeleteRole")]
        [HttpDelete]
        public async Task<ActionResult<ERole>> UpdateLogDeleteRole(int idRole, int iduser)
        {
            try
            {
                return Ok(await new DRole(_configuration).UpdateLogDeleteRole(idRole, iduser));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Route("Role_StateRole")]
        [HttpPost]
        public async Task<ActionResult<ERole>> UpdateStateRole(int idRole, int iduser)
        {
            try
            {
                return Ok(await new DRole(_configuration).UpdateStateRole(idRole, iduser));
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
        [Route("Role_aceess_list")]
        [HttpGet]
        public async Task<ActionResult<ERole>> getRoleAccesos(int role)
        {
            try
            {
                return Ok(await new DRole(_configuration).getRoleAccesos(role));
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
        [Route("Role_aceess_list_Modal")]
        [HttpGet]
        public async Task<ActionResult<ERole>> getRoleAccesosListModal(int role)
        {
            try
            {
                return Ok(await new DRole(_configuration).getRoleAccesosListModal(role));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [Route("Role_access")]
        [HttpGet]
        public async Task<ActionResult<ERole>> getRoleAccesosList(int role)
        {
            try
            {
                return Ok(await new DRole(_configuration).getRoleAccesosList(role));
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }


        [Route("RoleForTypeUSer_list")]
        [HttpGet]
        public async Task<ActionResult<ERole>> getRoleForTypeUser(int idTyeUser)
        {
            try
            {
                return Ok(await new DRole(_configuration).getRoleForTypeUser(idTyeUser));
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [Route("Role_aceess_add")]
        [HttpPost]
        public async Task<ActionResult<ERole>> addRoleAccesos(PRoleAcces  roleAccess)
        {
            try
            {
                return Ok(await new DRole(_configuration).addRoleAccess(roleAccess));
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [Route("Tipo_User_List")]
        [HttpGet]
        public async Task<ActionResult<ERole>> getTipoUser()
        {
            try
            {
                return Ok(await new DRole(_configuration).getTipoUser());
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
        [Route("AccessRole_delete")]
        [HttpDelete]
        public async Task<ActionResult<ERole>> deleteAccessRole(int idAccessRole)
        {
            try
            {
                return Ok(await new DRole(_configuration).deleteAccessRole(idAccessRole));
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
    }
}
