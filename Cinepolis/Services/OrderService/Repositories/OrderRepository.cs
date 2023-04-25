using OrderService.Models;
using System.Reflection;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly static List<Order> _movies = PaymentSeed();

        private static List<Order> PaymentSeed()
        {
            var result = new List<Order>()
            {
                new Order
                {
                    Id = 1,
                    Title = "Prasad",
                },
                new Order
                {
                    Id = 2,
                    Title = "Praveen",
                },
                new Order {
                    Id = 3,
                    Title = "Pramod",
                }
            };

            return result;
        }

        public Order Get(int id)
        {
            return _movies[id];
        }

        public List<Order> GetAll()
        {
            return _movies;
        }
    }
}
