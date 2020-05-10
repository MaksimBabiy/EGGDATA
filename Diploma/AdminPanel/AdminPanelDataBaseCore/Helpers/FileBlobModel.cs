namespace AdminPanelDataBaseCore.Helpers
{
    using System;

    public class FileBlobModel
    {
        public FileBlobModel()
        {

        }

        public FileBlobModel(string fileId, BlobTaskHelper blobTaskHelper)
        {
            this.FileId = fileId;
            this.CreatedDate = blobTaskHelper.CreatedDate;
            this.Url = blobTaskHelper.Url;
        }

        public string FileId { get; set; }

        public string FileName { get; set; }

        public long Size { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Url { get; set; }
    }
}
