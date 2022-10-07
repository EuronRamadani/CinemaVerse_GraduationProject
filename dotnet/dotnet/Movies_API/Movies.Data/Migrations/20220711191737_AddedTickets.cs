using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Movies.Data.Migrations
{
    public partial class AddedTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TicketCode = table.Column<Guid>(type: "uuid", nullable: false),
                    HallId = table.Column<int>(type: "integer", nullable: false),
                    MovieTimeId = table.Column<int>(type: "integer", nullable: false),
                    SeatId = table.Column<int>(type: "integer", nullable: false),
                    RowId = table.Column<int>(type: "integer", nullable: false),
                    MovieStartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OwnerId = table.Column<string>(type: "text", nullable: true),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    Is3D = table.Column<bool>(type: "boolean", nullable: false),
                    IsVipTicket = table.Column<bool>(type: "boolean", nullable: false),
                    IsTicketForCouple = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    InsertedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_MovieTimes_MovieTimeId",
                        column: x => x.MovieTimeId,
                        principalTable: "MovieTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_HallId",
                table: "Tickets",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MovieTimeId",
                table: "Tickets",
                column: "MovieTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets",
                column: "SeatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
