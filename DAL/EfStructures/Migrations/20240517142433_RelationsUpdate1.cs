using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class RelationsUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_requests_TimeSlotId",
                table: "requests");

            migrationBuilder.DropIndex(
                name: "IX_registrations_request_id",
                table: "registrations");

            migrationBuilder.DropColumn(
                name: "request_id",
                table: "time_slots");

            migrationBuilder.RenameColumn(
                name: "TimeSlotId",
                table: "requests",
                newName: "time_slot_id");

            migrationBuilder.AlterColumn<long>(
                name: "time_slot_id",
                table: "requests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_requests_time_slot_id",
                table: "requests",
                column: "time_slot_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_registrations_request_id",
                table: "registrations",
                column: "request_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_requests_time_slot_id",
                table: "requests");

            migrationBuilder.DropIndex(
                name: "IX_registrations_request_id",
                table: "registrations");

            migrationBuilder.RenameColumn(
                name: "time_slot_id",
                table: "requests",
                newName: "TimeSlotId");

            migrationBuilder.AddColumn<long>(
                name: "request_id",
                table: "time_slots",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "TimeSlotId",
                table: "requests",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_requests_TimeSlotId",
                table: "requests",
                column: "TimeSlotId",
                unique: true,
                filter: "[TimeSlotId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_registrations_request_id",
                table: "registrations",
                column: "request_id");
        }
    }
}
