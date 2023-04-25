using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IMovieRepository
    {
        List<Movie> GetAll();
        Movie Get(int id);
    }
}