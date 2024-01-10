using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppControle.API.Migrations
{
    /// <inheritdoc />
    public partial class IniciandoClientsalterando : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Clients",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Clients",
                newName: "AddressNumber");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "Clients",
                newName: "AddressCep");

            migrationBuilder.RenameColumn(
                name: "Addres",
                table: "Clients",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Clients",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "AddressNumber",
                table: "Clients",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "AddressCep",
                table: "Clients",
                newName: "Cep");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Clients",
                newName: "Addres");
        }
    }
}
