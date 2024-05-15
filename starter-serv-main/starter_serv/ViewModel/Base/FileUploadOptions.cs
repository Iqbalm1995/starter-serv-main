namespace starter_serv.ViewModel
{
    public class FileUploadOptions
    {
        public string FilePathUpload = "wwwroot\\upload\\files";

        public string FilePathUploadProjectAttachments = "wwwroot\\upload\\files\\ProjectAttachments";

        public string FilePathUploadTaskAttachments = "wwwroot\\upload\\files\\TaskAttachments";

        public string FilePathUploadAvatarUsers = "wwwroot\\upload\\files\\AvatarUsers";

        public List<string> AllowedUploadTypes = new List<string>()
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".bpm",
            ".xls",
            ".xlsx",
            ".doc",
            ".docx",
            ".pdf",
            ".txt"
        }; 
        
        public List<string> AllowedUploadTypesImageOnly = new List<string>()
        {
            ".jpg",
            ".jpeg",
            ".png"
        };

        public Int64 MaxFileSize = 8388608; // 8 MB = 8388608 bytes

    }
}
