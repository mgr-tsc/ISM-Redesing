using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISMRedesing.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "QuantityInStock",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "StockUniqueID",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UnitMeasure",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name_UnitMeasure",
                table: "Products",
                columns: new[] { "Name", "UnitMeasure" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockUniqueID",
                table: "Products",
                column: "StockUniqueID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Name_UnitMeasure",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StockUniqueID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QuantityInStock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockUniqueID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitMeasure",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
