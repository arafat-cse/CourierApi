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
            //Invoice
            modelBuilder.Entity<Invoice>()
               .HasOne(s => s.Customers)
              .WithMany(d => d.Invoices)
              .HasForeignKey(s => s.customerId)
              .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete
                                                  //Invoice
            modelBuilder.Entity<Invoice>()
               .HasOne(s => s.PaymentMethods)
              .WithMany(d => d.Invoices)
              .HasForeignKey(s => s.paymentMethodId)
              .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

            modelBuilder.Entity<Invoice>()
              .HasOne(s => s.Parcels)
             .WithMany(d => d.Invoices)
             .HasForeignKey(s => s.ParcelsId)
             .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

            //Parcel
            modelBuilder.Entity<Parcel>()
               .HasOne(s => s.Branchs)
              .WithMany(d => d.Parcels)
              .HasForeignKey(s => s.receiverBranchId)
              .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

            modelBuilder.Entity<Parcel>()
              .HasOne(s => s.Vans)
             .WithMany(d => d.Parcels)
             .HasForeignKey(s => s.VanId)
             .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

            modelBuilder.Entity<Parcel>()
              .HasOne(s => s.ParcelTypes)
             .WithMany(d => d.Parcels)
             .HasForeignKey(s => s.parcelTypeId)
             .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

            modelBuilder.Entity<Parcel>()
             .HasOne(s => s.DeliveryCharges)
            .WithMany(d => d.Parcels)
            .HasForeignKey(s => s.deliveryChargeId)
            .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete
            //DeliveryCharge
            modelBuilder.Entity<DeliveryCharge>()
            .HasOne(s => s.ParcelTypes)
           .WithMany(d => d.DeliveryCharges)
           .HasForeignKey(s => s.parcelTypeId)
           .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete



           
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
