using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSUOW.Persistence.Migrations
{
    public partial class dbdeploy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Deliveries",
                schema: "dbo",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumberOfVolumes = table.Column<int>(type: "int", nullable: false),
                    TotalWeightOfVolumes = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Instructions = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PreferentialPeriod = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    SenderClientCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SenderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SenderContactName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SenderContactPhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SenderContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SenderAddress = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    SenderAddressPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SenderAddressZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SenderAddressZipCodePlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SenderAddressCountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    ReceiverClientCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReceiverName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReceiverContactName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ReceiverContactPhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReceiverContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReceiverAddress = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    ReceiverAddressPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReceiverAddressZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReceiverAddressZipCodePlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReceiverAddressCountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    ReceiverFixedInstructions = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BarCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PinNumber = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    ETA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PickingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.DeliveryId);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                schema: "dbo",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryId = table.Column<int>(type: "int", nullable: false),
                    PackageBarCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PackageNumber = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    CreatedDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageId);
                    table.ForeignKey(
                        name: "FK_Packages_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalSchema: "dbo",
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ReceiverAddress",
                schema: "dbo",
                table: "Deliveries",
                column: "ReceiverAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ReceiverContactPhoneNumber",
                schema: "dbo",
                table: "Deliveries",
                column: "ReceiverContactPhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_SenderAddress",
                schema: "dbo",
                table: "Deliveries",
                column: "SenderAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_SenderContactPhoneNumber",
                schema: "dbo",
                table: "Deliveries",
                column: "SenderContactPhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_DeliveryId",
                schema: "dbo",
                table: "Packages",
                column: "DeliveryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Packages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Deliveries",
                schema: "dbo");
        }
    }
}
