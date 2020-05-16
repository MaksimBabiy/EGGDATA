namespace AdminPanelInfrastructure.Helpers
{
    using System;

    public class FileBlobModel
    {
        public FileBlobModel()
        {
        }

        public FileBlobModel(string fileId, BlobMetaData blob)
        {
            this.FileId = fileId;
            this.CreatedDate = blob.Created;
            this.FileUrl = blob.Url;
        }

        public FileBlobModel(string fileId, string url, string title, DateTime createdDate)
        {
            this.FileId = fileId;
            this.Title = title;
            this.FileUrl = url;
            this.CreatedDate = createdDate;
        }

        public string FileId { get; set; }

        public string Title { get; set; }

        public string FileUrl { get; set; }

        public long Size { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}