using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organic.Migrations
{
    /// <inheritdoc />
    public partial class ProductSlider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDollar",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SliderId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopButtonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopButtonUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailButton = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailButtonUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageNameInSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SliderId",
                table: "Products",
                column: "SliderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Slider_SliderId",
                table: "Products",
                column: "SliderId",
                principalTable: "Slider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Slider_SliderId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.DropIndex(
                name: "IX_Products_SliderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDollar",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SliderId",
                table: "Products");
        }
    }
}
