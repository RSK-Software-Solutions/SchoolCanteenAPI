using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolCanteen.DATA.Migrations.Application
{
    /// <inheritdoc />
    public partial class addProductStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFinishedProduct");

            migrationBuilder.DropColumn(
                name: "ValidityPeriod",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "FinishedProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductStorages",
                columns: table => new
                {
                    ProductStorageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "float", nullable: false),
                    Quantity = table.Column<float>(type: "float", nullable: false),
                    ValidityPeriod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStorages", x => x.ProductStorageId);
                    table.ForeignKey(
                        name: "FK_ProductStorages_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStorages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FinishedProductProductStorage",
                columns: table => new
                {
                    FinishedProductsFinishedProductId = table.Column<int>(type: "int", nullable: false),
                    ProductStoragesProductStorageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedProductProductStorage", x => new { x.FinishedProductsFinishedProductId, x.ProductStoragesProductStorageId });
                    table.ForeignKey(
                        name: "FK_FinishedProductProductStorage_FinishedProducts_FinishedProdu~",
                        column: x => x.FinishedProductsFinishedProductId,
                        principalTable: "FinishedProducts",
                        principalColumn: "FinishedProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishedProductProductStorage_ProductStorages_ProductStorage~",
                        column: x => x.ProductStoragesProductStorageId,
                        principalTable: "ProductStorages",
                        principalColumn: "ProductStorageId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProducts_ProductId",
                table: "FinishedProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductProductStorage_ProductStoragesProductStorageId",
                table: "FinishedProductProductStorage",
                column: "ProductStoragesProductStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStorages_CompanyId",
                table: "ProductStorages",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStorages_ProductId",
                table: "ProductStorages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedProducts_Products_ProductId",
                table: "FinishedProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinishedProducts_Products_ProductId",
                table: "FinishedProducts");

            migrationBuilder.DropTable(
                name: "FinishedProductProductStorage");

            migrationBuilder.DropTable(
                name: "ProductStorages");

            migrationBuilder.DropIndex(
                name: "IX_FinishedProducts_ProductId",
                table: "FinishedProducts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "FinishedProducts");

            migrationBuilder.AddColumn<int>(
                name: "ValidityPeriod",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductFinishedProduct",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    FinishedProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFinishedProduct", x => new { x.ProductId, x.FinishedProductId });
                    table.ForeignKey(
                        name: "FK_ProductFinishedProduct_FinishedProducts_FinishedProductId",
                        column: x => x.FinishedProductId,
                        principalTable: "FinishedProducts",
                        principalColumn: "FinishedProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFinishedProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFinishedProduct_FinishedProductId",
                table: "ProductFinishedProduct",
                column: "FinishedProductId");
        }
    }
}
