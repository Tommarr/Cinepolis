using PaymentApi.Context;
using System.Linq.Expressions;
using PaymentDomain.Repositories;
using PaymentDomain.Models;

namespace OrderApi.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext _Context;
        private readonly ILogger<PaymentRepository> _logger;
        public IQueryable<Payment> Entities => _Context.Set<Payment>();

        public PaymentRepository(ILogger<PaymentRepository> logger, PaymentContext orderContext)
        {
            _logger = logger;
            _Context = orderContext;
        }

        public Payment Add(Payment entity)
        {
            _Context.Set<Payment>().Add(entity);
            _Context.SaveChanges();
            return entity;
        }

        public IEnumerable<Payment> Find(Expression<Func<Payment, bool>> query)
        {
            return _Context.Set<Payment>().Where(query);
        }

        public Payment Get(string id)
        {
            return _Context.Set<Payment>().Find(id);
        }

        public IEnumerable<Payment> GetAll()
        {
            return _Context.Set<Payment>();
        }

        public void Remove(Payment entity)
        {
            _Context.Set<Payment>().Remove(entity);
        }

        public void Update(Payment entity)
        {
            _Context.Set<Payment>().Update(entity);
        }
    }
}
