using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOM.Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatenavigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHoldings_Products_ProductId",
                table: "InventoryHoldings");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_Products_ProductId",
                table: "InventoryTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_Suppliers_SupplierId",
                table: "InventoryTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "SupplierInfos");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "ProductInfos");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "ProductInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierInfos",
                table: "SupplierInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInfos",
                table: "ProductInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfos_SupplierId",
                table: "ProductInfos",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHoldings_ProductInfos_ProductId",
                table: "InventoryHoldings",
                column: "ProductId",
                principalTable: "ProductInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_ProductInfos_ProductId",
                table: "InventoryTransactions",
                column: "ProductId",
                principalTable: "ProductInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_SupplierInfos_SupplierId",
                table: "InventoryTransactions",
                column: "SupplierId",
                principalTable: "SupplierInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInfos_SupplierInfos_SupplierId",
                table: "ProductInfos",
                column: "SupplierId",
                principalTable: "SupplierInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHoldings_ProductInfos_ProductId",
                table: "InventoryHoldings");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_ProductInfos_ProductId",
                table: "InventoryTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_SupplierInfos_SupplierId",
                table: "InventoryTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInfos_SupplierInfos_SupplierId",
                table: "ProductInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierInfos",
                table: "SupplierInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInfos",
                table: "ProductInfos");

            migrationBuilder.DropIndex(
                name: "IX_ProductInfos_SupplierId",
                table: "ProductInfos");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "ProductInfos");

            migrationBuilder.RenameTable(
                name: "SupplierInfos",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "ProductInfos",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHoldings_Products_ProductId",
                table: "InventoryHoldings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_Products_ProductId",
                table: "InventoryTransactions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_Suppliers_SupplierId",
                table: "InventoryTransactions",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }
    }
}
