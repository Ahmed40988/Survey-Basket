using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Handmade.Migrations
{
    /// <inheritdoc />
    public partial class gf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstaPay",
                table: "Signups");

            migrationBuilder.DropColumn(
                name: "VodafonCash",
                table: "Signups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstaPay",
                table: "Signups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VodafonCash",
                table: "Signups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
