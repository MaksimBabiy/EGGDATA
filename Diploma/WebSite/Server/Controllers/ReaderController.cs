namespace Server.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Server.Infrastructure.Classes;

    [Produces("application/json")]
    [Route("api/reader")]
    public class ReaderController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public ReaderController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        private static IFormFile FileData { get; set; }

        [Authorize]
        [HttpPost("convert")]
        [DisableRequestSizeLimit]
        public IActionResult Convert()
        {
            try
            {
                string[] data;
                var file = this.Request.Form.Files[0];
                FileData = file;

                var filePath = Path.GetTempFileName();

                if (file == null)
                {
                    throw new Exception("File is null");
                }

                if (file.Length == 0)
                {
                    throw new Exception("File is empty");
                }

                using (Stream stream = file.OpenReadStream())
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        data = Reader.GetData(reader);
                    }
                }

                return this.Json(data);
            }
            catch (System.Exception ex)
            {
                return this.Json(new string[] { "Exception", ex.Message });
            }
        }

        [Authorize]
        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            string[] data;

            string folderName = "Upload";
            string webRootPath = this.hostingEnvironment.WebRootPath;
            string path = Path.Combine(webRootPath, folderName);
            try
            {
                using (Stream stream = System.IO.File.OpenRead(Path.Combine(path, id + ".dat")))
                {
                    try
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            data = Reader.GetData(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        return this.Json(new string[] { "Exception", ex.Message });
                    }
                }
            }
            catch (Exception ex)
            {
                return this.Json(new string[] { "Exception", ex.Message });
            }

            return this.Json(data);
        }

        [HttpGet("data")]
        public Task Data()
        {
            this.HttpContext.Response.ContentType = "text/html; charset=utf-8";
            return this.HttpContext.Response.WriteAsync($"<h2 style=\"color: blue;\">Hopa?</h2>");
        }
    }
}