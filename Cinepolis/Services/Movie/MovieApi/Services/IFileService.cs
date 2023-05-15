namespace MovieApi.Services
{
    public interface IFileService
    {
        Task<string> StoreFile(IFormFile fileToUpload, string fileNameToSave);
        public Stream GetFile(string fileName);
    }
}