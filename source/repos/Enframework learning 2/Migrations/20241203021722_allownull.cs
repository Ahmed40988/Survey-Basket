using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enframework_learning_2.Migrations
{
    /// <inheritdoc />
    public partial class allownull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blogs_Blogid",
                table: "Post");

            migrationBuilder.AlterColumn<int>(
                name: "Blogid",
                table: "Post",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blogs_Blogid",
                table: "Post",
                column: "Blogid",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blogs_Blogid",
                table: "Post");

            migrationBuilder.AlterColumn<int>(
                name: "Blogid",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blogs_Blogid",
                table: "Post",
                column: "Blogid",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
