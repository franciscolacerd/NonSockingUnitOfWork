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
                name: "Services",
                schema: "dbo",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
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
                    ServiceBarCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PinNumber = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    ETA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PickingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Volumes",
                schema: "dbo",
                columns: table => new
                {
                    VolumeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    VolumeBarCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VolumeNumber = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    CreatedDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volumes", x => x.VolumeId);
                    table.ForeignKey(
                        name: "FK_Volumes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "dbo",
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_ReceiverAddress",
                schema: "dbo",
                table: "Services",
                column: "ReceiverAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ReceiverContactPhoneNumber",
                schema: "dbo",
                table: "Services",
                column: "ReceiverContactPhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SenderAddress",
                schema: "dbo",
                table: "Services",
                column: "SenderAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SenderContactPhoneNumber",
                schema: "dbo",
                table: "Services",
                column: "SenderContactPhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Volumes_ServiceId",
                schema: "dbo",
                table: "Volumes",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volumes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "dbo");
        }
    }
}
