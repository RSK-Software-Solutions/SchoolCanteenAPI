using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolCanteen.DATA.Migrations
{
    /// <inheritdoc />
    public partial class Correct3RoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserDetails_UserDetailsId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails");

            migrationBuilder.RenameTable(
                name: "UserDetails",
                newName: "UsersDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersDetails",
                table: "UsersDetails",
                column: "UserDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UsersDetails_UserDetailsId",
                table: "Users",
                column: "UserDetailsId",
                principalTable: "UsersDetails",
                principalColumn: "UserDetailsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersDetails_UserDetailsId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersDetails",
                table: "UsersDetails");

            migrationBuilder.RenameTable(
                name: "UsersDetails",
                newName: "UserDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails",
                column: "UserDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserDetails_UserDetailsId",
                table: "Users",
                column: "UserDetailsId",
                principalTable: "UserDetails",
                principalColumn: "UserDetailsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
