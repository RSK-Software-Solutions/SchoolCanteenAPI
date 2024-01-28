using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolCanteen.DATA.Migrations.Application
{
    /// <inheritdoc />
    public partial class RebuildRecipeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDetails_Recipes_RecipeId",
                table: "RecipeDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDetails_Recipes_RecipeId",
                table: "RecipeDetails",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDetails_Recipes_RecipeId",
                table: "RecipeDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDetails_Recipes_RecipeId",
                table: "RecipeDetails",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }
    }
}
