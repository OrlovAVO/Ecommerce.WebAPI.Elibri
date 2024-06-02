using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elibri.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrder17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Categories",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Categories",
                newName: "Description");
        }
    }
}
