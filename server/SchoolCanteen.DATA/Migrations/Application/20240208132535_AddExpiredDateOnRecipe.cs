using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolCanteen.DATA.Migrations.Application
{
    /// <inheritdoc />
    public partial class AddExpiredDateOnRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidityPeriod",
                table: "ProductStorages");

            migrationBuilder.AddColumn<int>(
                name: "ValidityPeriod",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidityPeriod",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "ValidityPeriod",
                table: "ProductStorages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
