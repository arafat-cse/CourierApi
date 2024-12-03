using CourierApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierApi.Data
{
    public class CourierDbContext : DbContext
    {
        public CourierDbContext(DbContextOptions<CourierDbContext> options) : base(options)
        {
        }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchStaff> BranchesStaffs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryCharge> DeliveryCharges { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Parcel> Parsers { get; set; }
        public DbSet<ParcelType> ParsersTypes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Van> vans { get; set; }
    }
}
