namespace AdminSite.AzureBlobService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Helpers;
    using Microsoft.AspNetCore.Http;

    public interface IFileManager
    {
        Task<List<FileBlobModel>> AddFilesAsync(IFormFileCollection formFiles, string userId);

        Task<AddingFileResult> DeleteAsync(string fileName, string userId);
    }
}
