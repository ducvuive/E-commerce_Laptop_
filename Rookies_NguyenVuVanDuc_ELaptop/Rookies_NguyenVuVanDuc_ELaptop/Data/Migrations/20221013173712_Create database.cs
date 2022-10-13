using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rookies_NguyenVuVanDuc_ELaptop.Data.Migrations
{
    public partial class Createdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GioiTinh",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySinh",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bonhoram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DungLuongRam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiRam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusRam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoTroToiDa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonhoram", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boxuly",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CongNgheCPU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoNhan = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TocDoCPU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToCDoToiDa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoNhoDem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxuly", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Congketnoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CongGiaoTiep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KetNoiKhongDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KheDocTheNho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Webcam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TinhNangKhac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DenBanPhim = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Congketnoi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Danhmucsanpham",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDM = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Danhmucsanpham", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hoadon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayHD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaNV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiGiaoHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TongTien = table.Column<long>(type: "bigint", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    MaKhacHangId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoadon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hoadon_AspNetUsers_MaKhacHangId",
                        column: x => x.MaKhacHangId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Manhinh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KichThuoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoPhanGiai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TanSoQuet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CongNgheMH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CamUng = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manhinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sanpham",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManHinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoXuLy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CongKetNoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenSP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    MauSac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OCung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardManHinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DacBiet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HDH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThietKe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichThuocTrongLuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Webcam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RaMat = table.Column<int>(type: "int", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGia = table.Column<long>(type: "bigint", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CongKNId = table.Column<int>(type: "int", nullable: false),
                    BoNhoRamId = table.Column<int>(type: "int", nullable: false),
                    DanhMucSanPhamId = table.Column<int>(type: "int", nullable: false),
                    MHId = table.Column<int>(type: "int", nullable: false),
                    BXLId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanpham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sanpham_Bonhoram_BoNhoRamId",
                        column: x => x.BoNhoRamId,
                        principalTable: "Bonhoram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sanpham_Boxuly_BXLId",
                        column: x => x.BXLId,
                        principalTable: "Boxuly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sanpham_Congketnoi_CongKNId",
                        column: x => x.CongKNId,
                        principalTable: "Congketnoi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sanpham_Danhmucsanpham_DanhMucSanPhamId",
                        column: x => x.DanhMucSanPhamId,
                        principalTable: "Danhmucsanpham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sanpham_Manhinh_MHId",
                        column: x => x.MHId,
                        principalTable: "Manhinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cthd",
                columns: table => new
                {
                    MaHD = table.Column<int>(type: "int", nullable: false),
                    MaSP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    HoaDonId = table.Column<int>(type: "int", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cthd", x => new { x.MaHD, x.MaSP });
                    table.ForeignKey(
                        name: "FK_Cthd_Hoadon_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "Hoadon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cthd_Sanpham_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "Sanpham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cthd_HoaDonId",
                table: "Cthd",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_Cthd_SanPhamId",
                table: "Cthd",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_Hoadon_MaKhacHangId",
                table: "Hoadon",
                column: "MaKhacHangId");

            migrationBuilder.CreateIndex(
                name: "IX_Sanpham_BoNhoRamId",
                table: "Sanpham",
                column: "BoNhoRamId");

            migrationBuilder.CreateIndex(
                name: "IX_Sanpham_BXLId",
                table: "Sanpham",
                column: "BXLId");

            migrationBuilder.CreateIndex(
                name: "IX_Sanpham_CongKNId",
                table: "Sanpham",
                column: "CongKNId");

            migrationBuilder.CreateIndex(
                name: "IX_Sanpham_DanhMucSanPhamId",
                table: "Sanpham",
                column: "DanhMucSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_Sanpham_MHId",
                table: "Sanpham",
                column: "MHId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cthd");

            migrationBuilder.DropTable(
                name: "Hoadon");

            migrationBuilder.DropTable(
                name: "Sanpham");

            migrationBuilder.DropTable(
                name: "Bonhoram");

            migrationBuilder.DropTable(
                name: "Boxuly");

            migrationBuilder.DropTable(
                name: "Congketnoi");

            migrationBuilder.DropTable(
                name: "Danhmucsanpham");

            migrationBuilder.DropTable(
                name: "Manhinh");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NgaySinh",
                table: "AspNetUsers");
        }
    }
}
