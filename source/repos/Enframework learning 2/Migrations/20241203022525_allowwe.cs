using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enframework_learning_2.Migrations
{
    /// <inheritdoc />
    public partial class allowwe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogimage_Blogs_Blogforignkey",
                table: "Blogimage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogimage",
                table: "Blogimage");

            migrationBuilder.RenameTable(
                name: "Blogimage",
                newName: "blogimage");

            migrationBuilder.RenameIndex(
                name: "IX_Blogimage_Blogforignkey",
                table: "blogimage",
                newName: "IX_blogimage_Blogforignkey");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blogimage",
                table: "blogimage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_blogimage_Blogs_Blogforignkey",
                table: "blogimage",
                column: "Blogforignkey",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogimage_Blogs_Blogforignkey",
                table: "blogimage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_blogimage",
                table: "blogimage");

            migrationBuilder.RenameTable(
                name: "blogimage",
                newName: "Blogimage");

            migrationBuilder.RenameIndex(
                name: "IX_blogimage_Blogforignkey",
                table: "Blogimage",
                newName: "IX_Blogimage_Blogforignkey");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogimage",
                table: "Blogimage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogimage_Blogs_Blogforignkey",
                table: "Blogimage",
                column: "Blogforignkey",
                principalTable: "Blogs",
                principalColumn: "Id");
        }
    }
}
