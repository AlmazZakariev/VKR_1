using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registrations_requests",
                table: "registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_registrations_rooms",
                table: "registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_registrations_users",
                table: "registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_requests_users",
                table: "requests");

            migrationBuilder.EnsureSchema(
                name: "Logging");

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

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "rooms",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "room_id",
                table: "registrations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "SeriLogs",
                schema: "Logging",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetDate()"),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "Xml", nullable: true),
                    LogEvent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceContext = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriLogs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_registrations_requests",
                table: "registrations",
                column: "request_id",
                principalTable: "requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_registrations_rooms",
                table: "registrations",
                column: "room_id",
                principalTable: "rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_registrations_users",
                table: "registrations",
                column: "administrator_id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_requests_users",
                table: "requests",
                column: "user_id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registrations_requests",
                table: "registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_registrations_rooms",
                table: "registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_registrations_users",
                table: "registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_requests_users",
                table: "requests");

            migrationBuilder.DropTable(
                name: "SeriLogs",
                schema: "Logging");

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

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "rooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "room_id",
                table: "registrations",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_registrations_requests",
                table: "registrations",
                column: "request_id",
                principalTable: "requests",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_registrations_rooms",
                table: "registrations",
                column: "room_id",
                principalTable: "rooms",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_registrations_users",
                table: "registrations",
                column: "administrator_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_requests_users",
                table: "requests",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
