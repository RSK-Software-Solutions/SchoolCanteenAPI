using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolCanteen.DATA.Migrations.Application
{
    /// <inheritdoc />
    public partial class ModelsExtended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MinQuantity",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "FinishedProducts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "FinishedProducts");
        }
    }
}
