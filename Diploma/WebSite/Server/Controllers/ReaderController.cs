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
    using Server.DataBaseCore.SignatureFolderData;
    using Server.Infrastructure.Classes;
    using Server.Infrastructure.Logic;
    using Server.Infrastructure.Models;
    using RPeaksRecognizeDLL;
    using System.Security.Claims;

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
            try
            {
                //все точки
                List<string> data = GetData.ReadFile(id, this.hostingEnvironment);
                //все пики
                List<string> peaks = GetData.GetRPeaks(id, this.hostingEnvironment);
                //все результаті корреляции
                List<double> correlationRes = Correlation.CorrelationPoints(data, peaks);

                //модель ответа пользователю
                JsonResultModel model = new JsonResultModel
                {
                    Points = data,
                    Peaks = peaks,
                    CorelationResult = correlationRes
                };

                return this.Json(model);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("add/{id}")]
        public async Task<IActionResult> AddFile(IFormFile file, int id)
        {
            if (file != null)
            {

                string path = "/Upload/" + id + ".dat";
                using (var fileStream = new FileStream(this.hostingEnvironment.WebRootPath + path, FileMode.Create))
                {

                    await file.CopyToAsync(fileStream);
                }

                return this.Ok("File has been uploaded");
            }

            return this.BadRequest("Cannot upload file!");

        }
    }
}