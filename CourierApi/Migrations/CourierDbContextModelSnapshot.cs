﻿// <auto-generated />
using System;
using CourierApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CourierApi.Migrations
{
    [DbContext(typeof(CourierDbContext))]
    partial class CourierDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourierApi.Models.Bank", b =>
                {
                    b.Property<int>("bankId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("bankId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("accountNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("branchName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("companyId")
                        .HasColumnType("int");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("updateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("bankId");

                    b.HasIndex("companyId");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("CourierApi.Models.Branch", b =>
                {
                    b.Property<int>("branchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("branchId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("branchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("updateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("branchId");

                    b.HasIndex("ParentId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("CourierApi.Models.BranchStaff", b =>
                {
                    b.Property<int>("branchStaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("branchStaffId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("branchStaffName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("createBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("staffId")
                        .HasColumnType("int");

                    b.Property<string>("updateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("branchStaffId");

                    b.HasIndex("staffId");

                    b.ToTable("BranchesStaffs");
                });

            modelBuilder.Entity("CourierApi.Models.Company", b =>
                {
                    b.Property<int>("companyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("companyId"));

                    b.Property<string>("companyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.HasKey("companyId");

                    b.ToTable("Companys");
                });

            modelBuilder.Entity("CourierApi.Models.Customer", b =>
                {
                    b.Property<int>("customerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("customerId"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerMobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("updateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("customerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CourierApi.Models.DeliveryCharge", b =>
                {
                    b.Property<int>("deliveryChargeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("deliveryChargeId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("parcelTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("updateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("weight")
                        .HasColumnType("float");

                    b.HasKey("deliveryChargeId");

                    b.HasIndex("parcelTypeId");

                    b.ToTable("DeliveryCharges");
                });

            modelBuilder.Entity("CourierApi.Models.Designation", b =>
                {
                    b.Property<int>("designationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("designationId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("SalaryRange")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("updateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("designationId");

                    b.ToTable("Designations");
                });

            modelBuilder.Entity("CourierApi.Models.Invoice", b =>
                {
                    b.Property<int>("invoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("invoiceId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ParcelsId")
                        .HasColumnType("int");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("customerId")
                        .HasColumnType("int");

                    b.Property<string>("particular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("paymentMethodId")
                        .HasColumnType("int");

                    b.Property<DateTime>("paymentTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("updateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("invoiceId");

                    b.HasIndex("ParcelsId");

                    b.HasIndex("customerId");

                    b.HasIndex("paymentMethodId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("CourierApi.Models.Parcel", b =>
                {
                    b.Property<int>("parcelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("parcelId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("deliveryChargeId")
                        .HasColumnType("int");

                    b.Property<int?>("driverId")
                        .HasColumnType("int");

                    b.Property<DateTime>("estimatedReceiveTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isDispatchedFromBranch")
                        .HasColumnType("bit");

                    b.Property<bool>("isInTransit")
                        .HasColumnType("bit");

                    b.Property<bool>("isReceivedAtBranch")
                        .HasColumnType("bit");

                    b.Property<bool>("isReceivedByReceiver")
                        .HasColumnType("bit");

                    b.Property<string>("parcelCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("parcelTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("receiveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("receiverBranchId")
                        .HasColumnType("int");

                    b.Property<int>("receiverCustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("sendTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("senderBranchId")
                        .HasColumnType("int");

                    b.Property<int>("senderCustomerId")
                        .HasColumnType("int");

                    b.Property<string>("updateBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("vanId")
                        .HasColumnType("int");

                    b.Property<double>("weight")
                        .HasColumnType("float");

                    b.HasKey("parcelId");

                    b.HasIndex("deliveryChargeId");

                    b.HasIndex("parcelTypeId");

                    b.HasIndex("receiverBranchId");

                    b.HasIndex("senderBranchId");

                    b.HasIndex("vanId");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("CourierApi.Models.ParcelType", b =>
                {
                    b.Property<int>("parcelTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("parcelTypeId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("parcelTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("updateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("parcelTypeId");

                    b.ToTable("ParcelTypes");
                });

            modelBuilder.Entity("CourierApi.Models.PaymentMethod", b =>
                {
                    b.Property<int>("paymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("paymentMethodId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("paymentMethodType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("updateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("paymentMethodId");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("CourierApi.Models.Staff", b =>
                {
                    b.Property<int>("staffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("staffId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("designationId")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("staffName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("updateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("staffId");

                    b.HasIndex("designationId");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("CourierApi.Models.Van", b =>
                {
                    b.Property<int>("vanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("vanId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("createBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("registrationNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("updateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("vanId");

                    b.ToTable("Vans");
                });

            modelBuilder.Entity("CourierApi.Models.Bank", b =>
                {
                    b.HasOne("CourierApi.Models.Company", "Company")
                        .WithMany("Banks")
                        .HasForeignKey("companyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CourierApi.Models.Branch", b =>
                {
                    b.HasOne("CourierApi.Models.Branch", "Parent")
                        .WithMany("ChildBranches")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("CourierApi.Models.BranchStaff", b =>
                {
                    b.HasOne("CourierApi.Models.Staff", "Staffs")
                        .WithMany()
                        .HasForeignKey("staffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("CourierApi.Models.DeliveryCharge", b =>
                {
                    b.HasOne("CourierApi.Models.ParcelType", "ParcelTypes")
                        .WithMany("DeliveryCharges")
                        .HasForeignKey("parcelTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ParcelTypes");
                });

            modelBuilder.Entity("CourierApi.Models.Invoice", b =>
                {
                    b.HasOne("CourierApi.Models.Parcel", "Parcels")
                        .WithMany("Invoices")
                        .HasForeignKey("ParcelsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CourierApi.Models.Customer", "Customers")
                        .WithMany("Invoices")
                        .HasForeignKey("customerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CourierApi.Models.PaymentMethod", "PaymentMethods")
                        .WithMany("Invoices")
                        .HasForeignKey("paymentMethodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customers");

                    b.Navigation("Parcels");

                    b.Navigation("PaymentMethods");
                });

            modelBuilder.Entity("CourierApi.Models.Parcel", b =>
                {
                    b.HasOne("CourierApi.Models.DeliveryCharge", "DeliveryCharge")
                        .WithMany("Parcels")
                        .HasForeignKey("deliveryChargeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CourierApi.Models.ParcelType", "ParcelType")
                        .WithMany("Parcels")
                        .HasForeignKey("parcelTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CourierApi.Models.Branch", "ReceiverBranch")
                        .WithMany("ReceiverBranch")
                        .HasForeignKey("receiverBranchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CourierApi.Models.Branch", "SenderBranch")
                        .WithMany("SenderBranch")
                        .HasForeignKey("senderBranchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CourierApi.Models.Van", "Van")
                        .WithMany("Parcels")
                        .HasForeignKey("vanId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("DeliveryCharge");

                    b.Navigation("ParcelType");

                    b.Navigation("ReceiverBranch");

                    b.Navigation("SenderBranch");

                    b.Navigation("Van");
                });

            modelBuilder.Entity("CourierApi.Models.Staff", b =>
                {
                    b.HasOne("CourierApi.Models.Designation", "Designation")
                        .WithMany("Staffs")
                        .HasForeignKey("designationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Designation");
                });

            modelBuilder.Entity("CourierApi.Models.Branch", b =>
                {
                    b.Navigation("ChildBranches");

                    b.Navigation("ReceiverBranch");

                    b.Navigation("SenderBranch");
                });

            modelBuilder.Entity("CourierApi.Models.Company", b =>
                {
                    b.Navigation("Banks");
                });

            modelBuilder.Entity("CourierApi.Models.Customer", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("CourierApi.Models.DeliveryCharge", b =>
                {
                    b.Navigation("Parcels");
                });

            modelBuilder.Entity("CourierApi.Models.Designation", b =>
                {
                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("CourierApi.Models.Parcel", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("CourierApi.Models.ParcelType", b =>
                {
                    b.Navigation("DeliveryCharges");

                    b.Navigation("Parcels");
                });

            modelBuilder.Entity("CourierApi.Models.PaymentMethod", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("CourierApi.Models.Van", b =>
                {
                    b.Navigation("Parcels");
                });
#pragma warning restore 612, 618
        }
    }
}
