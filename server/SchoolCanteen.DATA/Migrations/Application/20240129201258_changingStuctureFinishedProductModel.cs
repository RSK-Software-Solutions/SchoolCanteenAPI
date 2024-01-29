using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolCanteen.DATA.Migrations.Application
{
    /// <inheritdoc />
    public partial class changingStuctureFinishedProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ProfitAmount",
                table: "FinishedProducts",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalCosts",
                table: "FinishedProducts",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalPrice",
                table: "FinishedProducts",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfitAmount",
                table: "FinishedProducts");

            migrationBuilder.DropColumn(
                name: "TotalCosts",
                table: "FinishedProducts");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "FinishedProducts");
        }
    }
}
