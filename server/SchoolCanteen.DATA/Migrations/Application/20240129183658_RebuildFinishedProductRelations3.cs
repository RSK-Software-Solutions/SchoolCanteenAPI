using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolCanteen.DATA.Migrations.Application
{
    /// <inheritdoc />
    public partial class RebuildFinishedProductRelations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinishedProductProductStorage");

            migrationBuilder.AddColumn<int>(
                name: "FinishedProductId",
                table: "ProductStorages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStorages_FinishedProductId",
                table: "ProductStorages",
                column: "FinishedProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStorages_FinishedProducts_FinishedProductId",
                table: "ProductStorages",
                column: "FinishedProductId",
                principalTable: "FinishedProducts",
                principalColumn: "FinishedProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStorages_FinishedProducts_FinishedProductId",
                table: "ProductStorages");

            migrationBuilder.DropIndex(
                name: "IX_ProductStorages_FinishedProductId",
                table: "ProductStorages");

            migrationBuilder.DropColumn(
                name: "FinishedProductId",
                table: "ProductStorages");

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
                name: "IX_FinishedProductProductStorage_ProductStoragesProductStorageId",
                table: "FinishedProductProductStorage",
                column: "ProductStoragesProductStorageId");
        }
    }
}
