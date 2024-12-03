using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourierApi.Migrations
{
    /// <inheritdoc />
    public partial class CourierApi : Migration
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
                    branchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    customerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customerId);
                });

            migrationBuilder.CreateTable(
                name: "ParsersTypes",
                columns: table => new
                {
                    parcelTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parcelTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParsersTypes", x => x.parcelTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    paymentMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentMethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.paymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    staffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    staffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateBy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.staffId);
                });

            migrationBuilder.CreateTable(
                name: "vans",
                columns: table => new
                {
                    vanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    registrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateBy = table.Column<int>(type: "int", nullable: false),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vans", x => x.vanId);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    bankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    companyId = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    accountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    branchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.bankId);
                    table.ForeignKey(
                        name: "FK_Banks_Companys_companyId",
                        column: x => x.companyId,
                        principalTable: "Companys",
                        principalColumn: "companyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryCharges",
                columns: table => new
                {
                    deliveryChargeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parcelTypeId = table.Column<int>(type: "int", nullable: false),
                    weight = table.Column<double>(type: "float", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryCharges", x => x.deliveryChargeId);
                    table.ForeignKey(
                        name: "FK_DeliveryCharges_ParsersTypes_parcelTypeId",
                        column: x => x.parcelTypeId,
                        principalTable: "ParsersTypes",
                        principalColumn: "parcelTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchesStaffs",
                columns: table => new
                {
                    branchStaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branchStaffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffId = table.Column<int>(type: "int", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                name: "Parsers",
                columns: table => new
                {
                    ParcelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcelCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderCustomerId = table.Column<int>(type: "int", nullable: false),
                    ReceiverCustomerId = table.Column<int>(type: "int", nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiveTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderBranchId = table.Column<int>(type: "int", nullable: false),
                    ReceiverBranchId = table.Column<int>(type: "int", nullable: false),
                    BranchsbranchId = table.Column<int>(type: "int", nullable: true),
                    EstimatedReceiveTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    VanId = table.Column<int>(type: "int", nullable: true),
                    driverId = table.Column<int>(type: "int", nullable: true),
                    deliveryChargeId = table.Column<int>(type: "int", nullable: true),
                    parcelTypeId = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sendingBranch = table.Column<bool>(type: "bit", nullable: false),
                    percelSendingDestribution = table.Column<bool>(type: "bit", nullable: false),
                    recebingDistributin = table.Column<bool>(type: "bit", nullable: false),
                    recebingBranch = table.Column<bool>(type: "bit", nullable: false),
                    recebingReceber = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parsers", x => x.ParcelId);
                    table.ForeignKey(
                        name: "FK_Parsers_Branches_BranchsbranchId",
                        column: x => x.BranchsbranchId,
                        principalTable: "Branches",
                        principalColumn: "branchId");
                    table.ForeignKey(
                        name: "FK_Parsers_DeliveryCharges_deliveryChargeId",
                        column: x => x.deliveryChargeId,
                        principalTable: "DeliveryCharges",
                        principalColumn: "deliveryChargeId");
                    table.ForeignKey(
                        name: "FK_Parsers_ParsersTypes_parcelTypeId",
                        column: x => x.parcelTypeId,
                        principalTable: "ParsersTypes",
                        principalColumn: "parcelTypeId");
                    table.ForeignKey(
                        name: "FK_Parsers_vans_VanId",
                        column: x => x.VanId,
                        principalTable: "vans",
                        principalColumn: "vanId");
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
                    customerId = table.Column<int>(type: "int", nullable: false),
                    paymentMethodId = table.Column<int>(type: "int", nullable: false),
                    ParcelsId = table.Column<int>(type: "int", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.invoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_customerId",
                        column: x => x.customerId,
                        principalTable: "Customers",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Parsers_ParcelsId",
                        column: x => x.ParcelsId,
                        principalTable: "Parsers",
                        principalColumn: "ParcelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_PaymentMethods_paymentMethodId",
                        column: x => x.paymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "paymentMethodId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Parsers_BranchsbranchId",
                table: "Parsers",
                column: "BranchsbranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Parsers_deliveryChargeId",
                table: "Parsers",
                column: "deliveryChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parsers_parcelTypeId",
                table: "Parsers",
                column: "parcelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parsers_VanId",
                table: "Parsers",
                column: "VanId");
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
                name: "Parsers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "DeliveryCharges");

            migrationBuilder.DropTable(
                name: "vans");

            migrationBuilder.DropTable(
                name: "ParsersTypes");
        }
    }
}
