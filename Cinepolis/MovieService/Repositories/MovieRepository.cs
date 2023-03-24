using MovieService.Models;
using System.Reflection;

namespace MovieService.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly static List<Movie> _movies = MoviesSeed();

        private static List<Movie> MoviesSeed()
        {
            var result = new List<Movie>()
            {
                new Movie
                {
                    Id = 1,
                    Title = "Prasad",
                },
                new Movie
                {
                    Id = 2,
                    Title = "Praveen",
                },
                new Movie {
                    Id = 3,
                    Title = "Pramod",
                }
            };

            return result;
        }

        public Movie Get(int id)
        {
            return _movies[id];
        }

        public List<Movie> GetAll()
        {
            return _movies;
        }
    }
}
