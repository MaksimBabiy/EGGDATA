namespace AdminSite.AzureBlobService
{
    using System.IO;
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Helpers;
    using Microsoft.WindowsAzure.Storage.Blob;

    public interface IAzureBlobService
    {
        CloudBlobContainer CloudBlobContainer { get; }

        Task<BlobTaskHelper> AddAsync(Stream file, string fileName);


    }
}
