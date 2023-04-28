using Microsoft.EntityFrameworkCore;
using PaymentDomain.Models;

namespace PaymentApi.Context
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options)
            : base(options)
        {

        }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Order> Orders { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().HasData(new Payment());
            modelBuilder.Entity<Payment>().HasData(new Payment());
        }
    }
}
