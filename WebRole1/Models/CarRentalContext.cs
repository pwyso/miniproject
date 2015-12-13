using System.Data.Entity;

namespace CarRental.Models
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext() : base("CarRentalContext")
        {
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
