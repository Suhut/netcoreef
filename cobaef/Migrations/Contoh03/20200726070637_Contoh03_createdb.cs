using Microsoft.EntityFrameworkCore.Migrations;

namespace cobaef.Migrations.Contoh03
{
    public partial class Contoh03_createdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductCode = table.Column<string>(nullable: false),
                    ProductName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductCode);
                });

            migrationBuilder.CreateTable(
                name: "Uoms",
                columns: table => new
                {
                    UomCode = table.Column<string>(nullable: false),
                    UomName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uoms", x => x.UomCode);
                });

            migrationBuilder.CreateTable(
                name: "ProductUoms",
                columns: table => new
                {
                    ProductCode = table.Column<string>(nullable: false),
                    UomCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUoms", x => new { x.ProductCode, x.UomCode });
                    table.ForeignKey(
                        name: "FK_ProductUoms_Products_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Products",
                        principalColumn: "ProductCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductUoms_Uoms_UomCode",
                        column: x => x.UomCode,
                        principalTable: "Uoms",
                        principalColumn: "UomCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductUoms_UomCode",
                table: "ProductUoms",
                column: "UomCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductUoms");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Uoms");
        }
    }
}
