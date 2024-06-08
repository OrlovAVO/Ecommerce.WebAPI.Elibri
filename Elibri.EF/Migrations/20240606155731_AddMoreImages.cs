using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elibri.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "Products");
        }
    }
}
