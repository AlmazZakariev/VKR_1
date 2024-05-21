using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class RegistrationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registrations_rooms",
                table: "registrations");

            migrationBuilder.DropIndex(
                name: "IX_registrations_room_id",
                table: "registrations");

            migrationBuilder.DropColumn(
                name: "room_id",
                table: "registrations");

            migrationBuilder.AlterColumn<short>(
                name: "gender",
                table: "users",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "room",
                table: "registrations",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "room",
                table: "registrations");

            migrationBuilder.AlterColumn<short>(
                name: "gender",
                table: "users",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<long>(
                name: "room_id",
                table: "registrations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_registrations_room_id",
                table: "registrations",
                column: "room_id");

            migrationBuilder.AddForeignKey(
                name: "FK_registrations_rooms",
                table: "registrations",
                column: "room_id",
                principalTable: "rooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
