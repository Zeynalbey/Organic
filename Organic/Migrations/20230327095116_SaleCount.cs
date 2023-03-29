using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organic.Migrations
{
    /// <inheritdoc />
    public partial class SaleCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleCount",
                table: "Products");
        }
    }
}
