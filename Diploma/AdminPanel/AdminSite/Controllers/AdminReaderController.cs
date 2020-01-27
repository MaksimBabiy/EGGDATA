namespace AdminSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using AdminPanelInfrastructure.Classes;
    using AdminPanelInfrastructure.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AdminReaderController : Controller
    {
        private static IFormFile _file { get; set; }

        private IHostingEnvironment hostingEnvironment;

        public AdminReaderController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        // [Authorize]
        [AllowAnonymous]
        [HttpPost("convert")]
        [DisableRequestSizeLimit]
        public IActionResult Convert()
        {
            try
            {
                string[] data;
                var file = this.Request.Form.Files[0];
                _file = file;

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

        // [Authorize]
        [AllowAnonymous]
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
                return this.Json(new string[] { "Exception", "Could not find a part of the path" });
            }

            return this.Json(data);
        }

        [AllowAnonymous]
        [HttpPost("Wave")]
        public IActionResult WaveletTransform([FromBody] string[] data)
        {
            double[] arr = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                arr[i] = System.Convert.ToDouble(data[i]);
            }

            Vector inp = new Vector(arr);

            // Быстрое вейвлет преобразование
            Vector inpNew = inp.CutAndZero(Vector.NextPow2(inp.N));
            List<double> sourceData = new List<double>();
            sourceData.AddRange(inpNew.DataInVector);
            double[] outp = Vector.DirectTransform(sourceData).ToArray();

            return this.Json(outp);
        }

        [HttpGet("data")]
        public Task Data()
        {
            this.HttpContext.Response.ContentType = "text/html; charset=utf-8";
            return this.HttpContext.Response.WriteAsync($"<h2 style=\"color: blue;\">Hopa?</h2>");
        }
    }
}