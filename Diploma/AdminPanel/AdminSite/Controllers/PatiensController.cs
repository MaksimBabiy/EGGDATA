namespace AdminSite.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using AdminPanel.DataBaseCore;
    using AdminPanelDataBaseCore.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/Patients")]
    public class PatiensController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly AdminDbContext db;

        private readonly IHostingEnvironment hostingEnvironment;

        public PatiensController(
            AdminDbContext db,
            IHostingEnvironment hostingEnvironment,
            UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        public static byte[] FileBytes { get; set; }

        private static string Path { get; set; }

        [Authorize]
        [HttpPost("Add")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Add([FromBody]Patient model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var userName = this.userManager.GetUserName(this.User);
                    Patient patient = new Patient()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MiddleName = model.MiddleName,
                        Age = model.Age,
                        Weight = model.Weight,
                        Height = model.Height,
                        Sex = model.Sex,
                        PhoneNumber = model.PhoneNumber,
                        HomeNumber = model.HomeNumber,
                        Email = model.Email,
                        Condition = model.Condition,
                        Doctor = userName
                    };
                    var data = await this.db.Patients.AddAsync(patient);
                    await this.db.SaveChangesAsync();

                    if (Path != null)
                    {
                        using (FileStream fstream = new FileStream(System.IO.Path.Combine(Path, patient.PatientId + ".dat"), FileMode.OpenOrCreate))
                        {
                            // запись массива байтов в файл
                            await fstream.WriteAsync(FileBytes, 0, FileBytes.Length);
                            Path = null;
                            FileBytes = null;
                        }
                    }

                    return this.Json("Created!");
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

        [Authorize]
        [HttpPost("Change")]
        public async Task<IActionResult> Change([FromBody]Patient model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var userName = this.userManager.GetUserName(this.User);
                    model.Doctor = userName;
                    try
                    {
                        var data = this.db.Patients
                            .Where(f => f.PatientId == model.PatientId)
                            .FirstOrDefault();

                        data.FirstName = model.FirstName;
                        data.LastName = model.LastName;
                        data.MiddleName = model.MiddleName;
                        data.Age = model.Age;
                        data.Weight = model.Weight;
                        data.Height = model.Height;
                        data.Sex = model.Sex;
                        data.PhoneNumber = model.PhoneNumber;
                        data.HomeNumber = model.HomeNumber;
                        data.Email = model.Email;
                        data.Condition = model.Condition;

                        if (Path != null)
                        {
                            System.IO.File.Delete(System.IO.Path.Combine(Path, model.PatientId + ".dat"));
                            using (FileStream fstream = new FileStream(System.IO.Path.Combine(Path, model.PatientId + ".dat"), FileMode.OpenOrCreate))
                            {
                                // запись массива байтов в файл
                                await fstream.WriteAsync(FileBytes, 0, FileBytes.Length);
                                Path = null;
                                FileBytes = null;
                            }
                        }

                        await this.db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return this.Json(new string[] { "Exception", ex.Message });
                    }

                    return this.Json("Created!");
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

        [Authorize(Roles = "SystemAdministrator")]
        [Authorize(Roles = "Doctor")]
        [HttpPost("UploadFile")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var file = this.Request.Form.Files[0];

                    if (System.IO.Path.GetExtension(file.FileName) != ".dat")
                    {
                        return this.Json(new string[] { "Exception", "Тільки файл з розширенням dat!" });
                    }

                    string folderName = "Upload";
                    string webRootPath = this.hostingEnvironment.WebRootPath;
                    string newPath = System.IO.Path.Combine(webRootPath, folderName);

                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }

                    if (file.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string fullPath = System.IO.Path.Combine(newPath, fileName);
                        Path = newPath;
                        using (var ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            FileBytes = ms.ToArray();
                        }
                    }
                    else
                    {
                        return this.Json(new string[] { "Exception", "Файл порожній!" });
                    }

                    return this.Json("Created!");
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

        [Authorize]
        [HttpGet("Get")]
        public IActionResult Get()
        {
            var userName = this.userManager.GetUserName(this.User);
            var patients = this.db.Patients.Where(f => f.Doctor.Contains(userName)).ToList();
            return this.Json(patients);
        }

        [Authorize(Roles = "SystemAdministrator,Doctor")]
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            string folderName = "Upload";
            string webRootPath = this.hostingEnvironment.WebRootPath;
            string path = System.IO.Path.Combine(webRootPath, folderName);

            var patient = this.db.Patients.First(f => f.PatientId == id);
            this.db.Patients.Remove(patient);
            this.db.SaveChanges();

            System.IO.File.Delete(System.IO.Path.Combine(path, id + ".dat"));

            return this.Json(new string[] { "Exception", id.ToString() });
        }
    }
}