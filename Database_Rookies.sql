create database BackendAPI_Rookies  collate LATIN1_GENERAL_100_CI_AS_SC_UTF8;
use BackendAPI_Rookies;

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [FullName] nvarchar(100) NOT NULL,
    [DiaChi] nvarchar(255) NOT NULL,
    [NgaySinh] datetime2 NULL,
    [GioiTinh] nvarchar(max) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [BoNhoRam] (
    [RamId] int NOT NULL IDENTITY,
    [DungLuongRam] nvarchar(100) NULL,
    [LoaiRam] nvarchar(100) NULL,
    [BusRam] nvarchar(100) NULL,
    [HoTroToiDa] nvarchar(100) NULL,
    CONSTRAINT [PK_BoNhoRam] PRIMARY KEY ([RamId])
);
GO

CREATE TABLE [BoXuLy] (
    [BoXuLyId] int NOT NULL IDENTITY,
    [CongNgheCPU] nvarchar(50) NULL,
    [SoNhan] int NULL,
    [SOLUONG] int NULL,
    [TocDoCPU] nvarchar(20) NULL,
    [ToCDoToiDa] nvarchar(50) NULL,
    [BoNhoDem] nvarchar(20) NULL,
    CONSTRAINT [PK_BoXuLy] PRIMARY KEY ([BoXuLyId])
);
GO

CREATE TABLE [CongKetNoi] (
    [CongKetNoiId] int NOT NULL IDENTITY,
    [CongGiaoTiep] nvarchar(200) NULL,
    [KetNoiKhongDay] nvarchar(100) NULL,
    [KheDocTheNho] nvarchar(20) NULL,
    [Webcam] nvarchar(50) NULL,
    [TinhNangKhac] nvarchar(200) NULL,
    [DenBanPhim] nvarchar(20) NULL,
    CONSTRAINT [PK_CongKetNoi] PRIMARY KEY ([CongKetNoiId])
);
GO

CREATE TABLE [DanhMucSanPham] (
    [DMSPId] int NOT NULL IDENTITY,
    [TenDM] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_DanhMucSanPham] PRIMARY KEY ([DMSPId])
);
GO

