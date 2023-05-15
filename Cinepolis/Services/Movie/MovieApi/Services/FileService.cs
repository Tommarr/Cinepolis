using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;
using MovieApi.Models;
using System.Text;

namespace MovieApi.Services
{
    public class FileService : IFileService
    {
        private readonly GCSConfigOptions _options;
        private readonly ILogger<FileService> _logger;
        private readonly GoogleCredential _googleCredential;

        public FileService(IOptions<GCSConfigOptions> options, ILogger<FileService> logger)
        {
            _options = options.Value;
            _logger = logger;
            try
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (environment == Environments.Production)
                {
                    // Store the json file in Secrets.
                    _googleCredential = GoogleCredential.FromJson(_options.GCPStorageAuthFile);
                }
                else
                {
                    _googleCredential = GoogleCredential.FromFile(_options.GCPStorageAuthFile);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                throw;
            }
        }

        public async Task<string> StoreFile(IFormFile fileToUpload, string fileNameToSave)
        {
            try
            {
                _logger.LogInformation($"Uploading: file {fileNameToSave} to storage {_options.GoogleCloudStorageBucketName}");
                using (var memoryStream = new MemoryStream())
                {
                    await fileToUpload.CopyToAsync(memoryStream);
                    // Create Storage Client from Google Credential
                    using (var storageClient = StorageClient.Create(_googleCredential))
                    {
                        // upload file stream
                        var uploadedFile = await storageClient.UploadObjectAsync(_options.GoogleCloudStorageBucketName, fileNameToSave, fileToUpload.ContentType, memoryStream);
                        _logger.LogInformation($"Uploaded: file {fileNameToSave} to storage {_options.GoogleCloudStorageBucketName}");
                        return uploadedFile.MediaLink;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while uploading file {fileNameToSave}: {ex.Message}");
                throw;
            }
        }

        public Stream GetFile(string fileName)
        {
            var storageClient = StorageClient.Create(_googleCredential);
            Stream stream = new MemoryStream();


            // convert to string


            //string downloadFilePath = Path.Combine("C:\\Users\\tomma\\Downloads", fileName);  // Replace "file.txt" with the actual file name
            //FileStream fs = new FileStream(downloadFilePath, FileMode.Create);

            //using var outputFile = File.OpenWrite("C:\\Users\\tomma\\Downloads");


            storageClient.DownloadObject(_options.GoogleCloudStorageBucketName, fileName, stream);
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();


            Console.WriteLine($"The contents of {fileName} from bucket {_options.GoogleCloudStorageBucketName} are downloaded");
            return stream;

        }
    }
}
