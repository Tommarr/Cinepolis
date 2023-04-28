using PaymentDomain.Models;
using System.Linq.Expressions;

namespace PaymentDomain.Repositories
{
    public interface IPaymentRepository
    {
        IQueryable<Payment> Entities { get; }

        Payment Add(Payment entity);
        IEnumerable<Payment> Find(Expression<Func<Payment, bool>> query);
        Payment Get(string id);
        IEnumerable<Payment> GetAll();
        void Remove(Payment entity);
        void Update(Payment entity);
    }
}