CREATE TABLE [ManHinh] (
    [ManHinhId] int NOT NULL IDENTITY,
    [KichThuoc] nvarchar(20) NULL,
    [DoPhanGiai] nvarchar(40) NULL,
    [TanSoQuet] nvarchar(10) NULL,
    [CongNgheMH] nvarchar(100) NULL,
    [CamUng] nvarchar(10) NULL,
    CONSTRAINT [PK_ManHinh] PRIMARY KEY ([ManHinhId])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [HoaDon] (
    [HoaDonId] int NOT NULL IDENTITY,
    [NgayHD] datetime2 NOT NULL,
    [NguoiNhan] nvarchar(100) NULL,
    [SDT] nvarchar(max) NULL,
    [DiaChiGiaoHang] nvarchar(100) NOT NULL,
    [TongTien] bigint NOT NULL,
    [TrangThai] int NULL,
    [MaKhacHangIdId] nvarchar(450) NULL,
    CONSTRAINT [PK_HoaDon] PRIMARY KEY ([HoaDonId]),
    CONSTRAINT [FK_HoaDon_AspNetUsers_MaKhacHangIdId] FOREIGN KEY ([MaKhacHangIdId]) REFERENCES [AspNetUsers] ([Id])
);
GO

CREATE TABLE [SanPham] (
    [SanPhamId] int NOT NULL IDENTITY,
    [ManHinhId] int NOT NULL,
    [BoXuLyId] int NOT NULL,
    [RamId] int NOT NULL,
    [CongKetNoiId] int NOT NULL,
    [DMSPId] int NOT NULL,
    [TenSP] nvarchar(max) NULL,
    [SoLuong] int NULL,
    [MauSac] nvarchar(max) NULL,
    [OCung] nvarchar(max) NULL,
    [CardManHinh] nvarchar(100) NULL,
    [DacBiet] nvarchar(100) NULL,
    [HDH] nvarchar(max) NULL,
    [ThietKe] nvarchar(max) NULL,
    [KichThuocTrongLuong] nvarchar(100) NULL,
    [Webcam] nvarchar(max) NULL,
    [Pin] nvarchar(40) NULL,
    [RaMat] int NULL,
    [MoTa] nvarchar(max) NOT NULL,
    [DonGia] bigint NOT NULL,
    [HinhAnh] nvarchar(max) NULL,
    [DanhGia] real NULL,
    CONSTRAINT [PK_SanPham] PRIMARY KEY ([SanPhamId]),
    CONSTRAINT [FK_SanPham_BoNhoRam_RamId] FOREIGN KEY ([RamId]) REFERENCES [BoNhoRam] ([RamId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SanPham_BoXuLy_BoXuLyId] FOREIGN KEY ([BoXuLyId]) REFERENCES [BoXuLy] ([BoXuLyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SanPham_CongKetNoi_CongKetNoiId] FOREIGN KEY ([CongKetNoiId]) REFERENCES [CongKetNoi] ([CongKetNoiId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SanPham_DanhMucSanPham_DMSPId] FOREIGN KEY ([DMSPId]) REFERENCES [DanhMucSanPham] ([DMSPId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SanPham_ManHinh_ManHinhId] FOREIGN KEY ([ManHinhId]) REFERENCES [ManHinh] ([ManHinhId]) ON DELETE CASCADE
);
GO

CREATE TABLE [CTHD] (
    [HoaDonId] int NOT NULL,
    [SanPhamId] int NOT NULL,
    [SoLuong] int NOT NULL,
    CONSTRAINT [PK_CTHD] PRIMARY KEY ([SanPhamId], [HoaDonId]),
    CONSTRAINT [FK_CTHD_HoaDon_HoaDonId] FOREIGN KEY ([HoaDonId]) REFERENCES [HoaDon] ([HoaDonId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CTHD_SanPham_SanPhamId] FOREIGN KEY ([SanPhamId]) REFERENCES [SanPham] ([SanPhamId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Rating] (
    [RatingID] int NOT NULL IDENTITY,
    [Rate] int NULL,
    [SanPhamId] int NOT NULL,
    CONSTRAINT [PK_Rating] PRIMARY KEY ([RatingID]),
    CONSTRAINT [FK_Rating_SanPham_SanPhamId] FOREIGN KEY ([SanPhamId]) REFERENCES [SanPham] ([SanPhamId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Rating_SanPhamId] ON [Rating] ([SanPhamId]);
GO


CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_CTHD_HoaDonId] ON [CTHD] ([HoaDonId]);
GO

CREATE INDEX [IX_HoaDon_MaKhacHangIdId] ON [HoaDon] ([MaKhacHangIdId]);
GO

CREATE INDEX [IX_SanPham_BoXuLyId] ON [SanPham] ([BoXuLyId]);
GO

CREATE INDEX [IX_SanPham_CongKetNoiId] ON [SanPham] ([CongKetNoiId]);
GO

CREATE INDEX [IX_SanPham_DMSPId] ON [SanPham] ([DMSPId]);
GO

CREATE INDEX [IX_SanPham_ManHinhId] ON [SanPham] ([ManHinhId]);
GO

CREATE INDEX [IX_SanPham_RamId] ON [SanPham] ([RamId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221018104639_create database', N'6.0.10');
GO

COMMIT;
GO

------------ Insert Database ---------------
INSERT INTO [DanhMucSanPham] (TenDM) VALUES ('MacBook');
INSERT INTO [DanhMucSanPham] (TenDM) VALUES ('Asus');
INSERT INTO [DanhMucSanPham] (TenDM) VALUES ('HP');
INSERT INTO [DanhMucSanPham] (TenDM) VALUES ('Lenovo');
INSERT INTO [DanhMucSanPham] (TenDM) VALUES ('Acer');
INSERT INTO [DanhMucSanPham] (TenDM) VALUES ('Dell');
INSERT INTO [DanhMucSanPham] (TenDM) VALUES ('Msi');
INSERT INTO [DanhMucSanPham] (TenDM) VALUES ('LG');
INSERT INTO [DanhMucSanPham] (TenDM) VALUES ('GIGABYTE');


insert into ManHinh values ('15.6 inch','Full HD (1920x1080)','144Hz','Chống chói Anti Glare, Tấm nền IPS','Không');
insert into ManHinh values ('15.6 inch','Full HD (1920x1080)','60Hz','Công nghệ IPSLED','Không');
insert into ManHinh values ('15.6 inch','Full HD (1920x1080)','240Hz','100% DCI-P3, Tấm nền IPS, Chống chói Anti Glare','Không');
insert into ManHinh values ('15.6 inch','Full HD (1920x1080)','360Hz','100% sRGB, Chống chói Anti Glare, Tấm nền IPS','Không');
insert into ManHinh values ('17.3 inch','Full HD (1920x1080)','240Hz','Tấm nền IPS','Không');
insert into ManHinh values ('16 inch','QHD+ (2560 x 1600)','120Hz','100% DCI-P3, Tấm nền IPS','Có');
insert into ManHinh values ('13.4 inch','Full HD+(1920 x 1200)','60Hz','100% sRGB, 500 nits Glossy, Tấm nền IPS','Có');
insert into ManHinh values ('15.6 inch','Full HD (1920x1080)','144Hz','Tấm nền IPS','Không');
insert into ManHinh values ('15.6 inch','Full HD (1920x1080)','144Hz','LED Tấm nền IPS','Không');
insert into ManHinh values ('17 inch','WQXGA (2560 x 1600)','60 Hz','Chống chói Anti Glare, Tấm nền IPS','Không');
insert into ManHinh values ('16 inch','WQXGA (2560 x 1600)','60 Hz','Chống chói Anti Glare, Tấm nền IPS','Không');
insert into ManHinh values ('14 inch','WUXGA (1920 x 1200)','60 Hz','Chống chói Anti Glare, Tấm nền IPS','Không');
insert into ManHinh values ('14 inch','4K/UHD(3840x21600)','60Hz','500 nits Glossy,  90% DCI-P3, HDR Ves400, Tấm nền IPS','có');
insert into ManHinh values ('13 inch','WQHD (2160x1350)','60Hz','Tấm nền IPS, 450 nits Glossy, Dolby Vision','Có');
insert into ManHinh values ('13.3 inch','QHD (2560 x 1600)','60Hz','300 nits Glossy, Chống chói Anti Glarem, Dolby Vision, Tấm nền IPS','Không');
insert into ManHinh values ('14 inch','Full HD (1920 x 1080)','60Hz','300 nits Glossy, Tấm nền IPS','Không');
insert into ManHinh values ('15.6 inch','Full HD (1920 x 1080)','120Hz','Tấm nền IPS, Chống chói Anti-Glare, 250 nits','có');
insert into ManHinh values ('15.6 inch','Full HD (1920 x 1080)','60 Hz','TFT, AcerComfyview, LED backlit','Không');
insert into ManHinh values ('14 inch','Full HD (1920x1080)','144Hz','Chống chói Anti Glare, Tấm nền IPS','Không');
insert into ManHinh values ('15.6 inch','Full HD (1920x1080)','144Hz','Tấm nền IPS, AcerComfyview, LED backlit','Không');
insert into ManHinh values ('15.6 inch','Full HD (1920x1080)','120Hz','LED, WVA','không');
insert into ManHinh values ('15.6 inch','QHD (2560 x 1600)','165Hz','Tấm nền IPS, AcerComfyview, LED backlit, TFT','Không');



        insert into CongKetNoi values ('2 x USB 3.2, 2 x USB Type-C, HDMI, Jack tai nghe 3.5 mm, LAN (RJ45)','Bluetooth 5.1, Wi-Fi 6 (802.11ax)','','HD webcam','','Có');
        insert into CongKetNoi values ('2 x USB 3.2, HDMI, USB 2.0, USB Type-C','Bluetooth 5.1, Wi-Fi 6 (802.11ax)','Micro SD','HD webcam','Bản lề mở 180 độ','Có');
        insert into CongKetNoi values ('Jack tai nghe 3.5 mm, Thunderbolt 4 USB-C, 3 x USB 3.2, HDMI, LAN (RJ45), Mini DisplayPort, USB Type-C','Bluetooth 5.2Wi-Fi 6E (802.11ax)','SD','FHD (30fps@1080p)','Đèn bàn phím chuyển màu RGB','Có');
        insert into CongKetNoi values ('3 x USB 3.2, HDMI, Jack tai nghe 3.5 mm, LAN (RJ45), Thunderbolt 4 USB-C, USB Type-C','Bluetooth 5.2Wi-Fi 6E (802.11ax)','','Camera IR','Mở khóa khuôn mặtĐèn bàn phím chuyển màu RGB','Có');
        insert into CongKetNoi values ('3 x USB 3.2, HDMI, Jack tai nghe 3.5 mm, LAN (RJ45), Thunderbolt 4 USB-C, USB Type-C','Bluetooth 5.2Wi-Fi 6E (802.11ax)','SD','FHD (30fps@1080p)','Đèn bàn phím chuyển màu RGB','Có');
        insert into CongKetNoi values ('Jack tai nghe 3.5 mm, 3 x USB 3.2, HDMI, LAN (RJ45), Mini DisplayPort','Bluetooth 5.2Wi-Fi 6 (802.11ax)','SD','HD webcam','Đèn bàn phím chuyển màu RGB','Có');
        insert into CongKetNoi values ('2 x Thunderbolt 4 USB-C, 2 x USB 3.2, HDMI,Jack tai nghe 3.5 mm','Bluetooth 5.2Wi-Fi 6E (802.11ax)','Micro SD','Camera IR, HD webcam','Bảo mật vân tay, Công tắc khóa camera, Mở khóa khuôn mặt','Có');
        insert into CongKetNoi values ('1 x USB 3.2, 2 x Thunderbolt 4 USB-C, Jack tai nghe 3.5 mmUSB Type-C','Bluetooth 5.2Wi-Fi 6E (802.11ax)','Micro SD','Camera IRHD webcam','Bảo mật vân tay, Công tắc khóa camera, Mở khóa khuôn mặt, Tiêu chuẩn Nền Intel Evo','Có');
        insert into CongKetNoi values ('Jack tai nghe 3.5 mm, 3 x USB 3.2, HDMI, LAN (RJ45), Mini DisplayPort, USB Type-C','Wi-Fi 6 (802.11ax)Bluetooth 5.1','SD','HD webcam','Đèn bàn phím chuyển màu RGB','Có');
        insert into CongKetNoi values ('2 x Thunderbolt 4 USB-C, 2 x USB 3.2, HDMI, Jack tai nghe 3.5 mm','Bluetooth 5.1Wi-Fi 6 (802.11ax)','Micro SD','HD webcam','Bảo mật vân tayTiêu chuẩn Nền Intel Evo','Có');
        insert into CongKetNoi values ('2 x USB 3.2, HDMI, Jack tai nghe 3.5 mm, LAN (RJ45), Mini DisplayPort, USB 2.0, USB Type-C','Bluetooth 5.2Wi-Fi 6 (802.11ax)','SD','HD webcam','','Có');
        insert into CongKetNoi values ('1 x USB 3.2, 2 x Thunderbolt 4 USB-C, Jack tai nghe 3.5 mm','Bluetooth 5.1, Wi-Fi 6 (802.11ax)','','HD webcam','Bảo mật vân tay, Công tắc khóa camera, Tiêu chuẩn Nền Intel Evo','Có');
        insert into CongKetNoi values ('2 x USB Type-C (Power Delivery and DisplayPort), USB Type-C, Jack tai nghe 3.5 mm','Bluetooth 5.0 Wi-Fi 6 (802.11ax)','Micro SD','Sau 5 MP, trước 5 MPCamera IR','Digital Pen, Mở khóa khuôn mặt','có');
        insert into CongKetNoi values ('2 x Thunderbolt 4 USB-C, Jack tai nghe 3.5 mm, USB Type-C','Bluetooth 5.0 Wi-Fi 6 (802.11ax)','','Camera IR , HD webcam','Bản lề mở 180 độ, Mở khóa khuôn mặt, Tiêu chuẩn Nền Intel Evo, Độ bền chuẩn quân đội MLT STD 810G','Có');
        insert into CongKetNoi values ('1 x USB 3.2 (Always on), 2 x Thunderbolt 4 USB-C, Jack tai nghe 3.5 mm','Bluetooth 5.0 Wi-Fi 6 (802.11ax)','','HD webcam','Bảo mật vân tay, Công tắc khóa camera, Tiêu chuẩn Nền Intel Evo, TPM 2.0','Có');
        insert into CongKetNoi values ('1 x USB 3.2, 1 x USB 3.2 (Always on), HDMI, Jack tai nghe 3.5 mm, Thunderbolt 4 USB-C, USB Type-C (support data transfer, Power Delivery 3.0 and DisplayPort 1.4)','Bluetooth 5.0 Wi-Fi 6 (802.11ax)','Micro SD ','HD webcam','Bút cảm ứng, Bảo mật vân tay, Công tắc khóa camera','Có');
        insert into CongKetNoi values ('2 x USB 3.2, 2 x USB Type-C, HDMI, Jack tai nghe 3.5 mm, LAN (RJ45)','Bluetooth 5.0 Wi-Fi 6 (802.11ax)','','HD webcam','Công tắc khóa camera','Có');
        insert into CongKetNoi values ('1 x USB 3.2, 1 x USB 3.2 (Always on), HDMI, Jack tai nghe 3.5 mm, Thunderbolt 4 USB-C, USB Type-C (support data transfer, Power Delivery 3.0 and DisplayPort 1.4)','Bluetooth 5.1Wi-Fi 6 (802.11ax)','Micro SD','HD webcam','Bút cảm ứng, Bảo mật vân tay, Công tắc khóa camera','có');
        insert into CongKetNoi values ('2 x USB 3.2, HDMI, Jack tai nghe 3.5 mm, LAN (RJ45), Thunderbolt 4 USB-C, USB Type-C','Bluetooth 5.1Wi-Fi 6 (802.11ax)','Micro SD','HD webcam','Bản lề mở 180 độ, Bảo mật vân tay, Công tắc khóa camera, Độ bền chuẩn quân đội MLT STD 810H','Không');
        insert into CongKetNoi values ('3 x USB 3.2, HDMI, Jack tai nghe 3.5 mm, LAN (RJ45), USB Type-C','Bluetooth 5.1 Wi-Fi 6 (802.11ax)','','HD webcam','Đèn bàn phím chuyển màu RGB','Có');
        insert into CongKetNoi values ('HDMIUSB 3.12 x USB 2.0LAN (RJ45)Jack tai nghe 3.5 mm','Bluetooth 5.0Wi-Fi 802.11 a/b/g/n/ac','','VGA webcam ','Bản lề mở 180 độ','Không');
        insert into CongKetNoi values ('2 x USB 3.2, HDMI, Jack tai nghe 3.5 mm, LAN (RJ45), USB 2.0','BluetoothWi-Fi 802.11 a/b/g/n/ac','SD','HD webcam','','không');
        insert into CongKetNoi values ('Jack tai nghe 3.5 mm, 1 x USB 3.2, HDMI, LAN (RJ45), Thunderbolt 3, 2 x USB 2.0, Mini DisplayPort','Bluetooth 5.0 Wi-Fi 6 (802.11ax)','SD','HD webcam','Bảo mật vân tay','có');
        insert into CongKetNoi values ('3 x USB 3.2, HDMI, Jack tai nghe 3.5 mm, LAN (RJ45), Mini DisplayPort, Thunderbolt 4 USB-C','Bluetooth 5.1Wi-Fi 6 (802.11ax)','','HD webcam','Công nghệ quạt AeroBlade 3D thế hệ 5 2 quạt tản nhiệtĐèn bàn phím chuyển màu RGB','Có');
        insert into CongKetNoi values ('1 x USB 3.2 2 x USB 2.0 HDMI Jack tai nghe 3.5 mm USB Type-C','Bluetooth 4.1Wi-Fi 802.11 a/b/g/n/ac','',' VGA Webcam',' Bảo mật vân tay','Không');
        insert into congketnoi values ('2 x Thunderbolt 3 (USB-C)Jack tai nghe 3.5 mm','Bluetooth 5.0Wi-Fi 6 (802.11ax)','','720p FaceTime Camera',' Bảo mật vân tay',' Có');     
  
        insert into BONHORAM values ('16 GB','DDR4 2 khe (1 khe 8GB + 1 khe 8GB)','3200MHz','64 GB');
        insert into BONHORAM values ('8 GB','DDR4 2 khe (1 khe 8GB + 1 khe rời)','3200MHz','64 GB');
        insert into BONHORAM values ('32 GB','DDR4 2 khe (1 khe 16GB + 1 khe 16GB)','3200MHz','64 GB');
        insert into BONHORAM values ('16 GB','LPDDR4X (8GB On board + 8GB On board)','2133 MHz (Hãng công bố)','Không hỗ trợ nâng cấp');
        insert into BONHORAM values ('16 GB','LPDDR4 (On board)','2133 MHz (Hãng công bố)','Không hỗ trợ nâng cấp');
        insert into BONHORAM values ('16 GB','DDR4 (2 khe)','2666 MHz','32 GB');
        insert into BONHORAM values ('16 GB','LPDDR4X (On board)','4266 MHz','Không hỗ trợ nâng cấp');
        insert into BONHORAM values ('8 GB','DDR4 2 khe (1 khe 8GB + 1 khe rời)','4266 MHz','');
        insert into BONHORAM values ('16GB','LPDDR4 (On board)','4266 MHz','Không hỗ trợ nâng cấp');
        insert into BONHORAM values ('8GB','DDR4 (On board)','2666 MHz','Không hỗ trợ nâng cấp');
        insert into BONHORAM values ('8GB','DDR4 (On board)','3200 MHz','Không hỗ trợ nâng cấp');
		insert into BONHORAM values ('8GB','DDR4 2 khe (1 khe 8GB onboard + 1 khe trống)','3200MHz','40GB');
        insert into BONHORAM values ('8GB','DDR4 (2 khe)','2933 MHz','16GB');
        insert into BONHORAM values ('16GB','DDR4 2 khe (8GB onboard+ 1 khe 8GB)','3200 MHz','40 GB');
        insert into BONHORAM values ('8 GB','DDR4 2 khe (1 khe 8GB + 1 khe rời)','3200 MHz','32 GB');
        insert into BONHORAM values ('4 GB','DDR4 2 khe (1 khe 4GB + 1 khe rời)','2400 MHz','12 GB');
        insert into BONHORAM values ('8 GB','DDR4 2 khe (1 khe 8GB + 1 khe rời)','2666 MHz','16 GB');
        insert into BONHORAM values ('8 GB','DDR4 2 khe (1 khe 8GB + 1 khe rời)','2666 MHz','32GB');
        insert into BONHORAM values ('16GB','DDR4 2 khe (1 khe 8GB + 1 khe 8GB)','2933 MHz','16GB');
        insert into BONHORAM values ('16 GB','DDR4 2 khe (1 khe 8GB + 1 khe 8GB)','2700 MHz','32 GB');
        insert into BONHORAM values ('16 GB','DDR4 2 khe (1 khe 8GB + 1 khe 8GB)','3200 MHz','32 GB');
        insert into BONHORAM values ('8GB','','','');
        insert into BONHORAM values ('16GB','','','');
        insert into BONHORAM values ('32GB','','','');
        insert into BONHORAM values ('8 GB','LPDDR4X (On board)','4267 MHz','Không hỗ trợ nâng cấp');
        insert into BONHORAM values ('4GB','LPDDR4X (On board)','3200 MHz','20GB');
        insert into BONHORAM values ('8 GB','DDR4 2 khe (1 khe 4GB + 1 khe 4GB)','3200 MHz','32 GB');
  
        insert into BOXULY values ('Intel Core i5 Comet Lake - 10500H',6,12,'2.50 GHz','Turbo Boost 4,5GHZ','12MB');
        insert into BOXULY values ('Intel Core i5 Tiger Lake - 1155G7',4,8,'2.50 GHz','Turbo Boost 4,5GHZ','8MB');
        insert into BOXULY values ('Intel Core i7 Tiger Lake - 11800H',8,16,'2.30 GHz','Turbo Boost 4.6 GHz','24MB');
        insert into BOXULY values ('Intel Core i7 Tiger Lake - 1195G7',4,8,'2.90 GHz','Turbo Boost 5.0 GHz','12MB');
        insert into BOXULY values ('Intel Core i7 Tiger Lake - 1185G7',4,8,'3.00 GHz','Turbo Boost 4.8 GHz','12MB');
        insert into BOXULY values ('Intel Core i7 Comet Lake - 10750H',6,12,'2.60 GHz','Turbo Boost 5.0 GHz','12MB');
        insert into BOXULY values ('Intel Core i7 Tiger Lake - 1165G7',4,8,'2.80 GHz','Turbo Boost 4.7 GHz','12MB');
        insert into BOXULY values ('Intel Core i5 Tiger Lake - 11400H',6,12,'2.70 GHz','Turbo Boost 4,5 GHZ','12MB');
        insert into BOXULY values ('Intel Core i5 Comet Lake - 10500H',6,12,'2.50 GHz','Turbo Boost 4,5 GHZ','12MB');
        insert into BOXULY values ('Intel Core i7 Comet Lake - 10510U',4,8,'1.80 GHz','Turbo Boost 4.9 GHz','8MB');
        insert into BOXULY values ('Intel Core i5 Tiger Lake - 1135G7',4,8,'2.40 GHz','Turbo Boost 4.2 GHz','8 MB');
        insert into BOXULY values ('Intel Core i5 Comet Lake - 10210U',4,8,'1.6GHz','Turbo Boost 4.2 GHz','6 MB');
        insert into BOXULY values ('Intel Core i3 Ice Lake - 1005G1',2,4,'1.2 GHz','Turbo Boost 3.4 GHz','4 MB');
        insert into BOXULY values ('Apple M1','','','','','');
        insert into BOXULY values ('Intel Core i5 Tiger Lake - 1135G7',4,8,'2.40 GHz','Turbo Boost 4.2 GHz','8 MB');
        insert into BOXULY values ('Intel Core i3 Tiger Lake - 1115G4',2,4,'3 GHz','Turbo Boost 4.1 GHz','6 MB');
        insert into BOXULY values ('Intel Core i5 Tiger Lake - 1135G7',4,8,'2.40 GHz','Turbo Boost 4.2 GHz','8MB');
        insert into BOXULY values ('AMD Ryzen 5 - 5600H',6,12,'3.30 GHz','Turbo Boost 4.2 GHz','16 MB');
		
insert into SanPham values (10,14,22,26,01,'Laptop Apple MacBook Air M1 2020 8GB/256GB/7-core GPU (MGN63SA/A)',3,'Trắng','256 GB SSD','Card tích hợp7 nhân GPU','','Mac OS','Vỏ kim loại nguyên khối','Dài 304.1 mm - Rộng 212.4 mm - Dày 4.1 mm đến 16.1 mm - Nặng 1.29 kg','','Khoảng 10 tiếng',2020,'','27490000','/HinhAnh/SP001.jpg',0);
insert into SanPham values (10,14,23,26,01,'Laptop Apple MacBook Pro M1 2020 16GB/512GB (Z11C)',5,'Xám','512 GB SSD','Card tích hợp8 nhân GPU','','Mac OS','Vỏ kim loại nguyên khối','Dài 304.1 mm - Rộng 212.4 mm - Dày 15.6 mm - Nặng 1.4 kg','','Khoảng 10 tiếng',2020,'','44990000','/HinhAnh/SP002.jpg',0);
insert into SanPham values (10,14,23,26,01,'Laptop Apple MacBook Pro M1 2020 16GB/512GB (Z11A)',5,'Đen','512 GB SSD','Card tích hợp8 nhân GPU','','Mac OS','Vỏ kim loại nguyên khối','Dài 304.1 mm - Rộng 212.4 mm - Dày 15.6 mm - Nặng 1.4 kg','','Khoảng 10 tiếng',2020,'','44990001','/HinhAnh/SP003.jpg',0);
insert into SanPham values (12,14,24,26,01,'Laptop Apple MacBook Pro 16 M1 Max 2021 10 core-CPU/32GB/1TB SSD/32 core-GPU (MK1A3SA/A)',4,'Đen','1 TB SSD','Card tích hợp32 core-GPU','','Mac OS','Vỏ kim loại nguyên khối','Dài 355.7 mm - Rộng 248.1 mm - Dày 16.8 mm - Nặng 2.2 kg','','Khoảng 10 tiếng',2021,'','90990000','/HinhAnh/SP004.jpg',0);
insert into SanPham values (11,14,24,26,01,'Laptop Apple MacBook Pro 16 M1 Max 2021 10 core-CPU/32GB/1TB SSD/32 core-GPU (MK1A3SA/A)',3,'Xám','1 TB SSD','Card tích hợp32 core-GPU','','Mac OS','Vỏ kim loại nguyên khối','Dài 355.7 mm - Rộng 248.1 mm - Dày 16.8 mm - Nặng 2.2 kg','','Khoảng 10 tiếng',2021,'','88990000','/HinhAnh/SP005.jpg',0);
insert into SanPham values (12,14,24,26,01,'Laptop Apple MacBook Pro 16 M1 Pro 2021 10 core-CPU/16GB/1TB SSD/16 core-GPU (MK193SA/A)',4,'Đen','1 TB SSD','Card tích hợp32 core-GPU','','Mac OS','Vỏ kim loại nguyên khối','Dài 355.7 mm - Rộng 248.1 mm - Dày 16.8 mm - Nặng 2.2 kg','','Khoảng 10 tiếng',2020,'','45112000','/HinhAnh/SP006.jpg',0);
insert into SanPham values (10,14,23,26,01,'Laptop Apple MacBook Pro M1 2020 16GB/1TB SSD (Z11C000CJ)',2,'Đen','1 TB SSD','Card tích hợp32 core-GPU','','Mac OS','Vỏ kim loại nguyên khối','Dài 355.7 mm - Rộng 248.1 mm - Dày 16.8 mm - Nặng 2.2 kg','','Khoảng 10 tiếng',2020,'','50990000','/HinhAnh/SP007.jpg',0);
insert into SanPham values (15,15,25,01,02,'Laptop Asus ZenBook UX325EA i5 1135G7/8GB/512GB/ OLED/Cáp/Túi/Win10 (KG363T)',5,'Đen','512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card tích hợpIntel Iris Xe','','Windows 10 Home SL','Vỏ kim loại nguyên khối','Dài 304.2 mm - Rộng 203 mm - Dày 13.9 mm - Nặng 1.14 kg','','4-cell Li-ion, 67 Wh',2021,'','23790000','/HinhAnh/SP008.jpg',0);
insert into SanPham values (15,15,25,02,02,'Laptop Asus ZenBook UX325EA i5 1135G7/8GB/512GB/ OLED/Cáp/Túi/Win10 (KG363A)',10,'Xám','512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card tích hợpIntel Iris Xe','','Windows 10 Home SL','Vỏ kim loại nguyên khối','Dài 304.2 mm - Rộng 203 mm - Dày 13.9 mm - Nặng 1.14 kg','','4-cell Li-ion, 67 Wh',2021,'','23790000','/HinhAnh/SP009.jpg',0);
insert into SanPham values (20,07,25,07,02,'Laptop Asus TUF Gaming FX516PC i7 11370H/8GB/512GB/4GB RTX3050/144Hz/Win10 (HN001T)',10,'Đen','513 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card tích hợpIntel Iris Xe','','Windows 10 Home SL','Vỏ kim loại nguyên khối','Dài 305 mm - Rộng 211 mm - Dày 11.9 mm - Nặng 1.3 kg','','4-cell Li-ion, 67 Wh',2020,'','23790000','/HinhAnh/SP010.jpg',0);
insert into SanPham values (20,07,04,08,02,'Laptop Asus ZenBook Flip UX363EA i7 1165G7/16GB/512GB/ OLED/Touch/Pen/Cáp/Túi/Win10 (HP548T) ',12,'Đen','SSD 512 GB NVMe PCIe','Card tích hợpIntel Iris Xe','','Windows 10 Home SL','Vỏ kim loại nguyên khối','Dài 305 mm - Rộng 211 mm - Dày 11.9 mm - Nặng 1.3 kg','','4-cell Li-ion, 67 Wh',2020,'','24020000','/HinhAnh/SP011.jpg',0);
insert into SanPham values (18,06,02,15,02,'Laptop Asus TUF Gaming FX506HC i5 11400H/8GB/512GB/4GB RTX3050/144Hz/Win10 (HN002T)',10,'Đen','SSD 512 GB NVMe PCIeHỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng','Card rờiRTX 3050 4GB','','Windows 10 Home SL','Vỏ kim loại nguyên khối','Dài 305 mm - Rộng 211 mm - Dày 11.9 mm - Nặng 1.3 kg','','4-cell Li-ion, 67 Wh',2021,'','33490000','/HinhAnh/SP012.jpg',0);
insert into SanPham values (19,09,02,15,02,'Laptop Asus VivoBook X415EA i5 1135G7/8GB/512GB/Win10 (EB637T)',10,'Đen','Hỗ trợ khe cắm HDD SATASSD 512 GB NVMe PCIe','Card tích hợpIntel Iris Xe','','Windows 10 Home SL + Office H&S 2019 vĩnh viễn','Vỏ nhựa','Dài 325.4 mm - Rộng 216 mm - Dày 19.9 mm - Nặng 1.55 kg','','2-cell Li-ion, 37 Wh',2020,'','45020000','/HinhAnh/SP013.jpg',0);
insert into SanPham values (02,16,26,17,02,'Laptop Asus VivoBook X515EA i3 1115G4/4GB/256GB/Win10 (BQ994T) ',10,'Đen','Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB)256 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card tích hợpIntel UHD','','Windows 10 Home SL','Vỏ nhựa','Dài 360.2 mm - Rộng 234.9 mm - Dày 19.9 mm - Nặng 1.8 kg','','2-cell Li-ion, 37 Wh',2020,'','49000000','/HinhAnh/SP014.jpg',0);
insert into SanPham values (15,10,12,18,03,'Laptop HP Envy 13 ba1030TU i7 1165G7/8GB/512GB/Office H&S2019/Win10 (2K0B6PA)',10,'Xám','SSD 512 GB NVMe PCIe','Card tích hợpIntel Iris Xe','','Windows 10 Home SL + Office H&S 2019 vĩnh viễn','Vỏ kim loại nguyên khối','Dài 306.5 mm - Rộng 194.6 mm - Dày 16.9 mm - Nặng 1.236 kg','','3 cell Lion',2020,'','30490000','/HinhAnh/SP015.jpg',0);
insert into SanPham values (10,18,14,13,03,'Laptop HP Gaming VICTUS 16 e0175AX R5 5600H/8GB/512GB/4GB RTX3050/144Hz/Win10 (4R0U8PA)',10,'Xám','512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB)Không hỗ trợ khe cắm HDD','Card rờiRTX 3050 4GB','','Windows 10 Home SL','Vỏ nhựa','Dài 370 mm - Rộng 260 mm - Dày 23.5 mm - Nặng 2.46 kg','','2-cell Li-ion, 37 Wh',2020,'','24290000','/HinhAnh/SP016.jpg',0);
insert into SanPham values (17,10,18,25,03,'Laptop HP Omen 15 ek0078TX i7 10750H/16GB/1TB SSD/8GB RTX2070 Max-Q/300Hz/Office H&S2019/Win10 (26Y68PA)',10,'Đen','1 TB SSD M.2 PCIeHỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng','Card rờiRTX 2070 Max-Q 8GB','','Windows 10 Home SL + Office H&S 2019 vĩnh viễn','Kim loại và polycarbonate','Dài 357.9 mm - Rộng 239.7 mm - Dày 22.5 mm - Nặng 2.36 kg','','6-cell Li-ion, 70.9 Wh',2021,'','50490000','/HinhAnh/SP017.jpg',0);
insert into SanPham values (17,10,19,25,03,'Laptop HP EliteBook X360 1030 G8 i7 1165G7/16GB/512GB/ Touch/Pen/Win10 Pro (3G1C4PA)',3,'Đen','SSD 512 GB NVMe PCIe','Card tích hợpIntel Iris Xe','','Windows 10 Pro','Vỏ kim loại nguyên khối','Dài 303.7 mm - Rộng 193.9 mm - Dày 16.1 mm - Nặng 1.25 kg','','4-cell Li-ion, 54 Wh',2021,'','49090000','/HinhAnh/SP018.jpg',0);
insert into SanPham values (18,17,27,25,03,'Laptop HP Pavilion 15 eg0505TU i5 1135G7/8GB/512GB/Win10 (46M02PA)',5,'Đen','SSD 512 GB NVMe PCIe','Card tích hợpIntel Iris Xe','','Windows 10 Home SL','Nắp lưng và chiếu nghỉ tay bằng kim loại','Dài 360.2 mm - Rộng 234 mm - Dày 17.9 mm - Nặng 1.677 kg','','2-cell Li-ion, 37 Wh',2020,'','18790000','/HinhAnh/SP019.jpg',0);
insert into SanPham values (17,17,27,25,03,'Laptop HP Pavilion 15 eg0505TU i5 1135G7/8GB/512GB/Win10 (46M02PB)',3,'Xám','SSD 512 GB NVMe PCIe','Card tích hợpIntel Iris Xe','','Windows 10 Home SL','Nắp lưng và chiếu nghỉ tay bằng kim loại','Dài 360.2 mm - Rộng 234 mm - Dày 17.9 mm - Nặng 1.677 kg','','2-cell Li-ion, 37 Wh',2020,'','18790000','/HinhAnh/SP020.jpg',0);
insert into SanPham values (13,05,09,12,04,'Laptop Lenovo Yoga 9 14ITL5 i7/1185G7/16GB/1TB SSD/Touch/Pen/Win10 (82BG006EVN)',5,'xám','1 TB SSD M.2 PCIe','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL','Vỏ kim loại','Dài 319.4 mm - Rộng 216.4 mm - Dày 15.7 mm - Nặng 1.37 kg',' ','Li-Polymer, 60 Wh',2021,'','29990000','/HinhAnh/SP021.jpg',0);
insert into SanPham values (14,10,10,13,04,'Laptop Lenovo Yoga Duet 7 13IML05 i7 10510U/8GB/512GB/ Touch/Pen/Win10 (82AS007CVN)',4,'đen','SSD 512 GB NVMe PCIe','Card tích hợp - Intel UHD Graphics','','Windows 10 Home SL','Vỏ kim loại','Dài 297.4 mm - Rộng 207.4 mm - Dày 9.19 mm - Nặng 1.1683 kg',' ','Li-Polymer, 42 Wh',2020,'','29990000','/HinhAnh/SP022.jpg',0);
insert into SanPham values (15,11,07,14,04,'Laptop Lenovo YOGA Slim 7 Carbon 13ITL5 i5 1135G7/16GB/512GB/Win10 (82EV0016VN)',4,'trắng','SSD 512 GB NVMe PCIe','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL','Mặt lưng Carbon - Chiếu nghỉ tay bằng Nhôm Magie','Dài 295.88 mm - Rộng 208.85 mm - Dày 15 mm - Nặng 0.966 kg',' ','Li-Polymer, 50 Wh',2020,'','28990000','/HinhAnh/SP023.jpg',0);
insert into SanPham values (16,07,11,15,04,'Laptop Lenovo Yoga 7 14ITL5 i7 1165G7/8GB/512GB/ Touch/Pen/Win10 (82BH00CKVN)',4,'đen','512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL','Vỏ kim loại','Dài 320.4 mm - Rộng 214.6 mm - Dày 17.7 mm - Nặng 1.43 kg',' ','71 Wh',2020,'','28990000','/HinhAnh/SP024.jpg',0);
insert into SanPham values (16,07,12,16,04,'Laptop Lenovo ThinkBook 14s Yoga ITL i7 1165G7/8GB/512GB/ Touch/Pen/Win10 (20WE004EVN)',5,'xám','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB)512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL','Vỏ kim loại','Dài 320 mm - Rộng 216 mm - Dày 16.9 mm - Nặng 1.5 kg',' ','60 Wh',2021,'','27890000','/HinhAnh/SP025.jpg',0);
insert into SanPham values (14,12,10,13,04,'Laptop Lenovo Yoga Duet 7 13IML05 i5 10210U/8GB/512GB/ Touch/Pen/Win10 (82AS007BVN)',5,'đen','SSD 512 GB NVMe PCIe','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL','Vỏ kim loại','Dài 297.4 mm - Rộng 207.4 mm - Dày 9.19 mm - Nặng 1.1683 kg',' ','Li-Polymer, 42 Wh',2020,'','26990000','/HinhAnh/SP026.jpg',0);
insert into SanPham values (17,06,13,17,04,'Laptop Lenovo Ideapad Gaming 3 15I05 i7 10750H/8GB/512GB/4GB GTX1650Ti/120Hz/Win10 (81Y4013UVN)',3,'xanh đen','SSD 512 GB NVMe PCIe, Hỗ trợ khe cắm HDD SATA','Card rời - NVIDIA GeForce GTX 1650Ti 4 GB','','Windows 10 Home SL','Vỏ nhựa','Dài 359 mm - Rộng 249.6 mm - Dày 24.9 mm - Nặng 2.2 kg',' ','45 Wh',2020,'','26990000','/HinhAnh/SP027.jpg',0);
insert into SanPham values (16,11,04,18,04,'Laptop Lenovo ThinkBook 14s Yoga ITL i5 1135G7/16GB/512GB/ Touch/Pen/Win10 (20WE004DVN)',3,'xám','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB) 512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL','Vỏ kim loại','Dài 320 mm - Rộng 216 mm - Dày 16.9 mm - Nặng 1.5 kg',' ','60 Wh',2021,'','25890000','/HinhAnh/SP028.jpg',0);
insert into SanPham values (16,11,12,18,04,'Laptop Lenovo ThinkBook 14s Yoga ITL i5 1135G7/8GB/512GB/ Touch/Pen/Win10 (20WE004CVN)',3,'xám','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB)512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL','Vỏ kim loại','Dài 320 mm - Rộng 216 mm - Dày 16.9 mm - Nặng 1.5 kg','  ','60 Wh',2021,'','24790000','/HinhAnh/SP029.jpg',0);
insert into SanPham values (16,07,12,19,04,'Laptop Lenovo ThinkBook 14 G2 ITL i7 1165G7/8GB/512GB/Win10 (20VD003LVN)',4,'xám','Hỗ trợ khe cắm HDD SATA SSD 512 GB NVMe PCIe','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL','Vỏ kim loại','Dài 323 mm - Rộng 218 mm - Dày 17.9 mm - Nặng 1.4 kg',' ','45 Wh',2020,'','21990000','/HinhAnh/SP030.jpg',0);
insert into SanPham values (20,08,20,20,05,'Laptop Acer Nitro 5 Gaming AN515 57 54AF i5 11400H/16GB/512GB/4GB RTX3050/144Hz/Win11 (NH.QENSV.004)',4,'đen','512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB)Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB)','Card rời - NVIDIA GeForce RTX3050, 4 GB','','Windows 10 Home SL','Vỏ nhựa','Dài 363.4 mm - Rộng 255 mm - Dày 23.9 mm - Nặng 2.2 kg',' ','4-cell Li-ion, 57 Wh',2021,'','28990000','/HinhAnh/SP031.jpg',0);
insert into SanPham values (18,13,16,21,05,'Laptop Acer Aspire A315 56 308N i3 1005G1/4GB/256GB/Win10 (NX.HS5SV.00C)',4,'đen','SSD 256 GB NVMe PCIeHỗ trợ khe cắm HDD SATA','Card tích hợp - Intel UHD Graphics','','Windows 10 Home SL','Vỏ nhựa','Dài 363.4 mm - Rộng 247.5 mm - Dày 19.9 mm - Nặng 1.7 kg                        ',' ','2-Cell Li-ion',2020,'','11790000','/HinhAnh/SP032.jpg',0);
insert into SanPham values (20,03,15,09,05,'Laptop Acer Predator Helios PH315 54 78W5 i7 11800H/8GB/512GB/4GB RTX3050Ti/144Hz/Balo/Win10 (NH.QC5SV.001)',4,'đen','512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB), Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB), Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB)','Card rời - NVIDIA GeForce RTX3050Ti, 4 GB','','Windows 10 Home SL','Vỏ nhựa','Dài 363 mm - Rộng 255 mm - Dày 22.9 mm - Nặng 2.3 kg',' ','4-cell Li-ion, 57 Wh',2021,'','32990000','/HinhAnh/SP033.jpg',0);
insert into SanPham values (20,03,15,20,05,'Laptop Acer Nitro 5 Gaming AN515 57 727J i7 11800H/8GB/512GB/4GB RTX3050Ti/144Hz/Win10 (NH.QD9SV.005.)',4,'đen','Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB)Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card rời - NVIDIA GeForce RTX3050Ti, 4 GB','','Windows 10 Home SL','Vỏ nhựa','Dài 363.4 mm - Rộng 255 mm - Dày 23.9 mm - Nặng 2.2 kg',' ','4-cell Li-ion, 57 Wh',2021,'','29990000','/HinhAnh/SP034.jpg',0);
insert into SanPham values (22,03,20,24,05,'Laptop Acer Predator Triton 300 PT315 53 71DJ i7 11800H/16GB/512GB/8GB RTX3070/165Hz/Win10 (NH.QDSSV.001)',4,'đen','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB)512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card rời - NVIDIA GeForce RTX 3070, 8 GB','','Windows 10 Home SL','Vỏ kim loại','Dài 353 mm - Rộng 255 mm - Dày 19.9 mm - Nặng 2 kg',' ','4-cell Li-ion, 59 Wh',2021,'','44990000','/HinhAnh/SP035.jpg',0);
insert into SanPham values (19,11,17,22,06,'Laptop Dell Vostro 3400 i5 1135G7/8GB/256GB//OfficeH&S 2019/Win10 (70253900)',4,'đen','SSD 256 GB NVMe PCIe, Hỗ trợ khe cắm HDD SATA','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL + Office Home & Student 2019 vĩnh viễn','Vỏ nhựa','Dài 328.7 mm - Rộng 239.5 mm - Dày 19.9 mm - Nặng 1.64 kg',' ','3-cell Li-ion, 42 Wh',2021,'','18890000','/HinhAnh/SP036.jpg',0);
insert into SanPham values (18,11,18,22,06,'Laptop Dell Vostro 3500 i5 1135G7/8GB/512GB/Office H&S2019/Win10 (7G3983)',5,'đen','SSD 512 GB NVMe PCIeHỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB)','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL + Office Home & Student 2019 vĩnh viễn','Vỏ nhựa','Dài 364 mm - Rộng 249 mm - Dày 19.9 mm - Nặng 1.98 kg',' ','3-cell Li-ion, 42 Wh',2021,'','20990000','/HinhAnh/SP037.jpg',0);
insert into SanPham values (21,06,19,23,06,'Laptop Dell Gaming G3 15 i7 10750H/16GB/512GB/6GB GTX1660Ti/120Hz/Win10 (P89F002BWH)',3,'trắng','SSD 512 GB NVMe PCIe Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng','Card rời - NVIDIA GeForce GTX 1660Ti, 6 GB','','windowns 10 Home SL','Vỏ nhựa','Dài 364.46 mm - Rộng 254 mm - Dày 30.96 mm - Nặng 2.58 kg',' ','4-cell Li-ion, 68 Wh',2020,'','31990000','/HinhAnh/SP038.jpg',0);
insert into SanPham values (21,06,19,23,06,'Laptop Dell Gaming G3 i7 10750H/16GB/512GB/6GB GTX1660Ti/120Hz/Win10 (P89F002G3500B)',6,'đen','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộngSSD 512 GB NVMe PCIe','Card rời - NVIDIA GeForce GTX 1660Ti, 6 GB','','Windows 10 Home SL','Vỏ nhựa','Dài 364.46 mm - Rộng 254 mm - Dày 30.96 mm - Nặng 2.58 kg',' ','4-cell Li-ion, 68 Wh',2020,'','31990000','/HinhAnh/SP039.jpg',0);
insert into SanPham values (21,06,19,23,06,'Laptop Dell Gaming G3 15 i7 10750H/16GB/512GB/4GB GTX1650Ti/120Hz/Win10 (P89F002DBL)',5,'đen','Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB) SSD 512 GB NVMe PCIe','Card rời - NVIDIA GeForce GTX 1650Ti 4 GB','','Windows 10 Home SL','Vỏ nhựa','Dài 364.46 mm - Rộng 254 mm - Dày 30.96 mm - Nặng 2.58 kg',' ','3-cell Li-ion, 51 Wh',2020,'','29990000','/HinhAnh/SP040.jpg',0);
insert into SanPham values (01,01,01,01,07,'Laptop MSI Gaming GF65 Thin 10UE i5 10500H/16GB/512GB/6GB RTX3060 Max-Q/144Hz/Balo/Win10 (286VN)',5,'Đen','512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 2TB)Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 2TB)','Card rời - NVIDIA GeForce RTX 3060 Max-Q, 6 GB','','Windows 10 Home SL','Vỏ nhựa','Dài 359 mm - Rộng 254 mm - Dày 21.7 mm - Nặng 1.86 kg','','3-cell, 52 Wh',2021,'','18990000','/HinhAnh/SP041.png',0);
insert into SanPham values (02,02,02,02,07,'Laptop MSI Modern 15 A11MU i5 1155G7/8GB/512GB/Túi/Chuột/Win10 (680VN)',5,'Xám','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 2TB)512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 2TB)','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL','Vỏ kim loại','Dài 356.8 mm - Rộng 233.75 mm - Dày 16.9 mm - Nặng 1.6 kg','','4-cell, 99.9 Wh',2021,'','79990000','/HinhAnh/SP042.png',0);
insert into SanPham values (03,03,03,03,07,'Laptop MSI Gaming GE66 Raider 11UH i7 11800H/32GB/2TB SSD/16GB RTX3080/240Hz/Balo/Chuột/Win10 (259VN)',4,'Xám','2 TB SSD NVMe PCIeHỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 2TB)','Card rời - NVIDIA GeForce RTX 3080, 16 GB','','Windows 10 Home SL','Vỏ kim loại','Dài 358 mm - Rộng 267 mm - Dày 23.4 mm - Nặng 2.38 Kg','','4-cell, 99.9 Wh',2021,'','64990000','/HinhAnh/SP043.png',0);
insert into SanPham values (04,03,03,04,07,'Laptop MSI Gaming GS66 Stealth 11UG i7 11800H/32GB/2TB SSD/8GB RTX3070 Max-Q/360Hz/Balo/Chuột/Win10 (219VN)',3,'Đen','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 2TB)2 TB SSD NVMe PCIe','Card rời - NVIDIA GeForce RTX 3070 Max-Q, 8 GB','','Windows 10 Home SL','Vỏ kim loại','Dài 358.3 mm - Rộng 248 mm - Dày 19.8 mm - Nặng 2.1 Kg','','4-cell, 99.9 Wh',2021,'','59990000','/HinhAnh/SP044.png',0);
insert into SanPham values (04,03,01,05,07,'Laptop MSI Gaming GE66 Raider 11UG i7 11800H/16GB/2TB SSD/8GB RTX3070/360Hz/Balo/Chuột/Win10 (258VN) ',3,'Đen','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 2TB)2 TB SSD NVMe PCIe','Card rời - NVIDIA GeForce RTX 3070, 8 GB','','Windows 10 Home SL','Vỏ kim loại','Dài 358 mm - Rộng 267 mm - Dày 23.4 mm - Nặng 2.38 Kg','','4-cell Li-ion, 65 Wh',2021,'','52990000','/HinhAnh/SP045.png',0);
insert into SanPham values (05,03,01,06,07,'Laptop MSI Gaming GP76 11UG i7 11800H/16GB/1TB SSD/8GB RTX3070/240Hz/Balo/Chuột/Win10 (435VN)',2,'Đen','1 TB SSD M.2 PCIeHỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng','Card rời - NVIDIA GeForce RTX 3070, 8 GB','','Windows 10 Home SL','Vỏ kim loại','Dài 397 mm - Rộng 284 mm - Dày 25.9 mm - Nặng 2.9 kg','','4-cell, 82Wh',2021,'','46490000','/HinhAnh/SP046.png',0);
insert into SanPham values (06,04,04,07,07,'Laptop MSI Summit E16 Flip i7 1195G7/16GB/1TB SSD/4GB RTX3050/120Hz/ Touch/Pen/Túi/Office365F/Win10 (082VN) ',3,'Đen','1 TB SSD M.2 PCIe (Có thể tháo ra, lắp thanh khác tối đa 2TB)','Card rời - NVIDIA GeForce RTX3050, 4 GB','','Windows 10 Home SL + Office 365 Family 1 năm','Vỏ kim loại','Dài 358.2 mm - Rộng 258.5 mm - Dày 16.9 mm - Nặng 1.9 kg','','4-cell Li-ion, 70 Wh',2021,'','39990000','/HinhAnh/SP047.png',0);
insert into SanPham values (07,05,05,08,07,'Laptop MSI Summit E13 Flip i7 1185G7/16GB/1TB SSD/Touch/Túi/Pen/Win10 (211VN)',3,'Đen','SSD 1 TB NVMe PCIe Gen4x4','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home SL','Vỏ kim loại nguyên khối','Dài 300.2 mm - Rộng 222.25 mm - Dày 14.9 mm - Nặng 1.35 kg','','6-cell Li-ion',2020,'','32490000','/HinhAnh/SP048.png',0);
insert into SanPham values (08,06,06,09,07,'Laptop MSI Gaming Leopard 10SDK GL65 i7 10750H/16GB/512GB/144Hz/6GB GTX1660Ti/Balo/Win10 (242VN)',3,'Đen','SSD 512 GB NVMe PCIeHỗ trợ khe cắm HDD SATA','Card rời - NVIDIA GeForce GTX 1660Ti, 6 GB','','Windows 10 Home SL','Vỏ nhựa','Dài 359 mm - Rộng 254 mm - Dày 21.7 mm - Nặng 2.3 kg','','3-cell Li-ion, 51 Wh',2021,'','31990000','/HinhAnh/SP049.png',0);
insert into SanPham values (09,06,01,01,07,'Laptop MSI Gaming GF65 10UE i7 10750H/16GB/512GB/6GB RTX3060 Max-Q/Balo/Win10 (228VN) ',3,'Đen','SSD 512 GB NVMe PCIeHỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng','Card rời - NVIDIA GeForce RTX 3060 Max-Q, 6 GB','','Windows 10 Home SL','Vỏ nhựa - nắp lưng bằng kim loại','Dài 359 mm - Rộng 254 mm - Dày 21.7 mm - Nặng 1.86 kg','','2-cell Li-ion, 80 Wh',2020,'','54890000','/HinhAnh/SP050.png',0);
insert into SanPham values (10,07,07,10,26,'Laptop LG G 17 2021 i7 1165G7/16GB/1TB SSD/Win10 (17Z90P-G.AH78A5) ',3,'Đen','1 TB SSD M.2 PCIeHỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home Standard','Hợp kim Nano Carbon - Magie','Dài 380.2 mm - Rộng 260.1 mm - Dày 17.8 mm - Nặng 1.35 kg','','2-cell Li-ion, 80 Wh',2020,'','52890000','/HinhAnh/SP051.png',0);
insert into SanPham values (10,07,07,10,26,'Laptop LG G 17 2021 i7 1165G7/16GB/512GB/Win10 (17Z90P-G.AH76A5) ',3,'Bạc','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộngSSD 512 GB NVMe PCIe','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home Standard','Hợp kim Nano Carbon - Magie','Dài 380.2 mm - Rộng 260.1 mm - Dày 17.8 mm - Nặng 1.35 kg','','2-cell Li-ion, 80 Wh',2020,'','50890000','/HinhAnh/SP052.png',0);
insert into SanPham values (11,07,07,10,26,'Laptop LG G 16 2021 i7 1165G7/16GB/512GB/Win10 (16Z90P-G.AH75A5)',3,'Đen','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộngSSD 512 GB NVMe PCIe','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home Standard','Hợp kim Nano Carbon - Magie','Dài 355.9 mm - Rộng 243.4 mm - Dày 16.8 mm - Nặng 1.19 kg','','2-cell Li-ion, 80 Wh',2020,'','48890000','/HinhAnh/SP053.png',0);
insert into SanPham values (11,07,07,10,26,'Laptop LG G 16 2021 i7 1165G7/16GB/256GB/Win10 (16Z90P-G.AH73A5) ',3,'Bạc','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộngSSD 256 GB NVMe PCIe','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home Standard','Hợp kim Nano Carbon - Magie','Dài 355.9 mm - Rộng 243.4 mm - Dày 16.8 mm - Nặng 1.19 kg','','4-cell Li-ion, 72 Wh',2021,'','47890000','/HinhAnh/SP054.png',0);
insert into SanPham values (12,07,07,10,26,'Laptop LG G 14 2021 i7 1165G7/16GB/512GB/Win 10 (14Z90P-G.AH75A5)',3,'Đen','Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộngSSD 512 GB NVMe PCIe','Card tích hợp - Intel Iris Xe Graphics','','Windows 10 Home Standard','Hợp kim Nano Carbon - Magie','Dài 313.4 mm - Rộng 215.2 mm - Dày 16.8 mm - Nặng 0.999 kg','','4-cell Li-ion, 41 Wh',2021,'','29990000','/HinhAnh/SP055.png',0);
insert into SanPham values (01,08,01,11,27,'Laptop GIGABYTE Gaming G5 i5 11400H/16GB/512GB/4GB RTX3050Ti/144Hz/Win10 (51S1123SH) ',4,'Đen','Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB), Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB), 512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card rời - NVIDIA GeForce RTX3050Ti, 4 GB','','Windows 10 Home SL','Vỏ nhựa','Dài 361 mm - Rộng 258 mm - Dày 24.9 mm - Nặng 2.2 kg','','4-cell Li-ion, 41 Wh',2021,'','29990000','/HinhAnh/SP056.png',0);
insert into SanPham values (01,09,01,11,27,'Laptop GIGABYTE Gaming G5 i5 10500H/16GB/512GB/6GB RTX3060/144Hz/Win10 (5S11130SH)',3,'Đen','Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB), Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB), 512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB)','Card rời - NVIDIA GeForce RTX 3060, 6 GB','','Windows 10 Home SL','Vỏ nhựa','Dài 361 mm - Rộng 258 mm - Dày 24.9 mm - Nặng 2.2 kg','','4-cell Li-ion, 41 Wh',2021,'','26990000','/HinhAnh/SP057.png',0);
insert into SanPham values (01,08,01,11,27,'Laptop Gigabyte G5 i5 11400H/16GB/512GB/4GB RTX3050/144Hz/Win10 (51S1121SH)',3,'Đen','512 GB SSD NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB), Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB), Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB)','Card rời - NVIDIA GeForce RTX3050, 4 GB','','Windows 10 Home SL','Vỏ nhựa','Dài 361 mm - Rộng 258 mm - Dày 24.9 mm - Nặng 2.2 kg','','4-cell Li-ion, 41 Wh',2021,'','26990000','/HinhAnh/SP058.png',0);

