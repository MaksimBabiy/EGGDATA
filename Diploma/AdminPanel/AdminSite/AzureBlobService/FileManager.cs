namespace AdminSite.AzureBlobService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Helpers;
    using AdminPanelInfrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    public class FileManager : IFileManager
    {
        private readonly IOptions<AppSettings> options;

        private readonly IAzureBlobService azureBlobService;

        public FileManager(IOptions<AppSettings> options, IAzureBlobService azureBlobService)
        {
            this.azureBlobService = azureBlobService;
            this.options = options;
        }

        public async Task<List<FileBlobModel>> AddFilesAsync(IFormFileCollection formFiles, string userId)
        {
            userId = userId ?? throw new ArgumentNullException(nameof(userId));

            try
            {
                List<FileBlobModel> blobTaskHelpers = new List<FileBlobModel>();

                foreach (IFormFile item in formFiles)
                {
                    string fileId = string.Format("{0},{1}", Guid.NewGuid().ToString(), Path.GetExtension(item.Name));

                    string fileName = this.BuidPath(userId, fileId);

                    BlobTaskHelper blobTaskHelper;

                    using (var fileStreamData = item.OpenReadStream())
                    {
                        blobTaskHelper = await this.azureBlobService.AddAsync(fileStreamData, fileName);
                    }

                    if (blobTaskHelper != null)
                    {
                        blobTaskHelpers.Add(new FileBlobModel(fileId, blobTaskHelper));
                    }
                }

                return blobTaskHelpers;
            }
            catch
            {
                throw;
            }
        }

        #region PrivateMethods
        private string BuidPath(string userId, string fileId)
        {
            return string.Format("{0}/{1}", userId, fileId);
        }
        #endregion
    }
}
