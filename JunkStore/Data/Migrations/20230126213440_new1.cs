using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JunkStore.Data.Migrations
{
    public partial class new1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_TypeProductTypeId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "TypeProductTypeId",
                table: "Product",
                newName: "ProductTypeId1");

            migrationBuilder.RenameIndex(
                name: "IX_Product_TypeProductTypeId",
                table: "Product",
                newName: "IX_Product_ProductTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId1",
                table: "Product",
                column: "ProductTypeId1",
                principalTable: "ProductType",
                principalColumn: "ProductTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId1",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId1",
                table: "Product",
                newName: "TypeProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductTypeId1",
                table: "Product",
                newName: "IX_Product_TypeProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_TypeProductTypeId",
                table: "Product",
                column: "TypeProductTypeId",
                principalTable: "ProductType",
                principalColumn: "ProductTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
