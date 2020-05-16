namespace AdminSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Entities;
    using AdminPanelDataBaseCore.Interfaces;
    using AdminPanelInfrastructure;
    using AdminPanelInfrastructure.Helpers;
    using AdminSite.AzureBlobService;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [Route("api/[controller]")]
    // [Authorize]
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
            this.adminRepositoryDb = adminRepositoryDb;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(IFormFileCollection files)
        {
            if (!this.HttpContext.Request.HasFormContentType)
            {
                return this.BadRequest("Unsupported media type");
            }

            if (files.Count == 0)
            {
                files = this.HttpContext.Request.Form.Files;
            }

            try
            {
                List<FileBlobModel> addedFiles = await this.fileManager.AddAsync(files, this.UserId).ConfigureAwait(false);

                IActionResult result = this.Ok(new { Items = addedFiles });

                return result;
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFilesAsync(int index, int count)
        {
            try
            {
                (List<FileStorage> items, int totalAmount) result = await this.adminRepositoryDb.GetFileStoragesAsync(this.UserId, index, count).ConfigureAwait(false);

                if (result.items != null)
                {
                    List<FileBlobModel> images = result.ToTuple().Item1
                        .Select(i => new FileBlobModel(
                            i.FileId,
                            this.GetFileStorageUrl(this.UserId, i.FileId),
                            i.Title,
                            i.CreatedDate))
                        .ToList();

                    return this.Ok(new { Items = images, TotalAmount = result.totalAmount });
                }

                return this.BadRequest("Error retrieving user images");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        public string GetFileStorageUrl(string userId, string fileId)
        {
            if (string.IsNullOrEmpty(this.options.Value.BlobStorageUrl))
            {
                throw new Exception("Blob url not found.");
            }
            else if (string.IsNullOrEmpty(this.options.Value.BlobStorage))
            {
                throw new Exception("Blob storage not found.");
            }

            return string.Format("{0}{1}/{2}/{3}", this.options.Value.BlobStorageUrl, this.options.Value.ContainerName, userId, fileId);
        }

        [HttpDelete("{blobName}")]
        public async Task<IActionResult> DeleteFile([FromRoute] string blobName)
        {
            var existingFile = await this.adminRepositoryDb.GetFileAsync(blobName).ConfigureAwait(false);

            if (Equals(existingFile, null))
            {
                return this.BadRequest($"File with {blobName} name not found");
            }

            if (!await this.fileManager.FileExistsAsync(blobName, existingFile.UserId).ConfigureAwait(false))
            {
                return this.NotFound();
            }

            var result = await this.fileManager.DeleteAsync(blobName, existingFile.UserId).ConfigureAwait(false);

            if (result.Successful)
            {
                try
                {
                    bool isFileDeleted = await this.adminRepositoryDb.DeleteFileAsync(blobName, this.UserId).ConfigureAwait(false);

                    if (isFileDeleted)
                    {
                        return this.Ok();
                    }

                    return this.BadRequest($"Error deleting {blobName} file");
                }
                catch (Exception ex)
                {
                    return this.BadRequest(ex.Message);
                }
            }
            else
            {
                return this.BadRequest(result.Message);
            }
        }
    }
}
