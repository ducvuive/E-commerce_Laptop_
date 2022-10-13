using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rookies_NguyenVuVanDuc_ELaptop.Data.Migrations
{
    public partial class Createdatabasewithforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanpham_Bonhoram_BoNhoRamId",
                table: "Sanpham");

            migrationBuilder.DropForeignKey(
                name: "FK_Sanpham_Boxuly_BXLId",
                table: "Sanpham");

            migrationBuilder.DropForeignKey(
                name: "FK_Sanpham_Congketnoi_CongKNId",
                table: "Sanpham");

            migrationBuilder.DropForeignKey(
                name: "FK_Sanpham_Danhmucsanpham_DanhMucSanPhamId",
                table: "Sanpham");

            migrationBuilder.DropForeignKey(
                name: "FK_Sanpham_Manhinh_MHId",
                table: "Sanpham");

            migrationBuilder.DropIndex(
                name: "IX_Sanpham_MHId",
                table: "Sanpham");

            migrationBuilder.DropColumn(
                name: "BoXuLy",
                table: "Sanpham");

            migrationBuilder.DropColumn(
                name: "CongKetNoi",
                table: "Sanpham");

            migrationBuilder.DropColumn(
                name: "ManHinh",
                table: "Sanpham");

            migrationBuilder.DropColumn(
                name: "Ram",
                table: "Sanpham");

            migrationBuilder.RenameColumn(
                name: "MHId",
                table: "Sanpham",
                newName: "RamId");

            migrationBuilder.RenameColumn(
                name: "DanhMucSanPhamId",
                table: "Sanpham",
                newName: "ManHinhId");

            migrationBuilder.RenameColumn(
                name: "CongKNId",
                table: "Sanpham",
                newName: "DanhMucSanPhamDMSPId");

            migrationBuilder.RenameColumn(
                name: "BoNhoRamId",
                table: "Sanpham",
                newName: "CongKetNoiId");

            migrationBuilder.RenameColumn(
                name: "BXLId",
                table: "Sanpham",
                newName: "BoXuLyId");

            migrationBuilder.RenameIndex(
                name: "IX_Sanpham_DanhMucSanPhamId",
                table: "Sanpham",
                newName: "IX_Sanpham_ManHinhId");

            migrationBuilder.RenameIndex(
                name: "IX_Sanpham_CongKNId",
                table: "Sanpham",
                newName: "IX_Sanpham_DanhMucSanPhamDMSPId");

            migrationBuilder.RenameIndex(
                name: "IX_Sanpham_BXLId",
                table: "Sanpham",
                newName: "IX_Sanpham_BoXuLyId");

            migrationBuilder.RenameIndex(
                name: "IX_Sanpham_BoNhoRamId",
                table: "Sanpham",
                newName: "IX_Sanpham_CongKetNoiId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Manhinh",
                newName: "ManHinhId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Hoadon",
                newName: "HoaDonId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Danhmucsanpham",
                newName: "DMSPId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Congketnoi",
                newName: "CongKetNoiId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Boxuly",
                newName: "BoXuLyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bonhoram",
                newName: "RamId");

            migrationBuilder.AddColumn<int>(
                name: "BoNhoRamRamId",
                table: "Sanpham",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sanpham_BoNhoRamRamId",
                table: "Sanpham",
                column: "BoNhoRamRamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sanpham_Bonhoram_BoNhoRamRamId",
                table: "Sanpham",
                column: "BoNhoRamRamId",
                principalTable: "Bonhoram",
                principalColumn: "RamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanpham_Boxuly_BoXuLyId",
                table: "Sanpham",
                column: "BoXuLyId",
                principalTable: "Boxuly",
                principalColumn: "BoXuLyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanpham_Congketnoi_CongKetNoiId",
                table: "Sanpham",
                column: "CongKetNoiId",
                principalTable: "Congketnoi",
                principalColumn: "CongKetNoiId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanpham_Danhmucsanpham_DanhMucSanPhamDMSPId",
                table: "Sanpham",
                column: "DanhMucSanPhamDMSPId",
                principalTable: "Danhmucsanpham",
                principalColumn: "DMSPId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanpham_Manhinh_ManHinhId",
                table: "Sanpham",
                column: "ManHinhId",
                principalTable: "Manhinh",
                principalColumn: "ManHinhId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanpham_Bonhoram_BoNhoRamRamId",
                table: "Sanpham");

            migrationBuilder.DropForeignKey(
                name: "FK_Sanpham_Boxuly_BoXuLyId",
                table: "Sanpham");

            migrationBuilder.DropForeignKey(
                name: "FK_Sanpham_Congketnoi_CongKetNoiId",
                table: "Sanpham");

            migrationBuilder.DropForeignKey(
                name: "FK_Sanpham_Danhmucsanpham_DanhMucSanPhamDMSPId",
                table: "Sanpham");

            migrationBuilder.DropForeignKey(
                name: "FK_Sanpham_Manhinh_ManHinhId",
                table: "Sanpham");

            migrationBuilder.DropIndex(
                name: "IX_Sanpham_BoNhoRamRamId",
                table: "Sanpham");

            migrationBuilder.DropColumn(
                name: "BoNhoRamRamId",
                table: "Sanpham");

            migrationBuilder.RenameColumn(
                name: "RamId",
                table: "Sanpham",
                newName: "MHId");

            migrationBuilder.RenameColumn(
                name: "ManHinhId",
                table: "Sanpham",
                newName: "DanhMucSanPhamId");

            migrationBuilder.RenameColumn(
                name: "DanhMucSanPhamDMSPId",
                table: "Sanpham",
                newName: "CongKNId");

            migrationBuilder.RenameColumn(
                name: "CongKetNoiId",
                table: "Sanpham",
                newName: "BoNhoRamId");

            migrationBuilder.RenameColumn(
                name: "BoXuLyId",
                table: "Sanpham",
                newName: "BXLId");

            migrationBuilder.RenameIndex(
                name: "IX_Sanpham_ManHinhId",
                table: "Sanpham",
                newName: "IX_Sanpham_DanhMucSanPhamId");

            migrationBuilder.RenameIndex(
                name: "IX_Sanpham_DanhMucSanPhamDMSPId",
                table: "Sanpham",
                newName: "IX_Sanpham_CongKNId");

            migrationBuilder.RenameIndex(
                name: "IX_Sanpham_CongKetNoiId",
                table: "Sanpham",
                newName: "IX_Sanpham_BoNhoRamId");

            migrationBuilder.RenameIndex(
                name: "IX_Sanpham_BoXuLyId",
                table: "Sanpham",
                newName: "IX_Sanpham_BXLId");

            migrationBuilder.RenameColumn(
                name: "ManHinhId",
                table: "Manhinh",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "HoaDonId",
                table: "Hoadon",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DMSPId",
                table: "Danhmucsanpham",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CongKetNoiId",
                table: "Congketnoi",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BoXuLyId",
                table: "Boxuly",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RamId",
                table: "Bonhoram",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "BoXuLy",
                table: "Sanpham",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CongKetNoi",
                table: "Sanpham",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManHinh",
                table: "Sanpham",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ram",
                table: "Sanpham",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sanpham_MHId",
                table: "Sanpham",
                column: "MHId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sanpham_Bonhoram_BoNhoRamId",
                table: "Sanpham",
                column: "BoNhoRamId",
                principalTable: "Bonhoram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanpham_Boxuly_BXLId",
                table: "Sanpham",
                column: "BXLId",
                principalTable: "Boxuly",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanpham_Congketnoi_CongKNId",
                table: "Sanpham",
                column: "CongKNId",
                principalTable: "Congketnoi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanpham_Danhmucsanpham_DanhMucSanPhamId",
                table: "Sanpham",
                column: "DanhMucSanPhamId",
                principalTable: "Danhmucsanpham",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanpham_Manhinh_MHId",
                table: "Sanpham",
                column: "MHId",
                principalTable: "Manhinh",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
