using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class NewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TimeSlotId",
                table: "requests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "general",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    start_date = table.Column<DateTime>(type: "date", nullable: false),
                    end_date = table.Column<DateTime>(type: "date", nullable: false),
                    active = table.Column<byte[]>(type: "varbinary(1)", maxLength: 1, nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_general", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "time_slots",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    time = table.Column<TimeOnly>(type: "time(7)", nullable: false),
                    free = table.Column<byte[]>(type: "binary(1)", fixedLength: true, maxLength: 1, nullable: false),
                    administrator_id = table.Column<long>(type: "bigint", nullable: false),
                    request_id = table.Column<long>(type: "bigint", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time_slots", x => x.id);
                    table.ForeignKey(
                        name: "FK_time_slots_users",
                        column: x => x.administrator_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_requests_TimeSlotId",
                table: "requests",
                column: "TimeSlotId",
                unique: true,
                filter: "[TimeSlotId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_time_slots_administrator_id",
                table: "time_slots",
                column: "administrator_id");

            migrationBuilder.AddForeignKey(
                name: "FK_requests_time_slots",
                table: "requests",
                column: "TimeSlotId",
                principalTable: "time_slots",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_requests_time_slots",
                table: "requests");

            migrationBuilder.DropTable(
                name: "general");

            migrationBuilder.DropTable(
                name: "time_slots");

            migrationBuilder.DropIndex(
                name: "IX_requests_TimeSlotId",
                table: "requests");

            migrationBuilder.DropColumn(
                name: "TimeSlotId",
                table: "requests");
        }
    }
}
