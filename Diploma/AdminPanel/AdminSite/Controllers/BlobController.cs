namespace AdminSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Helpers;
    using AdminPanelDataBaseCore.Interfaces;
    using AdminPanelInfrastructure;
    using AdminSite.AzureBlobService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BlobController : BaseApiController
    {
        private readonly IAzureBlobService azureBlobService;

        private readonly IAdminRepositoryDb adminRepositoryDb;

        private readonly IFileManager fileManager;

        private readonly IOptions<AppSettings> options;

        public BlobController(IAzureBlobService azureBlobService, IOptions<AppSettings> options, IAdminRepositoryDb adminRepositoryDb, IFileManager fileManager)
            : base(adminRepositoryDb, options)
        {
            this.azureBlobService = azureBlobService;
            this.options = options;
            this.fileManager = fileManager;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFileAsync(IFormFileCollection formFiles)
        {
            if (!this.HttpContext.Request.HasFormContentType)
            {
                return this.BadRequest("Something error occurred");
            }

            if (formFiles.Count == 0)
            {
                formFiles = this.HttpContext.Request.Form.Files;
            }

            try
            {
                List<FileBlobModel> fileBlobModels = await this.fileManager.AddFilesAsync(formFiles, this.UserId);

                IActionResult result = this.Ok(new { Items = fileBlobModels });

                return result;

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        // index of file and quantity = 1
        [HttpGet]
        public async Task<IActionResult> GetBlobDataAsync(int fileIndex, int quantity)
        {
            return null;
            // Tuple Item1 = List<FileBlobModel> models
            //       Item2 = int totalItems = 1
            // Add method to get format: 
            // string.Foramat("{0}, {1}, {2}, {3}", this.AppSettings.BlobStorageUrl, this.AppSettings.ContainerName, this.UserId, model.FileId)
        }

        // Delete file from blob
        [HttpDelete("{fileName}")]
        public async Task<IActionResult> DeleteFileFromBlob([FromRoute] string blobFileName)
        {
            return null;
        }
    }
}
