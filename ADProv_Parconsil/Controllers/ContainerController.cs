using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;

using ClosedXML.Excel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADProv_Parconsil.Models.Data;
using ADProv_Parconsil.Models.Parameters;
using ADProv_Parconsil.Models.Result;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using MimeKit.Utils;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Threading;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Drawing.Imaging;
using System.Drawing;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Google.Apis.Util.Store;
using System.IO;


namespace ADProv_Parconsil.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/Container")]
    [ApiExplorerSettings(GroupName = "Home")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Googled";

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ContainerController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("TablaMaestra_Dropdown")]
        [HttpGet]
        public async Task<ActionResult<List<ETablaMaestra>>> TablaMaestra_Dropdown(int IdTabla)
        {
            try
            {
                return await new DTablaMaestra(_configuration).TablaMaestra_Dropdown(new ETablaMaestra
                {
                    IdTabla = IdTabla
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ClienteProyectoGrupo_List")]
        [HttpGet]
        public async Task<ActionResult<EClienteGrupoProyecto>> ClienteProyectoGrupo_List(int IdCliente, int PageIndex, int PageSize)
        {
            try
            {
                return await new DCliente(_configuration).ClienteProyectoGrupo_List(new EClienteGrupoProyecto
                {
                    IdCliente = IdCliente,
                    PageIndex = PageIndex,
                    PageSize = PageSize
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Cliente_List")]
        [HttpGet]
        public async Task<ActionResult<ECliente>> Cliente_List(int IdCliente, int IdEstado, string Search, int PageIndex, int PageSize)
        {
            try
            {
                return await new DCliente(_configuration).Cliente_List(new ECliente
                {
                    IdCliente = IdCliente,
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

        [Route("Cliente_Dropdown")]
        [HttpGet]
        public async Task<ActionResult<ECliente>> Cliente_Dropdown()
        {
            try
            {
                return await new DCliente(_configuration).Cliente_Dropdown();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Route("Cliente_Get")]
        //[HttpGet]
        //public async Task<ActionResult<List<ECliente>>> Cliente_Get(int IdEstado, string Search, int PageIndex, int PageSize)
        //{
        //    try
        //    {
        //        return await new DCliente(_configuration).Cliente_List(new ECliente
        //        {
        //            IdEstado = IdEstado,
        //            Search = Search ?? "",
        //            PageIndex = PageIndex,
        //            PageSize = PageSize
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [Route("Cliente_InsertUpdate")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> Cliente_InsertUpdate([FromBody] PCliente p_Params)
        {
            try
            {
                DataTable ClienteProyecto = new DataTable();
                ClienteProyecto.Columns.Add("IdFila", typeof(int));
                ClienteProyecto.Columns.Add("IdProyecto", typeof(int));
                ClienteProyecto.Columns.Add("Proyecto", typeof(string));
                ClienteProyecto.Columns.Add("Responsable", typeof(string));

                p_Params.TablaClienteProyecto.ForEach(x =>
                {
                    ClienteProyecto.Rows.Add(
                        x.IdFila, x.IdProyecto, x.Proyecto, x.Responsable
                   );
                });
                p_Params.TClienteProyecto = ClienteProyecto;

                DataTable ClienteGrupo = new DataTable();
                ClienteGrupo.Columns.Add("IdFila", typeof(int));
                ClienteGrupo.Columns.Add("IdClienteGrupo", typeof(int));
                ClienteGrupo.Columns.Add("NombreGrupo", typeof(string));
                ClienteGrupo.Columns.Add("FlagEliminado", typeof(bool));

                p_Params.TablaClienteGrupo.ForEach(x =>
                {
                    ClienteGrupo.Rows.Add(
                        x.IdFila, x.IdClienteGrupo, x.NombreGrupo, x.FlagEliminado
                   );
                });

                p_Params.TClienteGrupos = ClienteGrupo;

                return await new DCliente(_configuration).Cliente_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ContratistaDocumentos_Insert")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> ContratistaDocumentos_Insert([FromBody] PClienteDocumento p_Params)
        {
            try
            {
                DataTable HomologacionE = new DataTable();
                HomologacionE.Columns.Add("IdHomologacion");
                HomologacionE.Columns.Add("IdCliente");
                HomologacionE.Columns.Add("SumaPuntaje");
                HomologacionE.Columns.Add("IdMatriz");
                HomologacionE.Columns.Add("IdDocumentoName");
                HomologacionE.Columns.Add("IdCriticidad");
                HomologacionE.Columns.Add("IdRegimen");
                HomologacionE.Columns.Add("Estado", typeof(bool));

                p_Params.TDocumentoHomologacionE.ForEach(x =>
                {
                    HomologacionE.Rows.Add(x.IdHomologacion, x.IdCliente, x.SumaPuntaje,
                        x.IdMatriz, x.IdDocumentoName, x.IdCriticidad, x.IdRegimen, x.Estado);
                });

                p_Params.THomologacionE = HomologacionE;

                DataTable HomEmpresaCabecera = new DataTable();
                HomEmpresaCabecera.Columns.Add("IdHomEmpresaCabecera");
                HomEmpresaCabecera.Columns.Add("IdCliente");
                HomEmpresaCabecera.Columns.Add("Puntaje");
                HomEmpresaCabecera.Columns.Add("IdMatriz");
                HomEmpresaCabecera.Columns.Add("IdDocumentoName");

                p_Params.TDocumentoHomEmpresaCabecera.ForEach(x =>
                {
                    HomEmpresaCabecera.Rows.Add(x.IdHomEmpresaCabecera, x.IdCliente, x.Puntaje,
                        x.IdMatriz, x.IdDocumentoName);
                });

                p_Params.THomEmpresaCabecera = HomEmpresaCabecera;

                return await new DCliente(_configuration).ContratistaDocumentos_Insert(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EmpleadosDocumentos_Insert")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> EmpleadosDocumentos_Insert([FromBody] PClienteDocumento p_Params)
        {
            try
            {
                DataTable HomEmpleado = new DataTable();
                HomEmpleado.Columns.Add("IdHomEmpleado");
                HomEmpleado.Columns.Add("IdCliente");
                HomEmpleado.Columns.Add("IdDocumentoName");
                HomEmpleado.Columns.Add("IdPeriodicidad");
                HomEmpleado.Columns.Add("IdCriticidad");
                HomEmpleado.Columns.Add("IdRegimenGeneral", typeof(bool));
                HomEmpleado.Columns.Add("IdRegimenMype", typeof(bool));
                HomEmpleado.Columns.Add("IdRegimenAgrario", typeof(bool));
                HomEmpleado.Columns.Add("IdConstruccionCivil", typeof(bool));
                HomEmpleado.Columns.Add("Estado", typeof(bool));

                p_Params.TDocumentoHomEmpresa.ForEach(x =>
                {
                    HomEmpleado.Rows.Add(x.IdHomEmpleado, x.IdCliente, x.IdDocumentoName, x.IdPeriodicidad, x.IdCriticidad,
                        x.IdRegimenGeneral, x.IdRegimenMype, x.IdRegimenAgrario, x.IdConstruccionCivil, x.Estado);
                });

                p_Params.THomEmpleado = HomEmpleado;

                DataTable Empresa = new DataTable();
                Empresa.Columns.Add("IdDocEmpresa");
                Empresa.Columns.Add("IdCliente");
                Empresa.Columns.Add("IdDocumentoName");
                Empresa.Columns.Add("Estado", typeof(bool));

                p_Params.TDocumentoEmpresa.ForEach(z =>
                {
                    Empresa.Rows.Add(z.IdDocEmpresa, z.IdCliente, z.IdDocumentoName, z.Estado);
                });

                p_Params.TEmpresa = Empresa;

                DataTable Empleado = new DataTable();
                Empleado.Columns.Add("IdGeneral");
                Empleado.Columns.Add("IdCliente");
                Empleado.Columns.Add("IdDocumentoName");
                Empleado.Columns.Add("Estado", typeof(bool));

                p_Params.TDocumentoEmpleado.ForEach(z =>
                {
                    Empleado.Rows.Add(z.IdDocEmpleado, z.IdCliente, z.IdDocumentoName, z.Estado);
                });

                p_Params.TEmpleado = Empleado;

                return await new DCliente(_configuration).EmpleadosDocumentos_Insert(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Cliente_Delete")]
        [HttpDelete]
        public async Task<ActionResult<EMessage>> Cliente_Delete(int IdCliente, int IdUsuarioAud)
        {
            try
            {
                return await new DCliente(_configuration).Cliente_Delete(IdCliente, IdUsuarioAud);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ContratistaClienteHom_Delete")]
        [HttpDelete]
        public async Task<ActionResult<EMessage>> ContratistaClienteHom_Delete(int IdCtrHomologacion, int IdCliente, int IdContratista, int IdEstado, string Observacion, int IdUsuarioAud)
        {
            try
            {
                return await new DCliente(_configuration).ContratistaClienteHom_Delete(IdCtrHomologacion, IdCliente, IdContratista, IdEstado, Observacion ?? "", IdUsuarioAud);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EmpleadoClienteHom_Delete")]
        [HttpDelete]
        public async Task<ActionResult<EMessage>> EmpleadoClienteHom_Delete(int IdCtrHomEmpleado, int IdCliente, int IdContratista, int IdEstado, string Observacion, int IdUsuarioAud)
        {
            try
            {
                return await new DCliente(_configuration).EmpleadoClienteHom_Delete(IdCtrHomEmpleado, IdCliente, IdContratista, IdEstado, Observacion ?? "", IdUsuarioAud);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ContratistaCliente_List")]
        [HttpGet]
        public async Task<ActionResult<EContratista>> ContratistaCliente_List(int IdCliente, int IdContratista, int IdEstado, string Search, int PageIndex, int PageSize)
        {
            try
            {
                return await new DContratista(_configuration).ContratistaCliente_List(new EContratista
                {
                    IdCliente = IdCliente,
                    IdContratista = IdContratista,
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

        [Route("EmpresaDocPresentados_List")]
        [HttpGet]
        public async Task<ActionResult<EContratista>> EmpresaDocPresentados_List(int IdCliente, int IdContratista, int IdEstado,
            string Search, int PageIndex, int PageSize, string nombreContratista, string fecha)
        {
            try
            {
                var res = await new DContratista(_configuration).EmpresaDocPresentados_List(new EContratista
                {
                    IdCliente = IdCliente,
                    IdContratista = IdContratista,
                    IdEstado = IdEstado,
                    Search = Search ?? "",
                    PageIndex = PageIndex,
                    PageSize = PageSize
                });

                res.ContratistaDocumento.ForEach(sus =>
                {
                    sus.RutaArchivo = $"{_configuration.GetSection("BaseUrl").Value}/Public/Homologacion/{sus.IdCliente}/{sus.IdContratista}/{sus.Archivo}";
                });

                return Ok(res.ContratistaDocumento.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Empleado_DocPresentados_List")]
        [HttpGet]
        public async Task<ActionResult<EEmpleadoDocuPresentados>> Empleado_DocPresentados_List(int IdCliente, int IdContratista, int IdEmpleado, int IdEstado, string Search, int PageIndex, int PageSize)
        {
            try
            {
                var res = await new DEmpleado(_configuration).Empleado_DocPresentados_List(new EEmpleadoDocuPresentados
                {
                    IdCliente = IdCliente,
                    IdContratista = IdContratista,
                    IdEmpleado = IdEmpleado,
                    IdEstado = IdEstado,
                    Search = Search ?? "",
                    PageIndex = PageIndex,
                    PageSize = PageSize
                });

                res.EmpleadoDocumento.ForEach(sus =>
                {
                    sus.RutaArchivo = $"{_configuration.GetSection("BaseUrl").Value}/public/homologacion/{sus.IdCliente}/{sus.IdContratista}/{sus.IdEmpleado}/{sus.Archivo}";
                });

                return Ok(res.EmpleadoDocumento.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [Route("generate_cadena_qr")]
        [HttpPost]
        public async Task<ActionResult<string>> generate_cadena_qr([FromBody] PClienteImagenQr pClienteImagenQr)
        {
            try
            {
                //string nombreCarpeta = documentEmpleado;
                //string rutaBaseProyecto = AppDomain.CurrentDomain.BaseDirectory+"/perfiles";
                //string rutaNuevaCarpeta = Path.Combine(rutaBaseProyecto, nombreCarpeta);

                // Verificar si la carpeta no existe antes de crearla
                //if (!Directory.Exists(rutaNuevaCarpeta))
                //{
                //    Directory.CreateDirectory(rutaNuevaCarpeta);
                //    Console.WriteLine("Carpeta creada exitosamente.");

                //}
                //else
                //{
                //    Console.WriteLine("La carpeta ya existe.");
                //}

                var res = await new DEmpleado(_configuration).generate_cadena_qr(pClienteImagenQr);


                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("generateQr")]
        [HttpGet]
        public async Task<ActionResult<Qr>> generateQr(string textQr)
        {
            try
            {
                QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                QrCode qrCode = new QrCode();
                qrEncoder.TryEncode(textQr.Trim(), out qrCode);
                GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero));

                using (Bitmap qrBitmap = new Bitmap(400, 400, PixelFormat.Format32bppArgb))
                {
                    using (Graphics graphics = Graphics.FromImage(qrBitmap))
                    {
                        renderer.Draw(graphics, qrCode.Matrix);
                    }

                    using (MemoryStream ms = new System.IO.MemoryStream())
                    {
                        qrBitmap.Save(ms, ImageFormat.Png);
                        byte[] qrBytes = ms.ToArray();
                        string base64Image = Convert.ToBase64String(qrBytes);

                        Qr qr = new Qr();
                        qr.qr = base64Image;


                        return Ok(qr);
                       // return "hola";
                    }
                }


               //return Ok(Convert.ToBase64String(qrBytes));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("Documento_List")]
        [HttpGet]
        public async Task<ActionResult<List<EDocumento>>> Documento_List(int IdEstado, string Search, int PageIndex, int PageSize)
        {
            try
            {
                return await new DCliente(_configuration).Documento_List(new EDocumento
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

        [Route("Contratista_List")]
        [HttpGet]
        public async Task<ActionResult<List<EContratistaCliente>>> Contratista_List(int IdContratista, int IdEstado, string Search, int PageIndex, int PageSize)
        {
            try
            {
                return await new DContratista(_configuration).Contratista_List(new EContratista
                {
                    IdContratista = IdContratista,
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

        [Route("Contratista_Dropdown")]
        [HttpGet]
        public async Task<ActionResult<List<EContratistaCliente>>> Contratista_Dropdown(int IdEstado, string Search, int PageIndex, int PageSize)
        {
            try
            {
                return await new DContratista(_configuration).Contratista_Dropdown(new EContratista
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

        [Route("ContratistaSustento_InsertUpdate")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> ContratistaSustento_InsertUpdate([FromBody] PSustentoHomologacion p_Params)
        {
            try
            {
                DriveService service = GetService();
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.Fields = "nextPageToken, files(*)";
                IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

                DataTable sustento = new DataTable();
                sustento.Columns.Add("IdCtrHomologacion");
                sustento.Columns.Add("IdHomologacion");
                sustento.Columns.Add("NombreDocumento");
                sustento.Columns.Add("Archivo");
                sustento.Columns.Add("TamanioArchivo");
                sustento.Columns.Add("TipoExtension");
                sustento.Columns.Add("IdEstado");
                sustento.Columns.Add("DriveId");

                p_Params.ContratistaDocumento.ForEach(x =>
                {
                    var delete = Path.Combine(_webHostEnvironment.ContentRootPath, $"Public/Homologacion/{p_Params.IdCliente}/{p_Params.IdContratista}");
                    var ruta = Path.Combine(_webHostEnvironment.ContentRootPath, $"Public/Homologacion/{p_Params.IdCliente}/{p_Params.IdContratista}/{x.IdHomologacion}");
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    if (!(x.Base64 ?? "").Equals(""))
                        System.IO.File.WriteAllBytes(Path.Combine(ruta, x.Archivo), Convert.FromBase64String(x.Base64.Split(',')[1]));

                    //files != null &&
                    if (files.Count > 0)
                    {
                        string folder = files.Where(x => x.Name == "CL" + p_Params.IdCliente + "-" + p_Params.RazonSocial && x.MimeType == "application/vnd.google-apps.folder" && x.Trashed == false).Select(z => z.Id).FirstOrDefault();
                        if (folder == null)
                        {
                            folder = CrearCarpeta("CL" + p_Params.IdCliente + "-" + p_Params.RazonSocial);
                            SubirArchivo(@"D:\RepoVC\PARCONSIL\PARCONSILBACKEDN\Parconsil_BackEnd\ADProv_Parconsil\Public\Pictures\Adprov.png", service, folder);
                            //SubirArchivo(@"C:\inetpub\wwwroot\sistema.adprov.app\adprov-Api\Public\Pictures\Adprov.png", service, folder);

                        }
                        string SubfolderId = files.Where(d => d.Name == "DOC" + p_Params.IdContratista + "-" + x.NombreDocumento && d.MimeType == "application/vnd.google-apps.folder" && d.Trashed == false).Select(z => z.Id).FirstOrDefault();
                        if (SubfolderId == null)
                        {
                            SubfolderId = CrearSubCarpeta(folder, "DOC" + p_Params.IdContratista + "-" + x.NombreDocumento);
                        }
                        //SubirArchivo($"{ruta}/{x.Archivo}", service, SubfolderId);

                        IList<string> ListFolders = new List<string>();
                        ListFolders.Add(SubfolderId);

                        var FileMetaData = new Google.Apis.Drive.v3.Data.File();
                        FileMetaData.Name = Path.GetFileName($"{ruta}/{x.Archivo}");
                        FileMetaData.MimeType = "application/octet-stream";
                        FileMetaData.Parents = ListFolders;

                        FilesResource.CreateMediaUpload request;
                        using (var stream = new System.IO.FileStream($"{ruta}/{x.Archivo}", System.IO.FileMode.Open))
                        {
                            request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                            request.Fields = "id";
                            request.Upload();

                            Google.Apis.Drive.v3.Data.File file = request.ResponseBody;
                            string fileId = file.Id;
                            x.DriveId = fileId;
                        }

                        sustento.Rows.Add(x.IdCtrHomologacion, x.IdHomologacion, x.NombreDocumento, x.Archivo, x.TamanioArchivo, x.TipoExtension, x.IdEstado, x.DriveId);

                        string fileExists = $"{delete}";
                        Directory.Delete(fileExists, true);

                        //string fileExists = $"{ruta}/{x.Archivo}";
                        //if (System.IO.File.Exists(fileExists))
                        //{
                        //    System.IO.File.Delete(fileExists);
                        //}
                    }
                });

                p_Params.TContratistaDocumento = sustento;

                return await new DContratista(_configuration).ContratistaSustento_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public static DriveService GetService()
        {
            UserCredential credential;

            using (var stream =
               new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Apps Script Execution API service.

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;

        }

        [Route("ObtenerArchivos")]
        [HttpGet]
        public async Task<ActionResult<List<EDrive>>> ObtenerArchivos()
        {
            DriveService service = GetService();
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 600;
            listRequest.Fields = "nextPageToken, files(id, name, size, version, createdTime, webViewLink, webContentLink, thumbnailLink)";

            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            List<EDrive> FileList = new List<EDrive>();

            try
            {
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        EDrive File = new EDrive
                        {
                            Id = file.Id,
                            Name = file.Name,
                            Size = file.Size,
                            Version = file.Version,
                            CreatedTime = file.CreatedTime,
                            WebViewLink = file.WebViewLink,
                            WebContentLink = file.WebContentLink,
                            ThumbnailLink = file.ThumbnailLink
                        };
                        FileList.Add(File);
                    }
                }
                return Ok(FileList.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EliminarArchivos")]
        [HttpDelete]
        public async Task<ActionResult<EMessage>> EliminarArchivos(string DriveId)
        {
            DriveService service = GetService();

            try
            {
                return Ok(service.Files.Delete(DriveId).Execute());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EliminarArchivosEmp")]
        [HttpDelete]
        public async Task<ActionResult<EMessage>> EliminarArchivosEmp(string DriveId)
        {
            DriveService service = GetService();

            try
            {
                return Ok(service.Files.Delete(DriveId).Execute());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("IniciarCarpetaUnica")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> IniciarCarpetaUnica([FromBody] Carpeta p_Params)
        {
            try
            {
                DriveService service = GetService();
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.Fields = "nextPageToken, files(*)";
                IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
                string folder = "";

                if (files.Count > 0)
                {
                    folder = files.Where(x => x.Name == "CL" + p_Params.IdCliente + "-" + p_Params.RazonSocial && x.MimeType == "application/vnd.google-apps.folder" && x.Trashed == false).Select(z => z.Id).FirstOrDefault();
                    if (folder == null)
                    {
                        folder = CrearCarpeta("CL" + p_Params.IdCliente + "-" + p_Params.RazonSocial);
                        SubirArchivo(@"D:\RepoVC\PARCONSIL\PARCONSILBACKEDN\Parconsil_BackEnd\ADProv_Parconsil\Public\Pictures\Adprov.png", service, folder);
                        //SubirArchivo(@"C:\inetpub\wwwroot\sistema.adprov.app\adprov-Api\Public\Pictures\Adprov.png", service, folder);
                    }
                }

                return Ok(folder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("IniciarCarpetaUnicaEmp")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> IniciarCarpetaUnicaEmp([FromBody] Carpeta p_Params)
        {
            try
            {
                DriveService service = GetService();
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.Fields = "nextPageToken, files(*)";
                IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

                string SubfolderId = "";

                if (files != null && files.Count > 0)
                {
                    string folder = files.Where(x => x.Name == "CL" + p_Params.IdCliente + "-" + p_Params.RazonSocial && x.MimeType == "application/vnd.google-apps.folder" && x.Trashed == false).Select(z => z.Id).FirstOrDefault();
                    if (folder == null)
                    {
                        folder = CrearCarpeta("CL" + p_Params.IdCliente + "-" + p_Params.RazonSocial);
                        SubirArchivo(@"D:\RepoVC\PARCONSIL\PARCONSILBACKEDN\Parconsil_BackEnd\ADProv_Parconsil\Public\Pictures\Adprov.png", service, folder);
                        //SubirArchivo(@"C:\inetpub\wwwroot\sistema.adprov.app\adprov-Api\Public\Pictures\Adprov.png", service, folder);
                    }
                    SubfolderId = files.Where(d => d.Name == "EMP" + p_Params.IdEmpleado + "-" + p_Params.Empleado && d.MimeType == "application/vnd.google-apps.folder" && d.Trashed == false).Select(z => z.Id).FirstOrDefault();
                    if (SubfolderId == null)
                    {
                        SubfolderId = CrearSubCarpeta(folder, "EMP" + p_Params.IdEmpleado + "-" + p_Params.Empleado);
                    }
                }

                return Ok(SubfolderId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public static string CrearCarpeta(string FolderName)
        {
            DriveService service = GetService();
            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = FolderName;
            FileMetaData.MimeType = "application/vnd.google-apps.folder";
            FileMetaData.ViewersCanCopyContent = true;

            FilesResource.CreateRequest request;
            request = service.Files.Create(FileMetaData);
            request.Fields = "id, name, webContentLink";
            var file = request.Execute();

            var Permission = new Google.Apis.Drive.v3.Data.Permission();
            Permission.Type = "anyone";
            Permission.Role = "reader";

            var permissionRequest = service.Permissions.Create(Permission, file.Id);
            permissionRequest.Fields = "id";
            var permissionResult = permissionRequest.Execute();

            return file.Id;
        }

        public static string CrearSubCarpeta(string FolderId, string FolderName)
        {
            DriveService service = GetService();
            var FileMetaData = new Google.Apis.Drive.v3.Data.File()
            {
                Name = FolderName,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string>
                {
                    FolderId
                }
            };

            FilesResource.CreateRequest request;
            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();

            return file.Id;
        }

        public static string CrearSubCarpetaEmp(string FolderEmp, string FolderId)
        {
            DriveService service = GetService();
            var FileMetaData = new Google.Apis.Drive.v3.Data.File()
            {
                Name = FolderId,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string>
                {
                    FolderEmp
                }
            };

            FilesResource.CreateRequest request;
            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();

            return file.Id;
        }

        private static void SubirArchivo(string path, DriveService service, string IdFolder)
        {
            IList<string> ListFolders = new List<string>();
            ListFolders.Add(IdFolder);
            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = Path.GetFileName(path);
            FileMetaData.MimeType = "application/octet-stream";
            FileMetaData.Parents = ListFolders;
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                request.Fields = "id";
                request.Upload();

                Google.Apis.Drive.v3.Data.File file = request.ResponseBody;
                string fileId = file.Id;
            }
        }

        [Route("EmpleadoSustento_InsertUpdate")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> EmpleadoSustento_InsertUpdate([FromBody] PSustentoHomEmpleado p_Params)
        {
            try
            {
                DriveService service = GetService();
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.Fields = "nextPageToken, files(*)";
                IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

                DataTable sustento = new DataTable();
                sustento.Columns.Add("IdCtrHomEmpleado");
                sustento.Columns.Add("IdHomologacion");
                sustento.Columns.Add("NombreDocumento");
                sustento.Columns.Add("Archivo");
                sustento.Columns.Add("TamanioArchivo");
                sustento.Columns.Add("TipoExtension");
                sustento.Columns.Add("IdEstado");
                sustento.Columns.Add("DriveId");

                p_Params.EmpleadoDocumento.ForEach(x =>
                {
                    var delete = Path.Combine(_webHostEnvironment.ContentRootPath, $"Public/Homologacion/{p_Params.IdCliente}/{p_Params.IdContratista}/{p_Params.IdEmpleado}");
                    var ruta = Path.Combine(_webHostEnvironment.ContentRootPath, $"Public/Homologacion/{p_Params.IdCliente}/{p_Params.IdContratista}/{p_Params.IdEmpleado}/{x.IdHomologacion}");
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    if (!(x.Base64 ?? "").Equals(""))
                        System.IO.File.WriteAllBytes(Path.Combine(ruta, x.Archivo), Convert.FromBase64String(x.Base64.Split(',')[1]));
                    if (files != null && files.Count > 0)
                    {
                        string folder = files.Where(x => x.Name == "CL" + p_Params.IdCliente + "-" + p_Params.RazonSocial && x.MimeType == "application/vnd.google-apps.folder" && x.Trashed == false).Select(z => z.Id).FirstOrDefault();
                        if (folder == null)
                        {
                            folder = CrearCarpeta("CL" + p_Params.IdCliente + "-" + p_Params.RazonSocial);
                            SubirArchivo(@"D:\RepoVC\PARCONSIL\PARCONSILBACKEDN\Parconsil_BackEnd\ADProv_Parconsil\Public\Pictures\Adprov.png", service, folder);
                            //SubirArchivo(@"C:\inetpub\wwwroot\sistema.adprov.app\adprov-Api\Public\Pictures\Adprov.png", service, folder);
                        }
                        string SubfolderId = files.Where(d => d.Name == "EMP" + p_Params.IdEmpleado + "-" + p_Params.Empleado && d.MimeType == "application/vnd.google-apps.folder" && d.Trashed == false).Select(z => z.Id).FirstOrDefault();
                        if (SubfolderId == null)
                        {
                            SubfolderId = CrearSubCarpeta(folder, "EMP" + p_Params.IdEmpleado + "-" + p_Params.Empleado);
                        }
                        string EmpfolderId = files.Where(d => d.Name == "DOC" + p_Params.IdEmpleado + "-" + x.NombreDocumento && d.MimeType == "application/vnd.google-apps.folder" && d.Trashed == false).Select(z => z.Id).FirstOrDefault();
                        if (EmpfolderId == null)
                        {
                            EmpfolderId = CrearSubCarpetaEmp(SubfolderId, "DOC" + p_Params.IdEmpleado + "-" + x.NombreDocumento);
                        }
                        //SubirArchivo($"{ruta}/{x.Archivo}", service, EmpfolderId);

                        IList<string> ListFolders = new List<string>();
                        ListFolders.Add(EmpfolderId);

                        var FileMetaData = new Google.Apis.Drive.v3.Data.File();
                        FileMetaData.Name = Path.GetFileName($"{ruta}/{x.Archivo}");
                        FileMetaData.MimeType = "application/octet-stream";
                        FileMetaData.Parents = ListFolders;

                        FilesResource.CreateMediaUpload request;
                        using (var stream = new System.IO.FileStream($"{ruta}/{x.Archivo}", System.IO.FileMode.Open))
                        {
                            request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                            request.Fields = "id";
                            request.Upload();

                            Google.Apis.Drive.v3.Data.File file = request.ResponseBody;
                            string fileId = file.Id;
                            x.DriveId = fileId;
                        }

                        sustento.Rows.Add(x.IdCtrHomEmpleado, x.IdHomologacion, x.NombreDocumento, x.Archivo, x.TamanioArchivo, x.TipoExtension, x.IdEstado, x.DriveId);

                        string fileExists = $"{delete}";
                        Directory.Delete(fileExists, true);
                        //if (System.IO.File.Exists(fileExists))
                        //{
                        //    System.IO.File.Delete(fileExists);
                        //}
                    }
                });

                p_Params.TEmpleadoDocumento = sustento;

                return await new DContratista(_configuration).EmpleadoSustento_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Contratista_InsertUpdate")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> Contratista_InsertUpdate([FromBody] PContratista p_Params)
        {
            try
            {
                return await new DContratista(_configuration).Contratista_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ContratistaLogin_Insert")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> ContratistaCliente_InsertUpdate([FromBody] PContratistaCliente p_Params)
        {
            try
            {
                DataTable ContratistaLogin = new DataTable();
                ContratistaLogin.Columns.Add("IdCtrCliente");
                ContratistaLogin.Columns.Add("IdCliente");
                ContratistaLogin.Columns.Add("IdContratista");
                ContratistaLogin.Columns.Add("IdClienteGrupo");

                p_Params.contratistaClienteEnlace.ForEach(x =>
                {
                    ContratistaLogin.Rows.Add(x.IdCtrCliente, x.IdCliente, x.IdContratista, x.IdClienteGrupo);
                });

                p_Params.TContratistaCliente_Enlace = ContratistaLogin;

                if (p_Params.IdContratista == 0)
                {
                    var random = new Random();
                    var password = random.Next(100000, 999999).ToString();
                    p_Params.Clave = password;

                    var email = new MimeMessage();
                    email.From.Add(new MailboxAddress("Administrador AdProv", "adprov@parconsil.com"));
                    email.To.Add(new MailboxAddress("Contratista", p_Params.Correo));
                    email.Subject = "AdProv";

                    var builder = new BodyBuilder();
                    //var image = builder.LinkedResources.Add(@"C:\inetpub\wwwroot\sistema.adprov.app\adprov-Api\Public\Pictures\Adprov.png");
                    var image = builder.LinkedResources.Add(@"D:\RepoVC\PARCONSIL\PARCONSILBACKEDN\Parconsil_BackEnd\ADProv_Parconsil\Public\Pictures\Adprov.png");
                    image.ContentId = MimeUtils.GenerateMessageId();

                    //builder.HtmlBody
                    builder.HtmlBody = string.Format
                    (
                        @"<body style='margin-left: 25%; margin-right: 22%; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;'>
                        <table cellpadding='0' cellspacing='0' class='nl-container' role='presentation' style='justify-content: center' width='100%'>
                        <tbody><tr><td>
                        <table cellpadding='0' cellspacing='0' class='row row-1' role='presentation' style='justify-content: center' width='100%'>
                        <tbody><tr><td>
                        <table cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='justify-content: center; border: 0; background-color: #2596be; color: #000000; width: 600px;' width='600'>
                        <tbody><tr>
                        <td class='column column-1' style='font-weight: 400; text-align: left; vertical-align: top; padding-top: 10px; padding-bottom: 10px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <table cellpadding='0' cellspacing='0' class='paragraph_block block-1' role='presentation' style='justify-content: center; word-break: break-word;' width='100%'>
                        <tr><td class='pad'>
                        <div style='color:#ffffff;direction:ltr;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:11px;font-weight:400;letter-spacing:3px;line-height:120%;text-align:center;'>
                        <p style='margin: 0;'>TIENE HASTA LA FECHA " + p_Params.Caducidad + @" PARA SUBIR LA INFORMACIÓN</p>
                        </div></td></tr></table></td></tr></tbody></table></td></tr></tbody></table>

                        <table cellpadding='0' cellspacing='0' class='row row-2' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='color: #000000; width: 600px;' width='600'>
                        <tbody><tr>
                        <td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <table cellpadding='0' cellspacing='0' class='image_block block-1' role='presentation' width='100%'>
                        <tr>
                        <td class='pad' style='padding-bottom:15px;padding-top:20px;width:100%;padding-right:0px;padding-left:0px;'>
                        <div style='justify-content: center; border: 0;' class='alignment' style='line-height:10px'>
                        <a style='outline:none' tabindex='-1' target='_blank'></a>
                        </div></td></tr></table></td></tr></tbody></table></td></tr></tbody></table>


                        <table cellpadding='0' cellspacing='0' class='row row-3' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='color: #000000; width: 600px;' width='600'>
                        <tbody><tr>
                        <td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 20px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <table cellpadding='0' cellspacing='0' class='paragraph_block block-1' role='presentation' style=' word-break: break-word;' width='100%'>
                        <tr><td class='pad' style='padding-top:15px;'>
                        <div style='color:#ffffff;direction:ltr;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:29px;font-weight:700;letter-spacing:0px;line-height:120%;text-align:center;'>
                        <p style='margin: 0;'>TE DAMOS LA BIENVENIDA A</p>
                        </div></td></tr></table>

                        <table cellpadding='0' cellspacing='0' class='paragraph_block block-2' role='presentation' style=' margin-top: 42px;  word-break: break-word;' width='100%'>
                        <tr><td class='pad'>
                        <div style='color:#01dad0;direction:ltr;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:77px;font-weight:700;letter-spacing:0px;line-height:120%;text-align:center;'>                        
                        <img src=""cid:{0}"" alt='Logo' width='240' style='margin: 0'>
                        </div></td></tr></table>

                        <table cellpadding='0' cellspacing='0' class='button_block block-4' role='presentation' style=' margin-top: 40px; ' width='100%'>
                        <tr><td class='pad' style='text-align:center;padding-top:15px;'>
                        <div style='justify-content: center; border: 0;' class='alignment'>
                        <div style='text-decoration:none;display:inline-block;color:#ffffff;background-color:transparent;border-radius:0px;width:auto;border-top:1px solid #FFFFFF;font-weight:400;border-right:1px solid #FFFFFF;border-bottom:1px solid #FFFFFF;border-left:1px solid #FFFFFF;padding-top:5px;padding-bottom:5px;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:10px;text-align:center;word-break:keep-all;'>
                        <span style='padding-left:20px;padding-right:20px;font-size:10px;display:inline-block;letter-spacing:normal;'>
                        <span dir='ltr' style='word-break: break-word; line-height: 20px;text-align:center;'>TIPO: CONTRATISTA</span></span>
                        </div></div></td></tr></table></td></tr></tbody></table></td></tr></tbody></table>

                        <table cellpadding='0' cellspacing='0' class='row row-4' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' color: #000000; width: 600px;' width='600'>
                        <tbody><tr>
                        <td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <table cellpadding='0' cellspacing='0' class='image_block block-1' role='presentation' width='100%'>
                        <tr></tr></table></td></tr></tbody></table></td></tr></tbody></table>
                            
                        <table cellpadding='0' cellspacing='0' class='row row-5' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' background-color: #2596be; color: #000000; width: 600px;' width='600'>
                        <tbody><tr>
                        <td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; vertical-align: top; padding-top: 25px; padding-bottom: 30px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <table cellpadding='0' cellspacing='0' class='paragraph_block block-1' role='presentation' style=' word-break: break-word;' width='100%'>
                        <tr><td class='pad'>
                        <div style='color:#ffffff;direction:ltr;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:18px;font-weight:400;letter-spacing:0px;line-height:120%;text-align:center;'></div>
                        </td></tr></table>

                        <table cellpadding='0' cellspacing='0' class='paragraph_block block-2' role='presentation' style=' word-break: break-word;' width='100%'>
                        <tr><td class='pad'>
                        <div style='color:#ffffff;direction:ltr;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:18px;font-weight:400;letter-spacing:0px;line-height:120%;text-align:center;'>
                        <p style='margin: 0;'>Este es su usuario y contraseña para ingresar al sistema:</p>
                        </div></td></tr></table>

                        <table cellpadding='0' cellspacing='0' class='paragraph_block block-4' role='presentation' style=' word-break: break-word;' width='100%'>
                        <tr><td class='pad' style='padding-bottom:5px;padding-top:40px;'>
                        <div style='color:#ffffff;direction:ltr;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:25px;font-weight:700;letter-spacing:0px;line-height:120%;text-align:center;'>
                        <p style='margin: 0;text-align:center;'>USUARIO: " + p_Params.ContratistaUser + @"</p>
                        </div></td></tr></table>

                        <table cellpadding='0' cellspacing='0' class='heading_block block-5' role='presentation' width='100%'>
                        <tr><td class='pad' style='width:100%;text-align:center;'>
                        <h1 style='margin: 0; color: #ffffff; font-size: 25px; font-family: Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif; line-height: 120%; justify-content: center; direction: ltr; font-weight: 700; letter-spacing: normal; margin-top: 0; margin-bottom: 0;'>
                        <span class='tinyMce-placeholder' style='margin: 0;text-align:center;'>CLAVE: " + p_Params.Clave + @"</span>
                        </h1></td></tr></table>

                        <table cellpadding='0' cellspacing='0' class='button_block block-7' role='presentation' width='100%'>
                        <tr><td class='pad' style='text-align:center;padding-top:60px;'>
                        <div style='justify-content: center; border: 0;' class='alignment'>
                        <a href = 'https://sistema.adprov.app/web/' style='justify-content: center;text-decoration:none;display:inline-block;color:#ffffff;background-color:#21476e;border-radius:0px;width:auto;border-top:0px solid transparent;font-weight:700;border-right:0px solid transparent;border-bottom:0px solid transparent;border-left:0px solid transparent;padding-top:5px;padding-bottom:5px;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:16px;text-align:center;word-break:keep-all;' target='_blank'>
                        <span style='padding-left:20px;padding-right:20px;font-size:16px;display:inline-block;letter-spacing:normal;'>
                        <span dir='ltr' style='word-break: break-word; line-height: 32px;'>ABRIR DESDE EL NAVEGADOR</span></span></a>
                        </div></td></tr></table></td></tr></tbody></table></td></tr></tbody></table>

                        <table cellpadding='0' cellspacing='0' class='row row-13' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' color: #000000; width: 600px;' width='600'>
                        <tbody><tr><td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 0px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <div class='spacer_block' style='height:30px;line-height:30px;font-size:1px;'> </div>
                        </td></tr></tbody></table></td></tr></tbody></table>

                        <table cellpadding='0' cellspacing='0' class='row row-14' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' background-color: #151515; color: #000000; width: 600px;' width='600'>
                        <tbody><tr><td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; padding-left: 20px; padding-right: 20px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='33.333333333333336%'>
                        <table cellpadding='0' cellspacing='0' class='html_block block-2' role='presentation' width='100%'>
                        <tr><td class='pad' style='padding-top:5px;'>
                        <div style='justify-content: center; border: 0;' style='font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;text-align:center;'>
                        <div style='height:20px;'> </div></div>
                        </td></tr></table></td>

                        <td class='column column-2' style='background-color: #24293d; font-weight: 400; text-align: left; padding-left: 20px; padding-right: 20px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='66.66666666666667%'>
                        <table cellpadding='0' cellspacing='0' class='text_block block-2' role='presentation' style='word-break: break-word;' width='100%'>
                        <tr><td class='pad' style='padding-bottom:15px;padding-right:10px;padding-top:15px;'>
                        <div style='font-family: Arial, sans-serif'>
                        <div class='' style='font-size: 12px; font-family: Open Sans, Helvetica Neue, Helvetica, Arial, sans-serif; color: #C0C0C0; line-height: 1.2;'>
                        <p style='margin: 0; text-align: justify; '> </p>
                        <p style='margin: 0; text-align: right; '><span style='color:#c0c0c0;'>Copyright ©2023, Todos los derechos reservados.</span></p>
                        <p style='margin: 0; justify-content: center;'> </p>
                        </div></div></td></tr></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body>", image.ContentId
                        );

                    email.Body = builder.ToMessageBody();

                    // send email
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    ////smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.Auto);
                    //smtp.Connect("mail.parconsil.com");
                    //smtp.Connect("smtp.gmail.com", 587, false);

                    //smtp.Send(email);
                    //smtp.Disconnect(true);

                    using (var client = new SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.Connect("mail.parconsil.com");
                        client.Authenticate("adprov@parconsil.com", "Adprov2023");

                        client.Send(email);
                        client.Disconnect(true);

                    }
                }

                return await new DContratista(_configuration).ContratistaCliente_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpGet]
        //public async Task<ActionResult<Response>> Email(string correo, string clave, string destinatario)
        //{
        //    Response rpta;
        //    try
        //    {
        //        //var user = _configuration.GetSection("Smtp:Dominio");
        //        //var password = _configuration.GetSection("Smtp:Alias");
        //        MimeMessage mensaje = new MimeMessage();

        //        MailboxAddress from = new MailboxAddress("Prueb", correo);
        //        mensaje.From.Add(from);

        //        //foreach (var address in _params.Correo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //        //{
        //        MailboxAddress to = new MailboxAddress("Destino", destinatario);
        //        mensaje.To.Add(to);
        //        //}

        //        mensaje.Subject = "Prueba_Asunto";

        //        BodyBuilder bodyBuilder = new BodyBuilder();
        //        bodyBuilder.HtmlBody = "Contenido";
        //        bodyBuilder.TextBody = "Contenido 2";

        //        mensaje.Body = bodyBuilder.ToMessageBody();

        //        using (var client = new SmtpClient())
        //        {

        //            client.Connect("mail.parconsil.com");
        //            client.Authenticate(correo, clave);

        //            client.Send(mensaje);
        //            client.Disconnect(true);
        //            //client.LocalDomain = user.Value;
        //            //await client.ConnectAsync(_params.smtp, _params.port, SecureSocketOptions.None).ConfigureAwait(false);
        //            //await client.SendAsync(message).ConfigureAwait(false);
        //            //await client.DisconnectAsync(true).ConfigureAwait(false);
        //        }

        //        //SmtpClient client = new SmtpClient();
        //        //client.Connect("smtp.gmail.com", 465, true);
        //        //client.Connect(_params.smtp, _params.port, true);
        //        //client.Authenticate(user.Value, password.Value);

        //        //client.Send(message);
        //        //client.Disconnect(true);
        //        //client.Dispose();

        //        rpta = new Response
        //        {
        //            Ok = true,
        //            Message = "Mensaje Enviado "
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        //Helpers.WriteLog(_webHostEnvironment.ContentRootPath, "Weighing_Email", ex.Message);
        //        rpta = new Response
        //        {
        //            Message = "[B]: An internal error has occurred" + ex.Message
        //        };
        //    }
        //    return rpta;

        //    //var message = new MimeMessage();

        //    //message.From.Add(new MailboxAddress("Joey Tribbiani", "noreply@localhost.com"));
        //    //message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "mymail@gmail.com"));
        //    //message.Subject = "How you doin'?";
        //    //message.Body = new TextPart("plain") { Text = @"Hey" };

        //    //using (var client = new SmtpClient())
        //    //{
        //    //    client.Connect("smtp.gmail.com", 587);

        //    //    ////Note: only needed if the SMTP server requires authentication
        //    //    client.Authenticate("mymail@gmail.com", "mypassword");

        //    //    client.Send(message);
        //    //    client.Disconnect(true);
        //    //}
        //}


        [Route("Contratista_Delete")]
        [HttpDelete]
        public async Task<ActionResult<EMessage>> Contratista_Delete(int IdContratista, int IdUsuarioAud)
        {
            try
            {
                return await new DContratista(_configuration).Contratista_Delete(IdContratista, IdUsuarioAud);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Empleado_List")]
        [HttpGet]
        public async Task<ActionResult<List<EEmpleado>>> Empleado_List(int IdCliente, int IdProyecto, int IdContratista, int IdEstado, string Search, int PageIndex, int PageSize)
        {
            try
            {
                return await new DEmpleado(_configuration).Empleado_List(new EEmpleado
                {
                    IdCliente = IdCliente,
                    IdProyecto = IdProyecto,
                    IdContratista = IdContratista,
                    IdEstado = IdEstado,
                    Search = Search ?? "",
                    PageIndex = PageIndex,
                    PageSize = PageSize
                });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("Empleado_List_document")]
        [HttpGet]
        public async Task<ActionResult<List<EEmpleado>>> Empleado_List_document(int IdCliente,  string NroDocumento)
        {
            try
            {
                return await new DEmpleado(_configuration).Empleado_List_document(IdCliente, NroDocumento);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }




        [Route("Empleado_InsertUpdate")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> Empleado_InsertUpdate([FromBody] PEmpleado p_Params)
        {
            try
            {
                //DataTable EmpleadoVinculo = new DataTable();
                //EmpleadoVinculo.Columns.Add("IdEmpContratista");
                //EmpleadoVinculo.Columns.Add("IdCliente");
                //EmpleadoVinculo.Columns.Add("IdContratista");
                //EmpleadoVinculo.Columns.Add("IdClienteGrupo");

                //p_Params.empleadoContratistaEnlace.ForEach(x =>
                //{
                //    EmpleadoVinculo.Rows.Add(x.IdEmpContratista, x.IdEmpleado, x.IdCliente, x.IdContratista, x.IdProyecto);
                //});

                //p_Params.TEmpleadoContratista_Enlace = EmpleadoVinculo;
                return await new DEmpleado(_configuration).Empleado_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Empleado_Delete")]
        [HttpDelete]
        public async Task<ActionResult<EMessage>> Empleado_Delete(int IdEmpleado, int IdUsuarioAud)
        {
            try
            {
                return await new DEmpleado(_configuration).Empleado_Delete(new EEmpleadoD
                {
                    IdEmpleado = IdEmpleado,
                    IdUsuarioAud = IdUsuarioAud
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EmpresaMatriz_Get")]
        [HttpGet]
        public async Task<ActionResult<List<EDocumentosEmpresa>>> EmpresaMatriz_Get(int IdCliente, int IdClienteGrupo)
        {
            try
            {
                return await new DCliente(_configuration).EmpresaMatriz_Get(new EDocumentosEmpresa
                {
                    IdCliente = IdCliente,
                    IdClienteGrupo = IdClienteGrupo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EmpleadoMatriz_Get")]
        [HttpGet]
        public async Task<ActionResult<List<EDocumentosEmpleado>>> EmpleadoMatriz_Get(int IdCliente, int IdClienteProyecto)
        {
            try
            {
                return await new DCliente(_configuration).EmpleadoMatriz_Get(new EDocumentosEmpleado
                {
                    IdCliente = IdCliente,
                    IdClienteProyecto = IdClienteProyecto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DocumentoHomEmpresa_List")]
        [HttpGet]
        public async Task<ActionResult<List<EDocumentosEmpresa>>> DocumentoHomEmpresa_List(int IdCliente, int IdClienteGrupo, int IdContratista)
        {
            try
            {
                return await new DCliente(_configuration).DocumentoHomEmpresa_List(new EDocumentosEmpresa
                {
                    IdCliente = IdCliente,
                    IdClienteGrupo = IdClienteGrupo,
                    IdContratista = IdContratista
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DocumentoHomEmpleado_List")]
        [HttpGet]
        public async Task<ActionResult<List<EDocumentosEmpleado>>> DocumentoHomEmpleado_List(int IdCliente, int IdProyecto, int IdEmpleado,
            int IdRegimenGeneral, int IdRegimenMype, int IdRegimenAgrario, int IdConstruccionCivil)
        {
            try
            {
                return await new DCliente(_configuration).DocumentoHomEmpleado_List(new EDocumentosEmpleado
                {
                    IdCliente = IdCliente,
                    IdProyecto = IdProyecto,
                    IdEmpleado = IdEmpleado,
                    IdRegimenGeneralParameter = IdRegimenGeneral,
                    IdRegimenMypeParameter = IdRegimenMype,
                    IdRegimenAgrarioParameter = IdRegimenAgrario,
                    IdConstruccionCivilParameter = IdConstruccionCivil
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DocumentoEmpresa_Get")]
        [HttpGet]
        public async Task<ActionResult<List<EDocumentosEmpresa>>> DocumentoEmpresa_Get(int IdDocEmpresa)
        {
            try
            {
                return await new DCliente(_configuration).DocumentoEmpresa_Get(new EDocumentosEmpresa
                {
                    IdDocEmpresa = IdDocEmpresa
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DocumentoEmpleado_Get")]
        [HttpGet]
        public async Task<ActionResult<List<EDocumentosEmpresa>>> DocumentoEmpleado_Get(int IdDocEmpleado)
        {
            try
            {
                return await new DCliente(_configuration).DocumentoEmpleado_Get(new EDocumentosEmpresa
                {
                    IdDocEmpleado = IdDocEmpleado
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Documento_InsertUpdate")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> Documento_InsertUpdate([FromBody] PDocumento p_Params)
        {
            try
            {
                return await new DDocumento(_configuration).Documento_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("HomologacionEmpresa_Delete")]
        [HttpDelete]
        public async Task<ActionResult<EMessage>> HomologacionEmpresa_Delete(int IdCtrHomologacion)
        {
            try
            {
                return await new DDocumento(_configuration).HomologacionEmpresa_Delete(IdCtrHomologacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("PuntajeFinal_List")]
        [HttpGet]
        public async Task<ActionResult<List<EPuntajeFinal>>> PuntajeFinal_List(int IdCliente, int IdClienteGrupo, int IdContratista)
        {
            try
            {
                return await new DCliente(_configuration).PuntajeFinal_List(new EDocumentosEmpresa
                {
                    IdCliente = IdCliente,
                    IdClienteGrupo = IdClienteGrupo,
                    IdContratista = IdContratista
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DocumentosHomologacion_InsertUpdate")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> DocumentosHomologacion_InsertUpdate([FromBody] PDocumentosXCliente p_Params)
        {
            try
            {
                return await new DDocumento(_configuration).DocumentosHomologacion_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EmpresaDocumento_InsertUpdate")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> EmpresaDocumento_InsertUpdate([FromBody] PNewDocCliente p_Params)
        {
            try
            {
                return await new DCliente(_configuration).EmpresaDocumento_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ContratistaCliente_UpdateEstado")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> ContratistaCliente_UpdateEstado([FromBody] PContratistaUpdate p_Params)
        {

            try
            {
                var emailReturn = new MimeMessage();
                emailReturn.From.Add(new MailboxAddress("Administrador AdProv", "adprov@parconsil.com"));
                emailReturn.To.Add(new MailboxAddress("Contratista", p_Params.Correo));
                emailReturn.Subject = "AdProv";

                var builder = new BodyBuilder();
                //var image = builder.LinkedResources.Add(@"C:\inetpub\wwwroot\sistema.adprov.app\adprov-Api\Public\Pictures\Adprov.png");
                var image = builder.LinkedResources.Add(@"D:\RepoVC\PARCONSIL\PARCONSILBACKEDN\Parconsil_BackEnd\ADProv_Parconsil\Public\Pictures\Adprov.png");
                image.ContentId = MimeUtils.GenerateMessageId();

                //builder.HtmlBody
                builder.HtmlBody = string.Format
                (
                    @"<body style='margin-left: 25%; margin-right: 22%; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;'>
                        <table cellpadding='0' cellspacing='0' class='nl-container' role='presentation' style='justify-content: center' width='100%'>
                        <tbody><tr><td>
                        <table cellpadding='0' cellspacing='0' class='row row-1' role='presentation' style='justify-content: center' width='100%'>
                        <tbody><tr><td>
                        <table cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='justify-content: center; border: 0; background-color: #2596be; color: #000000; width: 600px;' width='600'>
                        <tbody><tr>
                        <td class='column column-1' style='font-weight: 400; text-align: left; vertical-align: top; padding-top: 10px; padding-bottom: 10px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <table cellpadding='0' cellspacing='0' class='paragraph_block block-1' role='presentation' style='justify-content: center; word-break: break-word;' width='100%'>
                        <tr><td class='pad'>
                        <div style='color:#ffffff;direction:ltr;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:11px;font-weight:400;letter-spacing:3px;line-height:120%;text-align:center;'>
                        <p style='margin: 0;'>RECUERDA SUBIR TU INFORMACIÓN ANTES DE LA FECHA LÍMITE</p>
                        </div></td></tr></table></td></tr></tbody></table></td></tr></tbody></table>

                        <table cellpadding='0' cellspacing='0' class='row row-2' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='color: #000000; width: 600px;' width='600'>
                        <tbody><tr>
                        <td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <table cellpadding='0' cellspacing='0' class='image_block block-1' role='presentation' width='100%'>
                        <tr>
                        <td class='pad' style='padding-bottom:15px;padding-top:20px;width:100%;padding-right:0px;padding-left:0px;'>
                        <div style='justify-content: center; border: 0;' class='alignment' style='line-height:10px'>
                        <a style='outline:none' tabindex='-1' target='_blank'></a>
                        </div></td></tr></table></td></tr></tbody></table></td></tr></tbody></table>


                        <table cellpadding='0' cellspacing='0' class='row row-3' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='color: #000000; width: 600px;' width='600'>
                        <tbody><tr>
                        <td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 20px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <table cellpadding='0' cellspacing='0' class='paragraph_block block-1' role='presentation' style=' word-break: break-word;' width='100%'>
                        <tr><td class='pad' style='padding-top:15px;'>
                        <div style='color:#ffffff;direction:ltr;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:29px;font-weight:700;letter-spacing:0px;line-height:120%;text-align:center;'>
                        <p style='margin: 0;'>DOCUMENTOS REVISADOS</p>
                        </div></td></tr></table>

                        <table cellpadding='0' cellspacing='0' class='paragraph_block block-2' role='presentation' style=' margin-top: 42px;  word-break: break-word;' width='100%'>
                        <tr><td class='pad'>
                        <div style='color:#01dad0;direction:ltr;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:77px;font-weight:700;letter-spacing:0px;line-height:120%;text-align:center; justify-content: center'>                        
                        <img src=""cid:{0}"" alt='Logo' width='200' style='margin: 0'>
                        </div></td></tr></table>
                        
                        <table cellpadding='0' cellspacing='0' class='button_block block-4' role='presentation' style=' margin-top: 40px; ' width='100%'>
                        <tr><td class='pad' style='text-align:center;padding-top:15px;'>
                        <div style='justify-content: center; border: 0;' class='alignment'>
                        <div style='text-decoration:none;display:inline-block;color:#ffffff;background-color:transparent;border-radius:0px;width:auto;border-top:1px solid #FFFFFF;font-weight:400;border-right:1px solid #FFFFFF;border-bottom:1px solid #FFFFFF;border-left:1px solid #FFFFFF;padding-top:5px;padding-bottom:5px;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:10px;text-align:center; justify-content: center; word-break:keep-all;'>
                        <span style='padding-left:20px;padding-right:20px;font-size:10px;display:inline-block;letter-spacing:normal;'>
                        <span dir='ltr' style='word-break: break-word; line-height: 20px;text-align:center;'>TIPO: CONTRATISTA</span></span>
                        </div></div></td></tr></table></td></tr></tbody></table></td></tr></tbody></table>

                        <table cellpadding='0' cellspacing='0' class='row row-4' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' color: #000000; width: 600px;' width='600'>
                        <tbody><tr>
                        <td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <table cellpadding='0' cellspacing='0' class='image_block block-1' role='presentation' width='100%'>
                        <tr></tr></table></td></tr></tbody></table></td></tr></tbody></table>
                            
                        <table cellpadding='0' cellspacing='0' class='row row-5' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' background-color: #2596be; color: #000000; width: 600px;' width='600'>
                        <tbody><tr>
                        <td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; vertical-align: top; padding-top: 25px; padding-bottom: 30px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <table cellpadding='0' cellspacing='0' class='paragraph_block block-1' role='presentation' style=' word-break: break-word;' width='100%'>
                        <tr><td class='pad'>
                        <div style='color:#ffffff;direction:ltr;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:18px;font-weight:400;letter-spacing:0px;line-height:120%;text-align:center;'></div>
                        </td></tr></table>

                        <table cellpadding='0' cellspacing='0' class='paragraph_block block-2' role='presentation' style=' word-break: break-word;' width='100%'>
                        <tr><td class='pad'>
                        <div style='color:#ffffff;direction:ltr; margin:30px; font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:18px;font-weight:400;letter-spacing:0px;line-height:120%;text-align:center;'>                        
                        <p style='margin: 0;'>Buen día, " + p_Params.ContratistaUser + @"</p>
                        <br><span> los documentos han sido revisados, por favor revisar y volver a enviar en caso hubiera documentos rechazados.    
    
                        </div></td></tr></table>
                        
                        <table cellpadding='0' cellspacing='0' class='heading_block block-5' role='presentation' width='100%'>
                        <tr><td class='pad' style='width:100%;text-align:center;'>
                        <h1 style='margin: 0; color: #ffffff; font-size: 25px; font-family: Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif; line-height: 120%; justify-content: center; direction: ltr; font-weight: 700; letter-spacing: normal; margin-top: 0; margin-bottom: 0;'>                        
                        </h1></td></tr></table>

                        <table cellpadding='0' cellspacing='0' class='button_block block-7' role='presentation' width='100%'>
                        <tr><td class='pad' style='text-align:center;padding-top:60px;'>
                        <div style='justify-content: center; border: 0;' class='alignment'>
                        <a href = 'https://sistema.adprov.app/web/' style='justify-content: center;text-decoration:none;display:inline-block;color:#ffffff;background-color:#21476e;border-radius:0px;width:auto;border-top:0px solid transparent;font-weight:700;border-right:0px solid transparent;border-bottom:0px solid transparent;border-left:0px solid transparent;padding-top:5px;padding-bottom:5px;font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;font-size:16px;text-align:center;word-break:keep-all;' target='_blank'>
                        <span style='padding-left:20px;padding-right:20px;font-size:16px;display:inline-block;letter-spacing:normal;'>
                        <span dir='ltr' style='word-break: break-word; line-height: 32px; text-align:center;'>ABRIR DESDE EL NAVEGADOR</span></span></a>
                        </div></td></tr></table></td></tr></tbody></table></td></tr></tbody></table>

                        <table cellpadding='0' cellspacing='0' class='row row-13' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' color: #000000; width: 600px;' width='600'>
                        <tbody><tr><td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 0px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
                        <div class='spacer_block' style='height:30px;line-height:30px;font-size:1px;'> </div>
                        </td></tr></tbody></table></td></tr></tbody></table>

                        <table cellpadding='0' cellspacing='0' class='row row-14' role='presentation' style='justify-content: center;' width='100%'>
                        <tbody><tr><td>
                        <table style='justify-content: center; border: 0;' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' background-color: #151515; color: #000000; width: 600px;' width='600'>
                        <tbody><tr><td class='column column-1' style='background-color: #24293d; font-weight: 400; text-align: left; padding-left: 20px; padding-right: 20px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='33.333333333333336%'>
                        <table cellpadding='0' cellspacing='0' class='html_block block-2' role='presentation' width='100%'>
                        <tr><td class='pad' style='padding-top:5px;'>
                        <div style='justify-content: center; border: 0;' style='font-family:Montserrat, Trebuchet MS, Lucida Grande, Lucida Sans Unicode, Lucida Sans, Tahoma, sans-serif;text-align:center;'>
                        <div style='height:20px;'> </div></div>
                        </td></tr></table></td>

                        <td class='column column-2' style='background-color: #24293d; font-weight: 400; text-align: left; padding-left: 20px; padding-right: 20px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='66.66666666666667%'>
                        <table cellpadding='0' cellspacing='0' class='text_block block-2' role='presentation' style='word-break: break-word;' width='100%'>
                        <tr><td class='pad' style='padding-bottom:15px;padding-right:10px;padding-top:15px;'>
                        <div style='font-family: Arial, sans-serif'>
                        <div class='' style='font-size: 12px; font-family: Open Sans, Helvetica Neue, Helvetica, Arial, sans-serif; color: #C0C0C0; line-height: 1.2;'>
                        <p style='margin: 0; text-align: justify; '> </p>
                        <p style='margin: 0; text-align: justify; '><span style='color:#c0c0c0;'>Copyright ©2023, Todos los derechos reservados.</span></p>
                        <p style='margin: 0; justify-content: center;'> </p>
                        </div></div></td></tr></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body>", image.ContentId
                    );

                emailReturn.Body = builder.ToMessageBody();

                // send email
                //SmtpClient smtp = new SmtpClient();
                //smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                ////smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.Auto);
                //smtp.Connect("mail.parconsil.com");
                //smtp.Connect("smtp.gmail.com", 587, false);

                //smtp.Send(email);
                //smtp.Disconnect(true);

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("mail.parconsil.com");
                    client.Authenticate("adprov@parconsil.com", "Adprov2023");

                    client.Send(emailReturn);
                    client.Disconnect(true);

                }

                return await new DCliente(_configuration).ContratistaCliente_UpdateEstado(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ContratistaEmpleado_UpdateEstado")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> ContratistaEmpleado_UpdateEstado([FromBody] PEmpleadoUpdate p_Params)
        {
            try
            {
                return await new DCliente(_configuration).ContratistaEmpleado_UpdateEstado(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EmpleadoClienteHom_Update")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> EmpleadoClienteHom_Update([FromBody] PClienteFecha p_Params)
        {
            try
            {
                return await new DCliente(_configuration).EmpleadoClienteHom_Update(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ContratistaProyecto_InsertUpdate")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> ContratistaProyecto_InsertUpdate([FromBody] PContratistaProyecto p_Params)
        {
            try
            {
                return await new DCliente(_configuration).ContratistaProyecto_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Documento_Delete")]
        [HttpDelete]
        public async Task<ActionResult<EMessage>> Documento_Delete(int IdDocumento, int IdUsuarioAud)
        {
            try
            {
                return await new DDocumento(_configuration).Documento_Delete(IdDocumento, IdUsuarioAud);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ContratistaGrupo_List")]
        [HttpGet]
        public async Task<ActionResult<List<GruposContratista>>> ContratistaGrupo_List(int Idcliente, int IdContratista, int PageIndex, int PageSize)
        {
            try
            {
                return await new DContratista(_configuration).ContratistaGrupo_List(new GruposContratista
                {
                    IdCliente = Idcliente,
                    IdContratista = IdContratista,
                    PageIndex = PageIndex,
                    PageSize = PageSize
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ContratistaProyecto_List")]
        [HttpGet]
        public async Task<ActionResult<List<GruposProyecto>>> ContratistaProyecto_List(int IdContratista, int IdProyecto)
        {
            try
            {
                return await new DContratista(_configuration).ContratistaProyecto_List(new GruposProyecto
                {
                    IdContratista = IdContratista,
                    IdProyecto = IdProyecto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Sector_List")]
        [HttpGet]
        public async Task<ActionResult<List<EClienteSector>>> Sector_List(int IdEstado, string Search, int PageIndex, int PageSize)
        {
            try
            {
                return await new DContratista(_configuration).Sector_List(new EClienteSector
                {
                    IdEstado = IdEstado,
                    Search = Search,
                    PageIndex = PageIndex,
                    PageSize = PageSize
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Sector_InsertUpdate")]
        [HttpPost]
        public async Task<ActionResult<EMessage>> Sector_InsertUpdate([FromBody] PClienteSector p_Params)
        {
            try
            {
                return await new DContratista(_configuration).Sector_InsertUpdate(p_Params);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Sector_Delete")]
        [HttpDelete]
        public async Task<ActionResult<EMessage>> Sector_Delete(int IdDocumento, int IdUsuarioAud)
        {
            try
            {
                return await new DContratista(_configuration).Sector_Delete(IdDocumento, IdUsuarioAud);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Contratista_Report")]
        [HttpGet]
        //, string Search, int PageIndex, int PageSize
        public async Task<ActionResult> Contratista_Report(int IdCliente, int IdContratista, int IdEstado, string Search, int PageIndex, int PageSize)
        {
            var fileName = "Contratistas Reporte.xlsx";

            try
            {
                XLWorkbook xLWorkbook = new XLWorkbook();
                var xLWSBase = xLWorkbook.AddWorksheet("CONTRATISTAS");

                EContratista listCon = await new DContratista(_configuration).ContratistaCliente_List(new EContratista
                {
                    IdCliente = IdCliente,
                    IdContratista = IdContratista,
                    IdEstado = IdEstado,
                    Search = Search ?? "",
                    PageIndex = PageIndex,
                    PageSize = PageSize
                });

                int rowIndex = 1;
                int idx = 1;
                Helpers.CellRangefont(xLWSBase.Range("A" + (rowIndex), "E" + (rowIndex)), ":: Contratistas", 14, true).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                rowIndex = 2;

                Helpers.Cell(xLWSBase.Cell("A2"), "CODIGO");
                Helpers.Cell(xLWSBase.Cell("B2"), "RUC (CLIENTE)");
                Helpers.Cell(xLWSBase.Cell("C2"), "Razon Social (CLIENTE)");
                Helpers.Cell(xLWSBase.Cell("D2"), "RUC");
                Helpers.Cell(xLWSBase.Cell("E2"), "RazonSocial");
                Helpers.Cell(xLWSBase.Cell("F2"), "Direccion");
                Helpers.Cell(xLWSBase.Cell("G2"), "Provincia");
                Helpers.Cell(xLWSBase.Cell("H2"), "Distrito");
                Helpers.Cell(xLWSBase.Cell("I2"), "PaginaWeb");
                Helpers.Cell(xLWSBase.Cell("J2"), "Telefono");
                Helpers.Cell(xLWSBase.Cell("K2"), "Correo");
                Helpers.Cell(xLWSBase.Cell("L2"), "FechaIngreso");
                Helpers.Cell(xLWSBase.Cell("M2"), "FechaCierre");
                Helpers.Cell(xLWSBase.Cell("N2"), "Estado");

                var headerC = xLWSBase.Range(rowIndex, 1, rowIndex, 14);
                headerC.Style.Fill.BackgroundColor = XLColor.FromHtml("62C9F0");
                headerC.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                headerC.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                headerC.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                headerC.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                headerC.Style.Font.Bold = true;
                rowIndex++;


                for (int i = 0; i < listCon.ContratistaCliente.Count; i++)
                {
                    idx = 1;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].CodContratista;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].RucCliente;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].RsCliente;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].RUC;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].RazonSocial;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].Direccion;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].Provincia;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].Distrito;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].PaginaWeb;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].Telefono;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].Correo;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].FechaIngreso;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].FechaCierre;
                    idx++;
                    xLWSBase.Cell(rowIndex, idx).Value = listCon.ContratistaCliente[i].Estado;
                    idx++;

                    var fill = xLWSBase.Range(rowIndex, 1, rowIndex, 14);
                    fill.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    fill.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    fill.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    fill.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    fill.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    fill.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    fill.Style.Font.Bold = true;
                    rowIndex++;
                }

                rowIndex++;



                xLWSBase.Columns(1, 14).AdjustToContents();
                

                var memoryStream = new MemoryStream();
                xLWorkbook.SaveAs(memoryStream);

                var content = memoryStream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                Helpers.WriteLog(_webHostEnvironment.ContentRootPath, "CONTRATISTA_LIST", ex.Message);
                return new EmptyResult();
            }
        }

    }
}
