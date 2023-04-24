using PaymentService.Models;

namespace PaymentService.Repositories
{
    public interface IMovieRepository
    {
        List<Movie> GetAll();
        Movie Get(int id);
    }
}