using Google.Cloud.Storage.V1;
using MovieDomain.Models;
using System.Security.AccessControl;

namespace MovieDomain.Services
{
    public interface IMovieService
    {
        public Task<Movie> CreateMovie(Movie movie);
    }
}