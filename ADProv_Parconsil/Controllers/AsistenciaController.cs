using ADProv_Parconsil.Models.Data;
using ADProv_Parconsil.Models.Result;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ADProv_Parconsil.Models.Parameters;

namespace ADProv_Parconsil.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AsistenciaController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("Asistencia_add")]
        [HttpPost]

        public async Task<ActionResult<int>> Add([FromBody] PAsistencia pAsistencia)
        {
            try
            {
                return await new DAsistencia(_configuration).Add(pAsistencia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Asistencia_list")]
        [HttpPost]
        public async Task<ActionResult<List<EAsistencia>>> List([FromBody] PAsistenciaList pAsistenciaList)
        {
            try
            {
                return await new DAsistencia(_configuration).List(pAsistenciaList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
