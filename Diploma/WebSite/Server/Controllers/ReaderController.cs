namespace Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
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
                List<string> data;
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
                    string f = (string)stream.ToString();
                    using (FileStream reader = new FileStream(f, FileMode.Open))
                    {

                        data = null;
                    }
                }

                return this.Json(data);
            }
            catch (System.Exception ex)
            {
                return this.Json(new string[] { "Exception", ex.Message });
            }
        }

        //[Authorize]
        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            List<string> data = new List<string>();

            string folderName = "Upload";
            string webRootPath = this.hostingEnvironment.WebRootPath;
            string path = Path.Combine(webRootPath, folderName);
            try
            {
                using (FileStream stream = new FileStream(Path.Combine(path, id + ".dat"), FileMode.Open))
                {
                    try
                    {
                        data = Reader.GetData(stream);
                    }
                    catch (Exception ex)
                    {
                        return this.Json(new string[] { "Inner Exception", ex.Message });
                    }
                }
            }
            catch (Exception ex)
            {
                
                return this.Json(new string[] { "Outer Ex", ex.Message });
            }
            this.Response.Headers.Add("Vary", "Accept-Encoding");
            this.Response.Headers.Add("Content-type", "text/plain");

            
            return this.Json(data);
        }

        [HttpGet("GetPeaks/{id}")]
        public IActionResult GetPeaks(int id)
        {
            List<string> peaks = new List<string>();

            string folderName = "Upload";
            string webRootPath = this.hostingEnvironment.WebRootPath;
            
            string path = Path.Combine(webRootPath, folderName);
            string path2 = Path.Combine(this.hostingEnvironment.ContentRootPath, "Rpeaks.txt");

            path = Path.Combine(path, id + ".dat");
            try
            {
                using (StreamReader stream = new StreamReader(path))
                {
                    try
                    {
                        PeaksDetection.GetRPeaks(path);
                        peaks = PeaksDetection.FindRPeaks(path2);
                    }
                    catch (Exception ex)
                    {
                        return this.Json(new string[] { "Inner Exception", ex.Message });
                    }
                }
            }
            catch (Exception ex)
            {

                return this.Json(new string[] { "Outer Ex", ex.Message });
            }
            this.Response.Headers.Add("Vary", "Accept-Encoding");
            this.Response.Headers.Add("Content-type", "text/plain");


            return this.Json(peaks);
        }

        [HttpGet("data")]
        public Task Data()
        {
            this.HttpContext.Response.ContentType = "text/html; charset=utf-8";
            return this.HttpContext.Response.WriteAsync($"<h2 style=\"color: blue;\">Hopa?</h2>");
        }
    }
}