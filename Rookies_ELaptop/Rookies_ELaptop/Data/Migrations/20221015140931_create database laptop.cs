using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rookies_ELaptop.Data.Migrations
{
    public partial class createdatabaselaptop : Migration
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
                name: "BoNhoRam",
                columns: table => new
                {
                    RamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DungLuongRam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LoaiRam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusRam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HoTroToiDa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoNhoRam", x => x.RamId);
                });

            migrationBuilder.CreateTable(
                name: "BoXuLy",
                columns: table => new
                {
                    BoXuLyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CongNgheCPU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SoNhan = table.Column<int>(type: "int", maxLength: 20, nullable: true),
                    SOLUONG = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    TocDoCPU = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ToCDoToiDa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BoNhoDem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoXuLy", x => x.BoXuLyId);
                });

            migrationBuilder.CreateTable(
                name: "CongKetNoi",
                columns: table => new
                {
                    CongKetNoiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CongGiaoTiep = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    KetNoiKhongDay = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KheDocTheNho = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Webcam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TinhNangKhac = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DenBanPhim = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongKetNoi", x => x.CongKetNoiId);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucSanPham",
                columns: table => new
                {
                    DMSPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucSanPham", x => x.DMSPId);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    HoaDonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayHD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiGiaoHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TongTien = table.Column<long>(type: "bigint", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    MaKhacHangId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.HoaDonId);
                    table.ForeignKey(
                        name: "FK_HoaDon_AspNetUsers_MaKhacHangId",
                        column: x => x.MaKhacHangId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ManHinh",
                columns: table => new
                {
                    ManHinhId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KichThuoc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DoPhanGiai = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    TanSoQuet = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CongNgheMH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CamUng = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManHinh", x => x.ManHinhId);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    SanPhamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManHinhId = table.Column<int>(type: "int", nullable: false),
                    BoXuLyId = table.Column<int>(type: "int", nullable: false),
                    RamId = table.Column<int>(type: "int", nullable: false),
                    CongKetNoiId = table.Column<int>(type: "int", nullable: false),
                    DMSPId = table.Column<int>(type: "int", nullable: false),
                    TenSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    MauSac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OCung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardManHinh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DacBiet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HDH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThietKe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KichThuocTrongLuong = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Webcam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    RaMat = table.Column<int>(type: "int", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGia = table.Column<long>(type: "bigint", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.SanPhamId);
                    table.ForeignKey(
                        name: "FK_SanPham_BoNhoRam_RamId",
                        column: x => x.RamId,
                        principalTable: "BoNhoRam",
                        principalColumn: "RamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_BoXuLy_BoXuLyId",
                        column: x => x.BoXuLyId,
                        principalTable: "BoXuLy",
                        principalColumn: "BoXuLyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_CongKetNoi_CongKetNoiId",
                        column: x => x.CongKetNoiId,
                        principalTable: "CongKetNoi",
                        principalColumn: "CongKetNoiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_DanhMucSanPham_DMSPId",
                        column: x => x.DMSPId,
                        principalTable: "DanhMucSanPham",
                        principalColumn: "DMSPId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_ManHinh_ManHinhId",
                        column: x => x.ManHinhId,
                        principalTable: "ManHinh",
                        principalColumn: "ManHinhId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTHD",
                columns: table => new
                {
                    HoaDonId = table.Column<int>(type: "int", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTHD", x => new { x.SanPhamId, x.HoaDonId });
                    table.ForeignKey(
                        name: "FK_CTHD_HoaDon_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDon",
                        principalColumn: "HoaDonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTHD_SanPham_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CTHD_HoaDonId",
                table: "CTHD",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaKhacHangId",
                table: "HoaDon",
                column: "MaKhacHangId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_BoXuLyId",
                table: "SanPham",
                column: "BoXuLyId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_CongKetNoiId",
                table: "SanPham",
                column: "CongKetNoiId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_DMSPId",
                table: "SanPham",
                column: "DMSPId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_ManHinhId",
                table: "SanPham",
                column: "ManHinhId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_RamId",
                table: "SanPham",
                column: "RamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CTHD");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "BoNhoRam");

            migrationBuilder.DropTable(
                name: "BoXuLy");

            migrationBuilder.DropTable(
                name: "CongKetNoi");

            migrationBuilder.DropTable(
                name: "DanhMucSanPham");

            migrationBuilder.DropTable(
                name: "ManHinh");

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
