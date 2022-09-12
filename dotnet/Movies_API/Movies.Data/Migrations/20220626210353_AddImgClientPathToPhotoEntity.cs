using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Data.Migrations
{
    public partial class AddImgClientPathToPhotoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgClientPath",
                table: "Photos",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgClientPath",
                table: "Photos");
        }
    }
}
