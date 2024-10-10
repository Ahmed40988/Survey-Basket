using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Handmade.Migrations
{
    /// <inheritdoc />
    public partial class updateuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SignUp",
                table: "Users",
                newName: "imageUrl");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "imageUrl",
                table: "Users",
                newName: "SignUp");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Login");
        }
    }
}
