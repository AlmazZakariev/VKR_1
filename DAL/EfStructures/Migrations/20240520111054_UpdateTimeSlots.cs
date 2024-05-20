using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTimeSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time",
                table: "time_slots");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "time_slots",
                type: "dateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "time_slots",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "dateTime");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "time",
                table: "time_slots",
                type: "time(7)",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
