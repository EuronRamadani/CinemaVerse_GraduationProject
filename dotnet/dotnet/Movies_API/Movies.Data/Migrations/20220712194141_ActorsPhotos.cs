using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Data.Migrations
{
    public partial class ActorsPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "Photos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ActorId",
                table: "Photos",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Actors_ActorId",
                table: "Photos",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Actors_ActorId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ActorId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Photos");
        }
    }
}
