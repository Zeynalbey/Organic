using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organic.Migrations
{
    /// <inheritdoc />
    public partial class BlogLIke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "Blogs",
                newName: "LikeCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LikeCount",
                table: "Blogs",
                newName: "BlogId");
        }
    }
}
