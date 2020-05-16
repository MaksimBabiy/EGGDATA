namespace AdminSite.AzureBlobService
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Helpers;
    using AdminPanelInfrastructure.Helpers;
    using Microsoft.AspNetCore.Http;

    public interface IFileManager
    {
        Task<List<FileBlobModel>> AddAsync(IFormFileCollection files, string userId);

        Task CopyBlobAsync(string fileName, string userId);

        Task<bool> FileExistsAsync(string fileName, string userId);

        Task<FileBlobModel> AddAsync(MemoryStream file, string fileUrl, string userId);

        Task<AddingFileResult> DeleteAsync(string fileName, string userId);

        Task<List<FileBlobModel>> GetAsync();
    }
}
