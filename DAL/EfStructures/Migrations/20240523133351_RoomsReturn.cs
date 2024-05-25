using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class RoomsReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "administrator_id",
                table: "rooms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_rooms_administrator_id",
                table: "rooms",
                column: "administrator_id");

            migrationBuilder.AddForeignKey(
                name: "FK_rooms_users",
                table: "rooms",
                column: "administrator_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rooms_users",
                table: "rooms");

            migrationBuilder.DropIndex(
                name: "IX_rooms_administrator_id",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "administrator_id",
                table: "rooms");
        }
    }
}
