using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FC4.HotelReservation.Reservations.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reservations");

            migrationBuilder.CreateTable(
                name: "guests",
                schema: "reservations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_type_inventory",
                schema: "reservations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hotel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_inventory = table.Column<int>(type: "integer", nullable: false),
                    total_reserved = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_type_inventory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                schema: "reservations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hotel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    stay_start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    stay_end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    guest_id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_quantity = table.Column<int>(type: "integer", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    total_currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.id);
                    table.ForeignKey(
                        name: "fk_reservations_guests",
                        column: x => x.guest_id,
                        principalSchema: "reservations",
                        principalTable: "guests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reservations_guest_id",
                schema: "reservations",
                table: "reservations",
                column: "guest_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_type_inventory_hotel_id_room_type_id_date",
                schema: "reservations",
                table: "room_type_inventory",
                columns: new[] { "hotel_id", "room_type_id", "date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservations",
                schema: "reservations");

            migrationBuilder.DropTable(
                name: "room_type_inventory",
                schema: "reservations");

            migrationBuilder.DropTable(
                name: "guests",
                schema: "reservations");
        }
    }
}
