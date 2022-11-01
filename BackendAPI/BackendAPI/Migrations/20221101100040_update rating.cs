using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAPI.Migrations
{
    public partial class updaterating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Rating",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KhachHangIdId",
                table: "Rating",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Rating_KhachHangIdId",
                table: "Rating",
                column: "KhachHangIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_KhachHangIdId",
                table: "Rating",
                column: "KhachHangIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_KhachHangIdzId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_KhachHangIdId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "KhachHangIdId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Rating");
        }
    }
}
