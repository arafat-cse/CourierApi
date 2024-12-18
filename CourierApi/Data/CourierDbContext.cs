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
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<ParcelType> ParcelTypes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Van> Vans { get; set; }
        public DbSet<Designation> Designations {  get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Branch>()
        //        .HasOne(b => b.Parent)
        //        .WithMany(b => b.ChildBranches)
        //        .HasForeignKey(b => b.ParentId)
        //        .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        //    //staff
        //    modelBuilder.Entity<Staff>()
        //      .HasOne(s => s.Designation)
        //      .WithMany(d => d.Staffs)
        //      .HasForeignKey(s => s.designationId)
        //      .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete
        //    //bank
        //    modelBuilder.Entity<Bank>()
        //       .HasOne(s => s.Company)
        //      .WithMany(d => d.Banks)
        //      .HasForeignKey(s => s.companyId)
        //      .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete
        //     //DeliveryCharge
        //    modelBuilder.Entity<DeliveryCharge>()
        //    .HasOne(s => s.ParcelTypes)
        //   .WithMany(d => d.DeliveryCharges)
        //   .HasForeignKey(s => s.parcelTypeId)
        //   .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

        //    //Parcel
        //    modelBuilder.Entity<Parcel>()
        //      .HasOne(s => s.SenderBranch)
        //     .WithMany(d => d.Parcels)
        //     .HasForeignKey(s => s.senderBranchId)
        //     .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

        //    modelBuilder.Entity<Parcel>()
        //       .HasOne(s => s.ReceiverBranch)
        //      .WithMany(d => d.Parcels)
        //      .HasForeignKey(s => s.receiverBranchId)
        //      .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

        //    modelBuilder.Entity<Parcel>()
        //      .HasOne(s => s.Van)
        //     .WithMany(d => d.Parcels)
        //     .HasForeignKey(s => s.vanId)
        //     .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

        //    modelBuilder.Entity<Parcel>()
        //      .HasOne(s => s.ParcelType)
        //     .WithMany(d => d.Parcels)
        //     .HasForeignKey(s => s.parcelTypeId)
        //     .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

        //    modelBuilder.Entity<Parcel>()
        //     .HasOne(s => s.DeliveryCharge)
        //    .WithMany(d => d.Parcels)
        //    .HasForeignKey(s => s.deliveryChargeId)
        //    .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

        //    //Invoice
        //    modelBuilder.Entity<Invoice>()
        //       .HasOne(s => s.Customers)
        //      .WithMany(d => d.Invoices)
        //      .HasForeignKey(s => s.customerId)
        //      .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete
        //                                          //Invoice
        //    modelBuilder.Entity<Invoice>()
        //       .HasOne(s => s.PaymentMethods)
        //      .WithMany(d => d.Invoices)
        //      .HasForeignKey(s => s.paymentMethodId)
        //      .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

        //    modelBuilder.Entity<Invoice>()
        //      .HasOne(s => s.Parcels)
        //     .WithMany(d => d.Invoices)
        //     .HasForeignKey(s => s.ParcelsId)
        //     .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete


        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Parent)
                .WithMany(b => b.ChildBranches)
                .HasForeignKey(b => b.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the staff-branch relationship
            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Designation)
                .WithMany(d => d.Staffs)
                .HasForeignKey(s => s.designationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure bank-company relationship
            modelBuilder.Entity<Bank>()
                .HasOne(s => s.Company)
                .WithMany(d => d.Banks)
                .HasForeignKey(s => s.companyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure delivery charge-parcel type relationship
            modelBuilder.Entity<DeliveryCharge>()
                .HasOne(s => s.ParcelTypes)
                .WithMany(d => d.DeliveryCharges)
                .HasForeignKey(s => s.parcelTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure SenderBranch and ReceiverBranch relationships separately for Parcel
            modelBuilder.Entity<Parcel>()
                .HasOne(s => s.SenderBranch)
                .WithMany(d => d.SenderBranch)
                .HasForeignKey(s => s.senderBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parcel>()
                .HasOne(s => s.ReceiverBranch)
                .WithMany(d => d.ReceiverBranch)
                .HasForeignKey(s => s.receiverBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parcel>()
                .HasOne(s => s.Van)
                .WithMany(d => d.Parcels)
                .HasForeignKey(s => s.vanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parcel>()
                .HasOne(s => s.ParcelType)
                .WithMany(d => d.Parcels)
                .HasForeignKey(s => s.parcelTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parcel>()
                .HasOne(s => s.DeliveryCharge)
                .WithMany(d => d.Parcels)
                .HasForeignKey(s => s.deliveryChargeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(s => s.Customers)
                .WithMany(d => d.Invoices)
                .HasForeignKey(s => s.customerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(s => s.PaymentMethods)
                .WithMany(d => d.Invoices)
                .HasForeignKey(s => s.paymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(s => s.Parcels)
                .WithMany(d => d.Invoices)
                .HasForeignKey(s => s.ParcelsId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }


}
