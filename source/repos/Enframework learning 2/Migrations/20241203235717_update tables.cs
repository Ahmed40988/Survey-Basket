using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enframework_learning_2.Migrations
{
    /// <inheritdoc />
    public partial class updatetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Posturl",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Blogimageid",
                table: "Post",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Post_Blogimageid",
                table: "Post",
                column: "Blogimageid");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_blogimage_Blogimageid",
                table: "Post",
                column: "Blogimageid",
                principalTable: "blogimage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_blogimage_Blogimageid",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_Blogimageid",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Blogimageid",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "Posturl",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
