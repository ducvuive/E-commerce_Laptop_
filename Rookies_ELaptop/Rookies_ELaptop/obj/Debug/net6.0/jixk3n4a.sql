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

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'00000000000000_CreateIdentitySchema', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [DiaChi] nvarchar(255) NULL;
GO

ALTER TABLE [AspNetUsers] ADD [Discriminator] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetUsers] ADD [FullName] nvarchar(100) NULL;
GO

ALTER TABLE [AspNetUsers] ADD [GioiTinh] nvarchar(max) NULL;
GO

ALTER TABLE [AspNetUsers] ADD [NgaySinh] datetime2 NULL;
GO

CREATE TABLE [BoNhoRam] (
    [RamId] int NOT NULL IDENTITY,
    [DungLuongRam] nvarchar(100) NULL,
    [LoaiRam] nvarchar(max) NULL,
    [BusRam] nvarchar(100) NULL,
    [HoTroToiDa] nvarchar(max) NULL,
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

CREATE TABLE [HoaDon] (
    [HoaDonId] int NOT NULL IDENTITY,
    [NgayHD] datetime2 NOT NULL,
    [NguoiNhan] nvarchar(max) NULL,
    [SDT] nvarchar(max) NULL,
    [DiaChiGiaoHang] nvarchar(max) NOT NULL,
    [TongTien] bigint NOT NULL,
    [TrangThai] int NULL,
    [MaKhacHangId] nvarchar(450) NULL,
    CONSTRAINT [PK_HoaDon] PRIMARY KEY ([HoaDonId]),
    CONSTRAINT [FK_HoaDon_AspNetUsers_MaKhacHangId] FOREIGN KEY ([MaKhacHangId]) REFERENCES [AspNetUsers] ([Id])
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
    [KichThuocTrongLuong] nvarchar(40) NULL,
    [Webcam] nvarchar(max) NULL,
    [Pin] nvarchar(40) NULL,
    [RaMat] int NULL,
    [MoTa] nvarchar(max) NOT NULL,
    [DonGia] bigint NOT NULL,
    [HinhAnh] nvarchar(max) NULL,
    [BoNhoRamRamId] int NOT NULL,
    CONSTRAINT [PK_SanPham] PRIMARY KEY ([SanPhamId]),
    CONSTRAINT [FK_SanPham_BoNhoRam_BoNhoRamRamId] FOREIGN KEY ([BoNhoRamRamId]) REFERENCES [BoNhoRam] ([RamId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SanPham_BoXuLy_BoXuLyId] FOREIGN KEY ([BoXuLyId]) REFERENCES [BoXuLy] ([BoXuLyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SanPham_CongKetNoi_CongKetNoiId] FOREIGN KEY ([CongKetNoiId]) REFERENCES [CongKetNoi] ([CongKetNoiId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SanPham_DanhMucSanPham_DMSPId] FOREIGN KEY ([DMSPId]) REFERENCES [DanhMucSanPham] ([DMSPId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SanPham_ManHinh_ManHinhId] FOREIGN KEY ([ManHinhId]) REFERENCES [ManHinh] ([ManHinhId]) ON DELETE CASCADE
);
GO

CREATE TABLE [CTHD] (
    [HoaDonId] int NOT NULL,
    [SanPhamId] int NOT NULL,
    [SoLuong] int NULL,
    CONSTRAINT [PK_CTHD] PRIMARY KEY ([SanPhamId], [HoaDonId]),
    CONSTRAINT [FK_CTHD_HoaDon_HoaDonId] FOREIGN KEY ([HoaDonId]) REFERENCES [HoaDon] ([HoaDonId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CTHD_SanPham_SanPhamId] FOREIGN KEY ([SanPhamId]) REFERENCES [SanPham] ([SanPhamId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CTHD_HoaDonId] ON [CTHD] ([HoaDonId]);
GO

CREATE INDEX [IX_HoaDon_MaKhacHangId] ON [HoaDon] ([MaKhacHangId]);
GO

CREATE INDEX [IX_SanPham_BoNhoRamRamId] ON [SanPham] ([BoNhoRamRamId]);
GO

CREATE INDEX [IX_SanPham_BoXuLyId] ON [SanPham] ([BoXuLyId]);
GO

CREATE INDEX [IX_SanPham_CongKetNoiId] ON [SanPham] ([CongKetNoiId]);
GO

CREATE INDEX [IX_SanPham_DMSPId] ON [SanPham] ([DMSPId]);
GO

CREATE INDEX [IX_SanPham_ManHinhId] ON [SanPham] ([ManHinhId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221015140315_create database laptop', N'6.0.9');
GO

COMMIT;
GO

