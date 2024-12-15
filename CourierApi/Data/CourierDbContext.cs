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
        public DbSet<Designation> designations {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Parent)
                .WithMany(b => b.ChildBranches)
                .HasForeignKey(b => b.ParentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
            //staff
            modelBuilder.Entity<Staff>()
              .HasOne(s => s.Designation)
              .WithMany(d => d.Staffs)
              .HasForeignKey(s => s.designationId)
              .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete
            //bank
            modelBuilder.Entity<Bank>()
               .HasOne(s => s.Company)
              .WithMany(d => d.Banks)
              .HasForeignKey(s => s.companyId)
              .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

            //modelBuilder.Entity<Branch>()
            //    .HasOne(b => b.Parent) // Parent সম্পর্ক
            //    .WithMany(b => b.ChildBranches) // ChildBranches সম্পর্ক
            //    .HasForeignKey(b => b.ParentId)
            //    .OnDelete(DeleteBehavior.Restrict); // Restrict delete to prevent cascade delete
        }
        /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          {
              optionsBuilder.UseLazyLoadingProxies(false);
          }*/
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Staff>()
        //        .HasOne(s => s.Designation)
        //        .WithMany(d => d.Staffs)
        //        .HasForeignKey(s => s.designationId)
        //        .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete
        //}

    }


}
