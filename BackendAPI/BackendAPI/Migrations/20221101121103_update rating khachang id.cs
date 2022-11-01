using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAPI.Migrations
{
    public partial class updateratingkhachangid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_KhachHangIdId",
                table: "Rating");

            migrationBuilder.RenameColumn(
                name: "KhachHangIdId",
                table: "Rating",
                newName: "KhachHangId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_KhachHangIdId",
                table: "Rating",
                newName: "IX_Rating_KhachHang");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_KhachHang",
                table: "Rating",
                column: "KhachHangId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_KhachHang",
                table: "Rating");

            migrationBuilder.RenameColumn(
                name: "KhachHang",
                table: "Rating",
                newName: "KhachHangIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_KhachHang",
                table: "Rating",
                newName: "IX_Rating_KhachHangIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_KhachHangIdId",
                table: "Rating",
                column: "KhachHangIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
