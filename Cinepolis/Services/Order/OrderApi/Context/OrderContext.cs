using Microsoft.EntityFrameworkCore;
using OrderDomain.Models;

namespace OrderApi.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Order>().ToTable("Orders");
        //    modelBuilder.Entity<Order>().HasKey(x => x.Id);
        //    modelBuilder.Entity<Order>().Property(x => x.CustomerName).IsRequired();
        //    modelBuilder.Entity<Order>().Property(x => x.CreatedAt).IsRequired();
        //    modelBuilder.Entity<Order>().Property(x => x.UpdatedAt).IsRequired();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(new Order());
            modelBuilder.Entity<Order>().HasData(new Order());
        }
    }
}
