using PaymentService.Models;
using System.Reflection;

namespace PaymentService.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly static List<Payment> _movies = PaymentSeed();

        private static List<Payment> PaymentSeed()
        {
            var result = new List<Payment>()
            {
                new Payment
                {
                    Id = 1,
                    Title = "Prasad",
                },
                new Payment
                {
                    Id = 2,
                    Title = "Praveen",
                },
                new Payment {
                    Id = 3,
                    Title = "Pramod",
                }
            };

            return result;
        }

        public Payment Get(int id)
        {
            return _movies[id];
        }

        public List<Payment> GetAll()
        {
            return _movies;
        }
    }
}
