using Microsoft.VisualBasic.FileIO;

namespace MovieApi.Models
{
    public class FileUploadModel
    {
        public IFormFile data { get; set; }
        public string fileName { get; set; }
    }
}
