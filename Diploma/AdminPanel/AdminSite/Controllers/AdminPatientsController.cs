namespace AdminSite.Controllers
{
    using System;
    using System.IO;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using AspNet.Security.OAuth.Validation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Server.Infrastructure.Interfaces;
    using Server.Infrastructure.Models;

    [Authorize]
    [Route("api/[controller]")]
    public class AdminPatientsController : /*TypeController<PatientViewModel, Patient>*/ Controller
    {
        private static byte[] fileBytes;

        private readonly IPatientLogic patientLogic;

        private readonly IHostingEnvironment hostingEnvironment;

        public AdminPatientsController(IPatientLogic patientLogic, IHostingEnvironment hostingEnvironment)
        {
            this.patientLogic = patientLogic;

            this.hostingEnvironment = hostingEnvironment;
        }

        private static string PathData { get; set; }

        // POST: api/AdminPatients/AddPatient
        //[Authorize(Roles = "SystemAdministrator")]
        [AllowAnonymous]
        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatientAsync([FromBody] PatientViewModel patientViewModel)
        {
            if (Equals(patientViewModel, null))
            {
                return this.BadRequest("Форму повністю не заповнено");
            }

            try
            {
                patientViewModel = await this.patientLogic.AddPatientAsync(patientViewModel);
                if (Equals(patientViewModel.PatientId, 0))
                {
                    return this.Json("Паціент з цим e-mail вже існує!");
                }

                if (PathData != null)
                {
                    using (FileStream fstream = new FileStream(System.IO.Path.Combine(PathData, patientViewModel.PatientId + ".dat"), FileMode.OpenOrCreate))
                    {
                        // запись массива байтов в файл
                        await fstream.WriteAsync(fileBytes, 0, fileBytes.Length);
                        PathData = null;
                        fileBytes = null;
                    }
                }

                return this.Json("Пацієнта включено до системи!");
            }
            catch (Exception ex)
            {
                return this.Json(new string[] { "Exception", ex.Message });
            }
        }

        // POST: api/AdminPatients/UpdatePatient
        //[Authorize(Roles = "SystemAdministrator")]
        [AllowAnonymous]
        [HttpPatch("UpdatePatient")]
        public async Task<IActionResult> UpdatePatientAsync([FromBody] PatientViewModel model)
        {
            try
            {
                await this.patientLogic.UpdatePatientAsync(model);

                if (PathData != null)
                {
                    System.IO.File.Delete(System.IO.Path.Combine(PathData, model.PatientId + ".dat"));
                    using (FileStream fstream = new FileStream(System.IO.Path.Combine(PathData, model.PatientId + ".dat"), FileMode.OpenOrCreate))
                    {
                        // запись массива байтов в файл
                        await fstream.WriteAsync(fileBytes, 0, fileBytes.Length);
                        PathData = null;
                        fileBytes = null;
                    }
                }
            }
            catch (Exception ex)
            {
                return this.Json(new string[] { "Exception", ex.Message });
            }

            return this.Json("Дані пацієнта успішно відредаговано!");
        }

        // Delete: api/AdminPatients/DeletePatient/{patientId}
        //[Authorize(Roles = "SystemAdministrator")]
        [AllowAnonymous]
        [HttpDelete("DeletePatient/{patientId}")]
        public async Task<IActionResult> DeletePatientAsync(int patientId)
        {
            try
            {
                string folderName = "Upload";
                string webRootPath = this.hostingEnvironment.WebRootPath;
                string path = Path.Combine(webRootPath, folderName);
                if (Directory.Exists(Path.Combine(path, patientId + ".dat")))
                {
                    System.IO.File.Delete(Path.Combine(path, patientId + ".dat"));
                }

                await this.patientLogic.DeletePatientAsync(patientId);
                return this.Json("Пацієнта видалено з системи.");
            }
            catch (Exception ex)
            {
                return this.Json(new string[] { "Exception", ex.Message });
            }
        }

        //[Authorize(Roles = "SystemAdministrator")]
        [AllowAnonymous]
        [HttpPost("UploadFile")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var file = this.Request.Form.Files[0];

                    if (Path.GetExtension(file.FileName) != ".dat")
                    {
                        return this.Json(new string[] { "Exception", "Тільки файл з розширенням dat!" });
                    }
                    
                    string folderName = "Upload";
                    string webRootPath = this.hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName);

                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }

                    if (file.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        PathData = newPath;
                        using (var ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            fileBytes = ms.ToArray();
                        }
                    }
                    else
                    {
                        return this.Json(new string[] { "Exception", "Файл порожній!" });
                    }

                    return this.Json("Файл завантажено!");
                }
                else
                {
                    return this.Json(new string[] { "Exception", "Modal exception" });
                }
            }
            catch (Exception ex)
            {
                return this.Json(new string[] { "Exception", ex.Message });
            }
        }

        //[Authorize(Roles = "SystemAdministrator")]
        [AllowAnonymous]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            return this.Json(await this.patientLogic.GetPatientsListAsync());
        }
    }
}
