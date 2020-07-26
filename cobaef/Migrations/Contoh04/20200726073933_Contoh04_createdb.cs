using Microsoft.EntityFrameworkCore.Migrations;

namespace cobaef.Migrations.Contoh04
{
    public partial class Contoh04_createdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierCode = table.Column<string>(nullable: false),
                    SupplierName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierCode);
                });

            migrationBuilder.CreateTable(
                name: "SupplierAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressName = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    SupplierCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierAddresses_Suppliers_SupplierCode",
                        column: x => x.SupplierCode,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAddresses_SupplierCode",
                table: "SupplierAddresses",
                column: "SupplierCode",
                unique: true,
                filter: "[SupplierCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierAddresses");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
