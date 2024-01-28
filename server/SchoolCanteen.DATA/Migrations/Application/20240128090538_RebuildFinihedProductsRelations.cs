using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolCanteen.DATA.Migrations.Application
{
    /// <inheritdoc />
    public partial class RebuildFinihedProductsRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_RecipeDetails_Recipes_RecipeId",
            //    table: "RecipeDetails");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "FinishedProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProducts_RecipeId",
                table: "FinishedProducts",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedProducts_Recipes_RecipeId",
                table: "FinishedProducts",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDetails_Recipes_RecipeId",
                table: "RecipeDetails",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinishedProducts_Recipes_RecipeId",
                table: "FinishedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDetails_Recipes_RecipeId",
                table: "RecipeDetails");

            migrationBuilder.DropIndex(
                name: "IX_FinishedProducts_RecipeId",
                table: "FinishedProducts");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "FinishedProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDetails_Recipes_RecipeId",
                table: "RecipeDetails",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
