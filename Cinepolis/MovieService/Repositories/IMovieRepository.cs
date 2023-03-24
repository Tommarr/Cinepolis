using MovieService.Models;

namespace MovieService.Repositories
{
    public interface IMovieRepository
    {
        List<Movie> GetAll();
        Movie Get(int id);
    }
}