using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAPI.Migrations
{
    public partial class newdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    DMSPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    isValid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.DMSPId);
                });

            migrationBuilder.CreateTable(
                name: "Connect",
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
                    table.PrimaryKey("PK_Connect", x => x.CongKetNoiId);
                });

            migrationBuilder.CreateTable(
                name: "Processor",
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
                    table.PrimaryKey("PK_Processor", x => x.BoXuLyId);
                });

            migrationBuilder.CreateTable(
                name: "Ram",
                columns: table => new
                {
                    RamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DungLuongRam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LoaiRam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BusRam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HoTroToiDa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ram", x => x.RamId);
                });

            migrationBuilder.CreateTable(
                name: "Screen",
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
                    table.PrimaryKey("PK_Screen", x => x.ManHinhId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    HoaDonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayHD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiNhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiGiaoHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TongTien = table.Column<long>(type: "bigint", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    KhachHangId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.HoaDonId);
                    table.ForeignKey(
                        name: "FK_Invoice_AspNetUsers_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
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
                    KichThuocTrongLuong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Webcam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    RaMat = table.Column<int>(type: "int", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonGia = table.Column<long>(type: "bigint", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanhGia = table.Column<float>(type: "real", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.SanPhamId);
                    table.ForeignKey(
                        name: "FK_Product_Category_DMSPId",
                        column: x => x.DMSPId,
                        principalTable: "Category",
                        principalColumn: "DMSPId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Connect_CongKetNoiId",
                        column: x => x.CongKetNoiId,
                        principalTable: "Connect",
                        principalColumn: "CongKetNoiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Processor_BoXuLyId",
                        column: x => x.BoXuLyId,
                        principalTable: "Processor",
                        principalColumn: "BoXuLyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Ram_RamId",
                        column: x => x.RamId,
                        principalTable: "Ram",
                        principalColumn: "RamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Screen_ManHinhId",
                        column: x => x.ManHinhId,
                        principalTable: "Screen",
                        principalColumn: "ManHinhId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetail",
                columns: table => new
                {
                    HoaDonId = table.Column<int>(type: "int", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetail", x => new { x.SanPhamId, x.HoaDonId });
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_Invoice_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "Invoice",
                        principalColumn: "HoaDonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_Product_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "Product",
                        principalColumn: "SanPhamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<int>(type: "int", nullable: true),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false),
                    KhachHangId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK_Rating_AspNetUsers_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Product_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "Product",
                        principalColumn: "SanPhamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_KhachHangId",
                table: "Invoice",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_HoaDonId",
                table: "InvoiceDetail",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BoXuLyId",
                table: "Product",
                column: "BoXuLyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CongKetNoiId",
                table: "Product",
                column: "CongKetNoiId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DMSPId",
                table: "Product",
                column: "DMSPId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ManHinhId",
                table: "Product",
                column: "ManHinhId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_RamId",
                table: "Product",
                column: "RamId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_KhachHangId",
                table: "Rating",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_SanPhamId",
                table: "Rating",
                column: "SanPhamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "InvoiceDetail");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Connect");

            migrationBuilder.DropTable(
                name: "Processor");

            migrationBuilder.DropTable(
                name: "Ram");

            migrationBuilder.DropTable(
                name: "Screen");
        }
    }
}
