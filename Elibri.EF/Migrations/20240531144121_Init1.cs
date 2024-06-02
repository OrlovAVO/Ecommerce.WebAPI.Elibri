using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elibri.EF.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Carts",
                type: "varchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Products_ProductId",
                table: "CartItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Products_ProductId",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Carts",
                type: "varchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(450)");
        }
    }
}
