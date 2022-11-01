using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAPI.Migrations
{
    public partial class updatehoadon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_AspNetUsers_MaKhacHangIdId",
                table: "HoaDon");

            migrationBuilder.RenameColumn(
                name: "MaKhacHangIdId",
                table: "HoaDon",
                newName: "KhachHangId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDon_MaKhacHangIdId",
                table: "HoaDon",
                newName: "IX_HoaDon_KhachHangId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_AspNetUsers_KhachHangId",
                table: "HoaDon",
                column: "KhachHangId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_AspNetUsers_KhachHangId",
                table: "HoaDon");

            migrationBuilder.RenameColumn(
                name: "KhachHangId",
                table: "HoaDon",
                newName: "MaKhacHangIdId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDon_KhachHangId",
                table: "HoaDon",
                newName: "IX_HoaDon_MaKhacHangIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_AspNetUsers_MaKhacHangIdId",
                table: "HoaDon",
                column: "MaKhacHangIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
