namespace AdminSite.AzureBlobService
{
    using System.IO;
    using System.Threading.Tasks;
    using AdminPanelInfrastructure.Helpers;
    using Microsoft.WindowsAzure.Storage.Blob;

    public interface IAzureBlobService
    {
        CloudBlobContainer CloudBlobContainer { get; }

        Task<BlobMetaData> AddAsync(Stream file, string fileName);

        Task DeleteAsync(string fileName);

        Task<BlobMetaData> UpdateAsync(MemoryStream stream, string blobName);

        Task<bool> FileExistsAsync(string blobName);
    }
}
