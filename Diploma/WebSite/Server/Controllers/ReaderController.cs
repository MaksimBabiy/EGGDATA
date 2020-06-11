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
                        //читаем все точки из файла и заносим в список
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
            
            try
            {
                using (StreamReader stream = new StreamReader(path))
                {
                    try
                    {
                        //Получаем пики и заносим их в список
                        PeaksDetection.GetRPeaks(path);
                        peaks = PeaksDetection.RPeaksToList(path2);
                    }
                    catch (Exception ex)
                    {
                        return this.Json(new string[] { "Cannot get peaks!", ex.InnerException.Message });
                    }
                }
            }
            catch (Exception ex)
            {

                return this.Json(new string[] { "Cannot open file!", ex.Message });
            }

            //заносим результаты корреляции в список
            List<double> CorrelationRes = Correlation.CorrelationPoints(data, peaks);

            //модель ответа пользователю
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