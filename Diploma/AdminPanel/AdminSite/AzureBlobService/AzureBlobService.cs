namespace AdminSite.AzureBlobService
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Helpers;
    using AdminPanelInfrastructure;
    using Microsoft.Extensions.Options;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class AzureBlobService : IAzureBlobService
    {
        public AzureBlobService(IOptions<AppSettings> options)
        {
            this.CloudStorageAccount = CloudStorageAccount.Parse(options.Value.BlobStorage);
            this.ContainerName = options.Value.ContainerName;
            CloudBlobClient cloudBlobClient = this.CloudStorageAccount.CreateCloudBlobClient();
            this.CloudBlobContainer = cloudBlobClient.GetContainerReference(this.ContainerName);
        }

        public CloudBlobContainer CloudBlobContainer { get; }

        private CloudStorageAccount CloudStorageAccount { get; }

        private string ContainerName { get; }

        public async Task<BlobTaskHelper> AddAsync(Stream file, string fileName)
        {
            try
            {
                await this.CloudBlobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);

                CloudBlockBlob cloudBlockBlob = this.CloudBlobContainer.GetBlockBlobReference(fileName);

                cloudBlockBlob.Metadata["Created"] = DateTime.Now.ToString();

                await cloudBlockBlob.UploadFromStreamAsync(file).ConfigureAwait(false);

                await cloudBlockBlob.FetchAttributesAsync().ConfigureAwait(false);

                return this.CreateDataAsync(cloudBlockBlob);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(string fileName)
        {
            try
            {
                await this.CloudBlobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);

                ICloudBlob blob = await this.CloudBlobContainer.GetBlobReferenceFromServerAsync(fileName).ConfigureAwait(false);

                await blob.DeleteAsync().ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        private BlobTaskHelper CreateDataAsync(CloudBlockBlob cloudBlockBlob)
        {
            return new BlobTaskHelper
            {
                FileName = cloudBlockBlob.Name,
                Size = cloudBlockBlob.Properties.Length / 1024,
                CreatedDate = !cloudBlockBlob.Metadata.ContainsKey("Created") || cloudBlockBlob.Metadata["Created"] == null ? DateTime.Now : DateTime.Parse(cloudBlockBlob.Metadata["Created"]),
                UpdatedDate = ((DateTimeOffset)cloudBlockBlob.Properties.LastModified).DateTime,
                Url = cloudBlockBlob.Uri.AbsoluteUri
            };
        }
    }
}
