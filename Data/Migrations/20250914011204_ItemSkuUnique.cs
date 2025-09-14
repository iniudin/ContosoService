using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoService.Migrations
{
    /// <inheritdoc />
    public partial class ItemSkuUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "items",
                type: "text",
                nullable: false,
                defaultValueSql: "'SKU-' || lpad(nextval('item_sku_seq')::text, 10, '0')",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "'SKU' || lpad(nextval('public.item_sku_seq')::text, 10, '0')");

            migrationBuilder.CreateIndex(
                name: "IX_items_Sku",
                table: "items",
                column: "Sku",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_items_Sku",
                table: "items");

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "items",
                type: "text",
                nullable: false,
                defaultValueSql: "'SKU' || lpad(nextval('public.item_sku_seq')::text, 10, '0')",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "'SKU-' || lpad(nextval('item_sku_seq')::text, 10, '0')");
        }
    }
}
