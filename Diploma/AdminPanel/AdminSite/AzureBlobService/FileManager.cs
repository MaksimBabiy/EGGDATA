namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Helpers;
    using AdminPanelInfrastructure;
    using AdminPanelInfrastructure.Helpers;
    using AdminSite.AzureBlobService;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Microsoft.WindowsAzure.Storage.Blob;

    // TODO: Review which need filetype of files(FileModel).
    public class FileManager : IFileManager
    {
        public FileManager(IOptions<AppSettings> appSettings, IAzureBlobService azureBlobManager)
        {
            this.AppSettings = appSettings.Value;
            this.BlobManager = azureBlobManager;
        }

        private AppSettings AppSettings { get; }

        private IAzureBlobService BlobManager { get; }

        public async Task<List<FileBlobModel>> GetAsync()
        {
            try
            {
                if (!await this.BlobManager.CloudBlobContainer.ExistsAsync().ConfigureAwait(false))
                {
                    await this.BlobManager.CloudBlobContainer.CreateAsync(BlobContainerPublicAccessType.Blob, null, null).ConfigureAwait(false);
                }

                var files = new List<FileBlobModel>();

                BlobContinuationToken token = null;
                do
                {
                    BlobResultSegment resultSegment = await this.BlobManager.CloudBlobContainer.ListBlobsSegmentedAsync(token).ConfigureAwait(false);
                    token = resultSegment.ContinuationToken;

                    foreach (IListBlobItem item in resultSegment.Results.Where(bi => bi.Parent.GetBlockBlobReference(bi.Parent.Container.Name).GetType() == typeof(CloudBlockBlob))
                        .Select(r => r.Parent.GetBlockBlobReference(r.Parent.Container.Name)))
                    {
                        CloudBlockBlob blobItem = (CloudBlockBlob)item;

                        await blobItem.FetchAttributesAsync().ConfigureAwait(false);

                        files.Add(new FileBlobModel
                        {
                            FileId = blobItem.Name,
                            Size = blobItem.Properties.Length / 1024,
                            CreatedDate = blobItem.Metadata["Created"] == null ? DateTime.Now : DateTime.Parse(blobItem.Metadata["Created"]),
                            FileUrl = blobItem.Uri.AbsoluteUri
                        });
                    }
                }
                while (token != null);

                return files;
            }
            catch
            {
                throw;
            }
        }

        public async Task<AddingFileResult> DeleteAsync(string fileName, string userId)
        {
            string path = this.BuildPath(userId, fileName);

            try
            {
                await this.BlobManager.DeleteAsync(path).ConfigureAwait(false);

                return new AddingFileResult { Successful = true, Message = $"{fileName} deleted successfully" };
            }
            catch (Exception ex)
            {
                return new AddingFileResult { Successful = false, Message = $"Error deleting {fileName} file: {ex.GetBaseException().Message}" };
            }
        }

        public async Task<List<FileBlobModel>> AddAsync(IFormFileCollection files, string userId)
        {
            try
            {
                List<FileBlobModel> addedFiles = new List<FileBlobModel>();

                foreach (IFormFile file in files)
                {
                    string fileId = string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(file.Name));

                    string fileName = this.BuildPath(userId, fileId);

                    BlobMetaData blob;

                    using (var fileStream = file.OpenReadStream())
                    {
                        blob = await this.BlobManager.AddAsync(fileStream, fileName).ConfigureAwait(false);
                    }

                    if (blob != null)
                    {
                        addedFiles.Add(new FileBlobModel(fileId, blob));
                    }
                }

                return addedFiles;
            }
            catch
            {
                throw;
            }
        }

        public async Task<FileBlobModel> AddAsync(MemoryStream file, string fileUrl, string userId)
        {
            try
            {
                string fileId = string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(fileUrl));

                string fileName = this.BuildPath(userId, fileId);

                BlobMetaData blob = await this.BlobManager.AddAsync(file, fileName).ConfigureAwait(false);

                if (blob != null)
                {
                    return new FileBlobModel(fileId, blob);
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task CopyBlobAsync(string fileName, string userId)
        {
            string path = this.BuildPath(userId, fileName);

            string newPath = this.BuildPath(userId, fileName);

            CloudBlockBlob blobCopy = this.BlobManager.CloudBlobContainer.GetBlockBlobReference(newPath);

            if (!await blobCopy.ExistsAsync().ConfigureAwait(false))
            {
                CloudBlockBlob blob = this.BlobManager.CloudBlobContainer.GetBlockBlobReference(path);

                if (await blob.ExistsAsync().ConfigureAwait(false))
                {
                    await blobCopy.StartCopyAsync(blob).ConfigureAwait(false);

                    await blob.DeleteIfExistsAsync().ConfigureAwait(false);
                }
            }
            else
            {
                throw new Exception($"Blob {newPath} is aready exists");
            }
        }

        public Task<bool> FileExistsAsync(string fileName, string userId)
        {
            string path = this.BuildPath(userId, fileName);

            return this.BlobManager.FileExistsAsync(path);
        }

        #region Private methods

        private string BuildPath(string userId, string fileId)
        {
            return string.Format("{0}/{1}", userId, fileId);
        }

        #endregion
    }
}