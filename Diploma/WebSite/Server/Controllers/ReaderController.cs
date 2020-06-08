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
    using Server.Infrastructure.Models;

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
        //[HttpGet("Get/{id}")]
        //public IActionResult Get(int id)
        //{
        //    List<string> data = new List<string>();

        //    string folderName = "Upload";
        //    string webRootPath = this.hostingEnvironment.WebRootPath;
        //    string path = Path.Combine(webRootPath, folderName);
        //    try
        //    {
        //        using (FileStream stream = new FileStream(Path.Combine(path, id + ".dat"), FileMode.Open))
        //        {
        //            try
        //            {
        //                //data - все точки
        //                data = Reader.GetData(stream);
        //            }
        //            catch (Exception ex)
        //            {
        //                return this.Json(new string[] { "Inner Exception", ex.Message });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
                
        //        return this.Json(new string[] { "Outer Ex", ex.Message });
        //    }
        //    this.Response.Headers.Add("Vary", "Accept-Encoding");
        //    this.Response.Headers.Add("Content-type", "text/plain");

            
        //    return this.Json(data);
        //}

        //[HttpGet("GetPeaks/{id}")]
        //public IActionResult GetPeaks(int id)
        //{
        //    //peaks - все пики
        //    List<string> peaks = new List<string>();

        //    string folderName = "Upload";
        //    string webRootPath = this.hostingEnvironment.WebRootPath;
            
        //    string path = Path.Combine(webRootPath, folderName);
        //    string path2 = Path.Combine(this.hostingEnvironment.ContentRootPath, "Rpeaks.txt");

        //    path = Path.Combine(path, id + ".dat");
        //    try
        //    {
        //        using (StreamReader stream = new StreamReader(path))
        //        {
        //            try
        //            {
        //                PeaksDetection.GetRPeaks(path);
        //                peaks = PeaksDetection.RPeaksToList(path2);
        //            }
        //            catch (Exception ex)
        //            {
        //                return this.Json(new string[] { "Inner Exception", ex.Message });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return this.Json(new string[] { "Outer Ex", ex.Message });
        //    }
        //    this.Response.Headers.Add("Vary", "Accept-Encoding");
        //    this.Response.Headers.Add("Content-type", "text/plain");


        //    return this.Json(peaks);
        //}

        [HttpGet("data")]
        public Task Data()
        {
            this.HttpContext.Response.ContentType = "text/html; charset=utf-8";
            return this.HttpContext.Response.WriteAsync($"<h2 style=\"color: blue;\">Hopa?</h2>");
        }

        //[HttpGet("fuck/{id}")]
        //public IActionResult Cor(int id)
        //{
        //    //ВСЕ ТОЧКИ
        //    List<string> data = new List<string>();

        //    string folderName = "Upload";
        //    string webRootPath = this.hostingEnvironment.WebRootPath;
        //    string path = Path.Combine(webRootPath, folderName);

        //    //ПИКИ
        //    List<string> peaks = new List<string>();
        //    string path2 = Path.Combine(this.hostingEnvironment.ContentRootPath, "Rpeaks.txt");

           

        //    using (FileStream stream = new FileStream(Path.Combine(path, id + ".dat"), FileMode.Open))
        //    {
        //        try
        //        {
        //            //data - все точки
        //            data = Reader.GetData(stream);
        //        }
        //        catch (Exception ex)
        //        {
        //            return this.Json(new string[] { "Inner Exception", ex.Message });
        //        }
        //    }

        //    path = Path.Combine(path, id + ".dat");

        //    using (StreamReader stream = new StreamReader(path))
        //    {
        //        try
        //        {
        //            PeaksDetection.GetRPeaks(path);
        //            peaks = PeaksDetection.RPeaksToList(path2);
        //        }
        //        catch (Exception ex)
        //        {
        //            return this.Json(new string[] { "Inner Exception", ex.Message });
        //        }
        //    }

        //    return this.Json(Correlation.CorrelationPoints(data, peaks));

        //}

        [HttpGet("GetData/{id}")]
        public IActionResult Show(int id)
        {


            //все точки
            List<string> data = new List<string>();
            //все пики
            List<string> peaks = new List<string>();
            //папка хранения файлов
            string folderName = "Upload";
            string webRootPath = this.hostingEnvironment.WebRootPath;
            //путь к этой папке
            string path = Path.Combine(webRootPath, folderName);
            //путь файла
            path = Path.Combine(path, id + ".dat");

            //достаем данные из датовского файла
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    try
                    {
                        //Correlation.Pearson(new List<double> { 1, 2, 3 }, new List<double> { 1, 2 });
                        data = Reader.GetData(stream);
                    }
                    catch (Exception ex)
                    {
                        return this.Json(new string[] { "Cannot convert data!", ex.Message });
                    }
                }
            }
            catch (Exception ex)
            {

                return this.Json(new string[] { "Cannot open file!", ex.Message });
            }

            //путь файла с пиками
            string path2 = Path.Combine(this.hostingEnvironment.ContentRootPath, "Rpeaks.txt");
            //заносим пики в список
            try
            {
                using (StreamReader stream = new StreamReader(path))
                {
                    try
                    {
                        PeaksDetection.GetRPeaks(path);
                        peaks = PeaksDetection.RPeaksToList(path2);
                    }
                    catch (Exception ex)
                    {
                        return this.Json(new string[] { "Cannot get peaks!", ex.Message });
                    }
                }
            }
            catch (Exception ex)
            {

                return this.Json(new string[] { "Cannot open file!", ex.Message });
            }

            List<double> CorrelationRes = Correlation.CorrelationPoints(data, peaks);

            JsonResultModel model = new JsonResultModel
            {
                Points = data,
                Peaks = peaks,
                CorelationResult = CorrelationRes
            };

            return this.Json(model);

        }
    }
}