using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppControle.API.Migrations
{
    /// <inheritdoc />
    public partial class updateo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MonthlyFee",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyFee_UserId",
                table: "MonthlyFee",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonthlyFee_AspNetUsers_UserId",
                table: "MonthlyFee",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonthlyFee_AspNetUsers_UserId",
                table: "MonthlyFee");

            migrationBuilder.DropIndex(
                name: "IX_MonthlyFee_UserId",
                table: "MonthlyFee");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MonthlyFee");
        }
    }
}
