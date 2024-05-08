using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    floor = table.Column<short>(type: "smallint", nullable: false),
                    number = table.Column<short>(type: "smallint", nullable: false),
                    add_number = table.Column<short>(type: "smallint", nullable: true),
                    gender = table.Column<short>(type: "smallint", nullable: true),
                    free_slots = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    pass = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    surname = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    patronymic = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    admin = table.Column<byte[]>(type: "binary(1)", fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    preference_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.id);
                    table.ForeignKey(
                        name: "FK_requests_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "registrations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_id = table.Column<long>(type: "bigint", nullable: false),
                    administrator_id = table.Column<long>(type: "bigint", nullable: false),
                    room_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registrations", x => x.id);
                    table.ForeignKey(
                        name: "FK_registrations_requests",
                        column: x => x.request_id,
                        principalTable: "requests",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_registrations_rooms",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_registrations_users",
                        column: x => x.administrator_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_registrations_administrator_id",
                table: "registrations",
                column: "administrator_id");

            migrationBuilder.CreateIndex(
                name: "IX_registrations_request_id",
                table: "registrations",
                column: "request_id");

            migrationBuilder.CreateIndex(
                name: "IX_registrations_room_id",
                table: "registrations",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_requests_user_id",
                table: "requests",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "registrations");

            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
