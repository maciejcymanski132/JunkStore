using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JunkStore.Data.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductType_Product_ProductId",
                table: "ProductType");

            migrationBuilder.DropIndex(
                name: "IX_ProductType_ProductId",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductType");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductType",
                newName: "ProductTypeId");

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeId",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeProductTypeId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Product_TypeProductTypeId",
                table: "Product",
                column: "TypeProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_TypeProductTypeId",
                table: "Product",
                column: "TypeProductTypeId",
                principalTable: "ProductType",
                principalColumn: "ProductTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_TypeProductTypeId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_TypeProductTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TypeProductTypeId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "ProductType",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductType",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_ProductId",
                table: "ProductType",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductType_Product_ProductId",
                table: "ProductType",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId");
        }
    }
}
