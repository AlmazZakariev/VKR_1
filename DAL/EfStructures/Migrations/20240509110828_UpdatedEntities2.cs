using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "rooms",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "requests",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "registrations",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "rooms",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "requests",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "registrations",
                newName: "Id");
        }
    }
}
