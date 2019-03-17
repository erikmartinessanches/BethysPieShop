using Microsoft.EntityFrameworkCore.Migrations;

namespace BethysPieShop.Migrations
{
    public partial class AddShoppingCartItemCorrectly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItemss_Pies_PieId",
                table: "ShoppingCartItemss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItemss",
                table: "ShoppingCartItemss");

            migrationBuilder.RenameTable(
                name: "ShoppingCartItemss",
                newName: "ShoppingCartItems");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItemss_PieId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_PieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems",
                column: "ShoppingCartItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Pies_PieId",
                table: "ShoppingCartItems",
                column: "PieId",
                principalTable: "Pies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Pies_PieId",
                table: "ShoppingCartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems");

            migrationBuilder.RenameTable(
                name: "ShoppingCartItems",
                newName: "ShoppingCartItemss");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_PieId",
                table: "ShoppingCartItemss",
                newName: "IX_ShoppingCartItemss_PieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItemss",
                table: "ShoppingCartItemss",
                column: "ShoppingCartItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItemss_Pies_PieId",
                table: "ShoppingCartItemss",
                column: "PieId",
                principalTable: "Pies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
