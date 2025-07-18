using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FC4.HotelReservation.Catalog.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.CreateTable(
                name: "hotels",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    street = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    state = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    zip_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_types",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_type_rates",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hotel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rate_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    rate_currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_type_rates", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_type_rates_hotels",
                        column: x => x.hotel_id,
                        principalSchema: "catalog",
                        principalTable: "hotels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_room_type_rates_room_types",
                        column: x => x.room_type_id,
                        principalSchema: "catalog",
                        principalTable: "room_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hotel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    floor = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_available = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.id);
                    table.ForeignKey(
                        name: "fk_rooms_hotels",
                        column: x => x.hotel_id,
                        principalSchema: "catalog",
                        principalTable: "hotels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rooms_room_types",
                        column: x => x.room_type_id,
                        principalSchema: "catalog",
                        principalTable: "room_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_room_type_rates_hotel_id_room_type_id_date",
                schema: "catalog",
                table: "room_type_rates",
                columns: new[] { "hotel_id", "room_type_id", "date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_room_type_rates_room_type_id",
                schema: "catalog",
                table: "room_type_rates",
                column: "room_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_hotel_id",
                schema: "catalog",
                table: "rooms",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_room_type_id",
                schema: "catalog",
                table: "rooms",
                column: "room_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "room_type_rates",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "rooms",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "hotels",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "room_types",
                schema: "catalog");
        }
    }
}
