using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISMRedesing.Migrations.Sqlite
{
    /// <inheritdoc />
    public partial class TestingSqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Expenses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Expenses");
        }
    }
}
