namespace AdminSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Helpers;
    using AdminPanelDataBaseCore.Interfaces;
    using AdminPanelInfrastructure;
    using AdminSite.AzureBlobService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.WindowsAzure.Storage.Blob;

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

            try
            {
                string blobFileName = string.Format("{0},{1}", Guid.NewGuid().ToString(), Path.GetExtension(fileIndex.ToString()));
                this.azureBlobService.CloudBlobContainer.GetBlockBlobReference(blobFileName);
                IActionResult result = this.Ok(200);
                return result;
            }
            catch
            {
                return this.BadRequest();
            }
            // Tuple Item1 = List<FileBlobModel> models
            //       Item2 = int totalItems = 1
            // Add method to get format: 
            // string.Foramat("{0}, {1}, {2}, {3}", this.AppSettings.BlobStorageUrl, this.AppSettings.ContainerName, this.UserId, model.FileId)
        }

        // Delete file from blob
        [HttpDelete("{fileName}")]
        public async Task<IActionResult> DeleteFileFromBlob([FromRoute] string blobFileName)
        {
            //var blobs = await ListBlobASync(container);
            //var files = blobs.Cast<CloudBlockBlob>().Select(item => item.Name).ToList();

            //if (files.Any(item => item == blobFileName))
            //{
            //    var blob = azureBlobService.CloudBlobContainer.GetBlockBlobReference(blobFileName);
            //    await blob.DeleteIfExistsAsync();
            //}

            try
            {
                await this.azureBlobService.CloudBlobContainer.GetBlockBlobReference(blobFileName).DeleteIfExistsAsync();
                IActionResult result = this.Ok(200);
                return result;
            }
            catch
            {
                return this.BadRequest();
            }

            /*
             * 1) Ищем файл: FileExistsAsync(blobFileName, this.UserId)
             * 2) result = DeleteFileDataAsync(blobFileName, this.UserId) ---- AzureBlobService => если результат успешен, то пункт 3
             * 3) DeleteFile(blobFileName, this.UserId) ----- FileManager
             * 4) Добавляем проверки
             */
        }

        //public async Task<List<IListBlobItem>> ListBlobASync(CloudBlobContainer container)
        //{
        //    BlobContinuationToken continuationToken = null;
        //    var results = new List<IListBlobItem>();
        //    do
        //    {
        //        var responce = await container.ListBlobsSegmentedAsync(continuationToken);
        //        continuationToken = responce.ContinuationToken;
        //        results.AddRange(responce.Results);
        //    }
        //    while (continuationToken != null);

        //    return results;
        //}
    }
}
