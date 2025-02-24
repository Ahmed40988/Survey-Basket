using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enframework_learning_2.Migrations
{
    /// <inheritdoc />
    public partial class allownullw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogimage_Blogs_Blogforignkey",
                table: "Blogimage");

            migrationBuilder.DropIndex(
                name: "IX_Blogimage_Blogforignkey",
                table: "Blogimage");

            migrationBuilder.AlterColumn<int>(
                name: "Blogforignkey",
                table: "Blogimage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Blogimage_Blogforignkey",
                table: "Blogimage",
                column: "Blogforignkey",
                unique: true,
                filter: "[Blogforignkey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogimage_Blogs_Blogforignkey",
                table: "Blogimage",
                column: "Blogforignkey",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogimage_Blogs_Blogforignkey",
                table: "Blogimage");

            migrationBuilder.DropIndex(
                name: "IX_Blogimage_Blogforignkey",
                table: "Blogimage");

            migrationBuilder.AlterColumn<int>(
                name: "Blogforignkey",
                table: "Blogimage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogimage_Blogforignkey",
                table: "Blogimage",
                column: "Blogforignkey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogimage_Blogs_Blogforignkey",
                table: "Blogimage",
                column: "Blogforignkey",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
