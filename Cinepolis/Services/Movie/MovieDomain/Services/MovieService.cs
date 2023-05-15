using Microsoft.Extensions.Logging;
using MovieDomain.Models;

using Firebase.Storage;
using Firebase.Auth.Providers;
using Google.Cloud.Storage.V1;

namespace MovieDomain.Services
{
    public class MovieService : IMovieService
    {
        private readonly ILogger<MovieService> _logger;
        private readonly string bucketName = "";
        private readonly string localPath = "";
        private readonly string objectName = "";

        public MovieService(ILogger<MovieService> logger)
        {
            _logger = logger;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            var storage = StorageClient.Create();
            using var fileStream = File.OpenRead(localPath);
            storage.UploadObject(bucketName, objectName, null, fileStream);
            Console.WriteLine($"Uploaded {objectName}.");
            return movie;
        }






    }
}
