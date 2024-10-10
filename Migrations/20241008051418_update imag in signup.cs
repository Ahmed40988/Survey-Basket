using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Handmade.Migrations
{
    /// <inheritdoc />
    public partial class updateimaginsignup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageurl",
                table: "Signups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageurl",
                table: "Signups");
        }
    }
}
