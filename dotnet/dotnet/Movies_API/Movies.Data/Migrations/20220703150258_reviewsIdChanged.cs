using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Data.Migrations
{
    public partial class reviewsIdChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "MovieReviews");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "MovieReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "MovieReviews",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "MovieReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
