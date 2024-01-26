using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolCanteen.DATA.Migrations.Application
{
    /// <inheritdoc />
    public partial class addUnitFoeignKeyToRecipeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_UnitId",
                table: "RecipeDetails",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDetails_Units_UnitId",
                table: "RecipeDetails",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDetails_Units_UnitId",
                table: "RecipeDetails");

            migrationBuilder.DropIndex(
                name: "IX_RecipeDetails_UnitId",
                table: "RecipeDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDetails_Units_RecipeDetailId",
                table: "RecipeDetails",
                column: "RecipeDetailId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
