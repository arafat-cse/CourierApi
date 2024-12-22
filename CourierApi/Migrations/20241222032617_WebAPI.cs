using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourierApi.Migrations
{
    /// <inheritdoc />
    public partial class WebAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    branchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.branchId);
                    table.ForeignKey(
                        name: "FK_Branches_Branches_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    companyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    companyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.companyId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customerId);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    designationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaryRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.designationId);
                });

            migrationBuilder.CreateTable(
                name: "ParcelTypes",
                columns: table => new
                {
                    parcelTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parcelTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelTypes", x => x.parcelTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    paymentMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentMethodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.paymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "Vans",
                columns: table => new
                {
                    vanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    registrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vans", x => x.vanId);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    bankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    accountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    branchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    companyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.bankId);
                    table.ForeignKey(
                        name: "FK_Banks_Companys_companyId",
                        column: x => x.companyId,
                        principalTable: "Companys",
                        principalColumn: "companyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    staffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    staffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    designationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.staffId);
                    table.ForeignKey(
                        name: "FK_Staffs_Designations_designationId",
                        column: x => x.designationId,
                        principalTable: "Designations",
                        principalColumn: "designationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryCharges",
                columns: table => new
                {
                    deliveryChargeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    weight = table.Column<double>(type: "float", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    parcelTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryCharges", x => x.deliveryChargeId);
                    table.ForeignKey(
                        name: "FK_DeliveryCharges_ParcelTypes_parcelTypeId",
                        column: x => x.parcelTypeId,
                        principalTable: "ParcelTypes",
                        principalColumn: "parcelTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchesStaffs",
                columns: table => new
                {
                    branchStaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branchStaffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    staffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchesStaffs", x => x.branchStaffId);
                    table.ForeignKey(
                        name: "FK_BranchesStaffs_Staffs_staffId",
                        column: x => x.staffId,
                        principalTable: "Staffs",
                        principalColumn: "staffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    parcelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parcelCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senderCustomerId = table.Column<int>(type: "int", nullable: false),
                    receiverCustomerId = table.Column<int>(type: "int", nullable: false),
                    sendTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    receiveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    senderBranchId = table.Column<int>(type: "int", nullable: false),
                    receiverBranchId = table.Column<int>(type: "int", nullable: false),
                    estimatedReceiveTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    weight = table.Column<double>(type: "float", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isDispatchedFromBranch = table.Column<bool>(type: "bit", nullable: false),
                    isInTransit = table.Column<bool>(type: "bit", nullable: false),
                    isReceivedAtBranch = table.Column<bool>(type: "bit", nullable: false),
                    isReceivedByReceiver = table.Column<bool>(type: "bit", nullable: false),
                    vanId = table.Column<int>(type: "int", nullable: true),
                    driverId = table.Column<int>(type: "int", nullable: true),
                    deliveryChargeId = table.Column<int>(type: "int", nullable: true),
                    parcelTypeId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.parcelId);
                    table.ForeignKey(
                        name: "FK_Parcels_Branches_receiverBranchId",
                        column: x => x.receiverBranchId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_Branches_senderBranchId",
                        column: x => x.senderBranchId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_DeliveryCharges_deliveryChargeId",
                        column: x => x.deliveryChargeId,
                        principalTable: "DeliveryCharges",
                        principalColumn: "deliveryChargeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_ParcelTypes_parcelTypeId",
                        column: x => x.parcelTypeId,
                        principalTable: "ParcelTypes",
                        principalColumn: "parcelTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_Vans_vanId",
                        column: x => x.vanId,
                        principalTable: "Vans",
                        principalColumn: "vanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    invoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    particular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    customerId = table.Column<int>(type: "int", nullable: false),
                    paymentMethodId = table.Column<int>(type: "int", nullable: false),
                    ParcelsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.invoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_customerId",
                        column: x => x.customerId,
                        principalTable: "Customers",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Parcels_ParcelsId",
                        column: x => x.ParcelsId,
                        principalTable: "Parcels",
                        principalColumn: "parcelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_PaymentMethods_paymentMethodId",
                        column: x => x.paymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "paymentMethodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_companyId",
                table: "Banks",
                column: "companyId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ParentId",
                table: "Branches",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchesStaffs_staffId",
                table: "BranchesStaffs",
                column: "staffId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryCharges_parcelTypeId",
                table: "DeliveryCharges",
                column: "parcelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_customerId",
                table: "Invoices",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ParcelsId",
                table: "Invoices",
                column: "ParcelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_paymentMethodId",
                table: "Invoices",
                column: "paymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_deliveryChargeId",
                table: "Parcels",
                column: "deliveryChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_parcelTypeId",
                table: "Parcels",
                column: "parcelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_receiverBranchId",
                table: "Parcels",
                column: "receiverBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_senderBranchId",
                table: "Parcels",
                column: "senderBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_vanId",
                table: "Parcels",
                column: "vanId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_designationId",
                table: "Staffs",
                column: "designationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "BranchesStaffs");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Companys");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "DeliveryCharges");

            migrationBuilder.DropTable(
                name: "Vans");

            migrationBuilder.DropTable(
                name: "ParcelTypes");
        }
    }
}
