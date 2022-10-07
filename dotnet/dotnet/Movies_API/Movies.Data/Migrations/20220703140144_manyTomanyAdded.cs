using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Movies.Data.Migrations
{
    public partial class manyTomanyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "actorId",
                table: "Actors",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Actors_Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    movieId = table.Column<int>(type: "integer", nullable: false),
                    actorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actors_Movies_Actors_actorId",
                        column: x => x.actorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actors_Movies_Movies_movieId",
                        column: x => x.movieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_actorId",
                table: "Actors",
                column: "actorId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_Movies_actorId",
                table: "Actors_Movies",
                column: "actorId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_Movies_movieId",
                table: "Actors_Movies",
                column: "movieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Actors_actorId",
                table: "Actors",
                column: "actorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Actors_actorId",
                table: "Actors");

            migrationBuilder.DropTable(
                name: "Actors_Movies");

            migrationBuilder.DropIndex(
                name: "IX_Actors_actorId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "actorId",
                table: "Actors");
        }
    }
}
