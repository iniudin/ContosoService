using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoService.Migrations
{
    /// <inheritdoc />
    public partial class ItemVariants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "items",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_items_ParentId",
                table: "items",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_items_items_ParentId",
                table: "items",
                column: "ParentId",
                principalTable: "items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_items_ParentId",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_items_ParentId",
                table: "items");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "items");
        }
    }
}
