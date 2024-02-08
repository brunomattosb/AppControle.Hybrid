using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppControle.API.Migrations
{
    /// <inheritdoc />
    public partial class Iniciando2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Products_Name_UserId",
                table: "Products");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Categories_Name_UserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Products",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Categories",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Products_Name_UserId",
                table: "Products",
                columns: new[] { "Name", "UserId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Categories_Name_UserId",
                table: "Categories",
                columns: new[] { "Name", "UserId" });
        }
    }
}
