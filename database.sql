create database BackendAPI_Rookies_Final  collate LATIN1_GENERAL_100_CI_AS_SC_UTF8;
use BackendAPI_Rookies_Final;

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
    [DiaChi] nvarchar(255) NULL,
    [NgaySinh] datetime2 NULL,
    [GioiTinh] nvarchar(max) NULL,
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

CREATE TABLE [Category] (
    [CategoryId] int NOT NULL IDENTITY,
    [CategoryName] nvarchar(50) NOT NULL,
    [Description] nvarchar(100) NULL,
    [isDisable] int NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryId])
);
GO

--CREATE TABLE [Connect] (
--    [CongKetNoiId] int NOT NULL IDENTITY,
--    [CongGiaoTiep] nvarchar(200) NULL,
--    [KetNoiKhongDay] nvarchar(100) NULL,
--    [KheDocTheNho] nvarchar(20) NULL,
--    [Webcam] nvarchar(50) NULL,
--    [TinhNangKhac] nvarchar(200) NULL,
--    [DenBanPhim] nvarchar(20) NULL,
--    CONSTRAINT [PK_Connect] PRIMARY KEY ([CongKetNoiId])
--);
--GO

CREATE TABLE [Processor] (
    [ProcessorId] int NOT NULL IDENTITY,
    [CPUTechnology] nvarchar(50) NULL,
    [Multiplier] int NULL,
    [Thread] int NULL,
    [Speed] nvarchar(20) NULL,
    [MaxSpeed] nvarchar(50) NULL,
    [Cache] nvarchar(20) NULL,
    CONSTRAINT [PK_Processor] PRIMARY KEY ([ProcessorId])
);
GO

CREATE TABLE [Ram] (
    [RamId] int NOT NULL IDENTITY,
    [Capacity] nvarchar(100) NULL,
    [Type] nvarchar(100) NULL,
    [BusRam] nvarchar(100) NULL,
    [MaxSupport] nvarchar(100) NULL,
    CONSTRAINT [PK_Ram] PRIMARY KEY ([RamId])
);
GO

CREATE TABLE [Screen] (
    [ScreenId] int NOT NULL IDENTITY,
    [Size] nvarchar(20) NULL,
    --[DoPhanGiai] nvarchar(40) NULL,
    --[TanSoQuet] nvarchar(10) NULL,
    --[CongNgheMH] nvarchar(100) NULL,
    --[CamUng] nvarchar(10) NULL,
    CONSTRAINT [PK_Screen] PRIMARY KEY ([ManHinhId])
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

CREATE TABLE [Invoice] (
    [InvoiceId] int NOT NULL IDENTITY,
    [DateReceived] datetime2 NOT NULL,
    [Receiver] nvarchar(100) NULL,
    [Phone] nvarchar(max) NULL,
    [Address] nvarchar(100) NOT NULL,
    [Total] bigint NOT NULL,
    [Status] int NULL,
    [KhachHangId] nvarchar(450) NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY ([InvoiceId]),
    CONSTRAINT [FK_Invoice_AspNetUsers_KhachHangId] FOREIGN KEY ([KhachHangId]) REFERENCES [AspNetUsers] ([Id])
);
GO

CREATE TABLE [Product] (
    [ProductId] int NOT NULL IDENTITY,
    [ScreenId] int NOT NULL,
    [ProcessorId] int NOT NULL,
    [RamId] int NOT NULL,
    --[CongKetNoiId] int NOT NULL,
    [CategoryId] int NOT NULL,
    [Name] nvarchar(max) NULL,
    [Quantity] int NULL,
    --[MauSac] nvarchar(max) NULL,
    --[OCung] nvarchar(max) NULL,
    --[CardManHinh] nvarchar(100) NULL,
    --[DacBiet] nvarchar(100) NULL,
    --[HDH] nvarchar(max) NULL,
    --[ThietKe] nvarchar(max) NULL,
    --[KichThuocTrongLuong] nvarchar(100) NULL,
    --[Webcam] nvarchar(max) NULL,
    --[Pin] nvarchar(40) NULL,
    --[RaMat] int NULL,
    --[MoTa] nvarchar(max) NULL,
    [DonGia] bigint NULL,
    [HinhAnh] nvarchar(max) NULL,
    [DanhGia] real NULL,
    [PublishedDate] datetime2 NULL,
    [UpdatedDate] datetime2 NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK_Product_Category_DMSPId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([DMSPId]) ON DELETE CASCADE,
    --CONSTRAINT [FK_Product_Connect_CongKetNoiId] FOREIGN KEY ([CongKetNoiId]) REFERENCES [Connect] ([CongKetNoiId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Product_Processor_BoXuLyId] FOREIGN KEY ([ProcesserId]) REFERENCES [Processor] ([ProcessorId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Product_Ram_RamId] FOREIGN KEY ([RamId]) REFERENCES [Ram] ([RamId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Product_Screen_ManHinhId] FOREIGN KEY ([ScreenId]) REFERENCES [Screen] ([ManHinhId]) ON DELETE CASCADE
);
GO

CREATE TABLE [InvoiceDetail] (
    [InvoiceId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY ([SanPhamId], [HoaDonId]),
    CONSTRAINT [FK_InvoiceDetail_Invoice_HoaDonId] FOREIGN KEY ([HoaDonId]) REFERENCES [Invoice] ([HoaDonId]) ON DELETE CASCADE,
    CONSTRAINT [FK_InvoiceDetail_Product_SanPhamId] FOREIGN KEY ([SanPhamId]) REFERENCES [Product] ([SanPhamId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Rating] (
    [RatingID] int NOT NULL IDENTITY,
    [Rate] int NULL,
    [PublishedDate] datetime2 NOT NULL,
    [Comments] nvarchar(max) NOT NULL,
    [SanPhamId] int NOT NULL,
    [KhachHangId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Rating] PRIMARY KEY ([RatingID]),
    CONSTRAINT [FK_Rating_AspNetUsers_KhachHangId] FOREIGN KEY ([KhachHangId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Rating_Product_SanPhamId] FOREIGN KEY ([SanPhamId]) REFERENCES [Product] ([SanPhamId]) ON DELETE CASCADE
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

CREATE INDEX [IX_Invoice_KhachHangId] ON [Invoice] ([KhachHangId]);
GO

CREATE INDEX [IX_InvoiceDetail_HoaDonId] ON [InvoiceDetail] ([HoaDonId]);
GO

CREATE INDEX [IX_Product_BoXuLyId] ON [Product] ([BoXuLyId]);
GO

CREATE INDEX [IX_Product_CongKetNoiId] ON [Product] ([CongKetNoiId]);
GO

CREATE INDEX [IX_Product_DMSPId] ON [Product] ([DMSPId]);
GO

CREATE INDEX [IX_Product_ManHinhId] ON [Product] ([ManHinhId]);
GO

CREATE INDEX [IX_Product_RamId] ON [Product] ([RamId]);
GO

CREATE INDEX [IX_Rating_KhachHangId] ON [Rating] ([KhachHangId]);
GO

CREATE INDEX [IX_Rating_SanPhamId] ON [Rating] ([SanPhamId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221113044737_new database', N'6.0.10');
GO

COMMIT;
GO


------------ Insert Database ---------------
------------ Insert [Category] ---------------
INSERT INTO [dbo].[Category]  VALUES ('MacBook','Đây là Macbook',1);
INSERT INTO [dbo].[Category]  VALUES ('Asus','Đây là Asus',1);
INSERT INTO [dbo].[Category]  VALUES ('HP','Đây là HP',1);
INSERT INTO [dbo].[Category]  VALUES ('Lenovo','Đây là Lenovo',1);
INSERT INTO [dbo].[Category]  VALUES ('Acer','Đây là Acer',1);
INSERT INTO [dbo].[Category]  VALUES ('Dell','Đây là Dell',1);
INSERT INTO [dbo].[Category]  VALUES ('Msi','Đây là Msi',1);
INSERT INTO [dbo].[Category]  VALUES ('LG','Đây là LG',1);
INSERT INTO [dbo].[Category]  VALUES ('GIGABYTE','Đây là GIGABYTE',1);

------------ Insert [Screen] ---------------
insert into Screen values ('15.6 inch Full HD (1920x1080)');
insert into Screen values ('15.6 inch Full HD (1920x1080)');
insert into Screen values ('15.6 inch Full HD (1920x1080)');
insert into Screen values ('15.6 inch Full HD (1920x1080)');
insert into Screen values ('17.3 inch Full HD (1920x1080)');
insert into Screen values ('16 inch QHD+ (2560 x 1600)');
insert into Screen values ('13.4 inch Full HD+(1920 x 1200)');
insert into Screen values ('15.6 inch Full HD (1920x1080)');
insert into Screen values ('15.6 inch Full HD (1920x1080)');
insert into Screen values ('17 inch WQXGA (2560 x 1600)');
insert into Screen values ('16 inch WQXGA (2560 x 1600)');
insert into Screen values ('14 inch WUXGA (1920 x 1200)');
insert into Screen values ('14 inch 4K/UHD(3840x21600)');
insert into Screen values ('13 inch WQHD (2160x1350)');
insert into Screen values ('13.3 inch QHD (2560 x 1600)');
insert into Screen values ('14 inch Full HD (1920 x 1080)');
insert into Screen values ('15.6 inch Full HD (1920 x 1080)');
insert into Screen values ('15.6 inch Full HD (1920 x 1080)');
insert into Screen values ('14 inch Full HD (1920x1080)');
insert into Screen values ('15.6 inch Full HD (1920x1080)');
insert into Screen values ('15.6 inch Full HD (1920x1080)');
insert into Screen values ('15.6 inch QHD (2560 x 1600)');
DBCC CHECKIDENT ('Screen', RESEED, 0);
------------ Insert [Ram] ---------------
insert into Ram values ('16 GB','DDR4 2 khe (1 khe 8GB + 1 khe 8GB)','3200MHz','64 GB');
insert into Ram values ('8 GB','DDR4 2 khe (1 khe 8GB + 1 khe rời)','3200MHz','64 GB');
insert into Ram values ('32 GB','DDR4 2 khe (1 khe 16GB + 1 khe 16GB)','3200MHz','64 GB');
insert into Ram values ('16 GB','LPDDR4X (8GB On board + 8GB On board)','2133 MHz (Hãng công bố)','Không hỗ trợ nâng cấp');
insert into Ram values ('16 GB','LPDDR4 (On board)','2133 MHz (Hãng công bố)','Không hỗ trợ nâng cấp');
insert into Ram values ('16 GB','DDR4 (2 khe)','2666 MHz','32 GB');
insert into Ram values ('16 GB','LPDDR4X (On board)','4266 MHz','Không hỗ trợ nâng cấp');
insert into Ram values ('8 GB','DDR4 2 khe (1 khe 8GB + 1 khe rời)','4266 MHz','');
insert into Ram values ('16GB','LPDDR4 (On board)','4266 MHz','Không hỗ trợ nâng cấp');
insert into Ram values ('8GB','DDR4 (On board)','2666 MHz','Không hỗ trợ nâng cấp');
insert into Ram values ('8GB','DDR4 (On board)','3200 MHz','Không hỗ trợ nâng cấp');
insert into Ram values ('8GB','DDR4 2 khe (1 khe 8GB onboard + 1 khe trống)','3200MHz','40GB');
insert into Ram values ('8GB','DDR4 (2 khe)','2933 MHz','16GB');
insert into Ram values ('16GB','DDR4 2 khe (8GB onboard+ 1 khe 8GB)','3200 MHz','40 GB');
insert into Ram values ('8 GB','DDR4 2 khe (1 khe 8GB + 1 khe rời)','3200 MHz','32 GB');
insert into Ram values ('4 GB','DDR4 2 khe (1 khe 4GB + 1 khe rời)','2400 MHz','12 GB');
insert into Ram values ('8 GB','DDR4 2 khe (1 khe 8GB + 1 khe rời)','2666 MHz','16 GB');
insert into Ram values ('8 GB','DDR4 2 khe (1 khe 8GB + 1 khe rời)','2666 MHz','32GB');
insert into Ram values ('16GB','DDR4 2 khe (1 khe 8GB + 1 khe 8GB)','2933 MHz','16GB');
insert into Ram values ('16 GB','DDR4 2 khe (1 khe 8GB + 1 khe 8GB)','2700 MHz','32 GB');
insert into Ram values ('16 GB','DDR4 2 khe (1 khe 8GB + 1 khe 8GB)','3200 MHz','32 GB');
insert into Ram values ('8GB','','','');
insert into Ram values ('16GB','','','');
insert into Ram values ('32GB','','','');
insert into Ram values ('8 GB','LPDDR4X (On board)','4267 MHz','Không hỗ trợ nâng cấp');
insert into Ram values ('4GB','LPDDR4X (On board)','3200 MHz','20GB');
insert into Ram values ('8 GB','DDR4 2 khe (1 khe 4GB + 1 khe 4GB)','3200 MHz','32 GB');
 
 ------------ Insert [Processor] ---------------
insert into Processor values ('Intel Core i5 Comet Lake - 10500H',6,12,'2.50 GHz','Turbo Boost 4,5GHZ','12MB');
insert into Processor values ('Intel Core i5 Tiger Lake - 1155G7',4,8,'2.50 GHz','Turbo Boost 4,5GHZ','8MB');
insert into Processor values ('Intel Core i7 Tiger Lake - 11800H',8,16,'2.30 GHz','Turbo Boost 4.6 GHz','24MB');
insert into Processor values ('Intel Core i7 Tiger Lake - 1195G7',4,8,'2.90 GHz','Turbo Boost 5.0 GHz','12MB');
insert into Processor values ('Intel Core i7 Tiger Lake - 1185G7',4,8,'3.00 GHz','Turbo Boost 4.8 GHz','12MB');
insert into Processor values ('Intel Core i7 Comet Lake - 10750H',6,12,'2.60 GHz','Turbo Boost 5.0 GHz','12MB');
insert into Processor values ('Intel Core i7 Tiger Lake - 1165G7',4,8,'2.80 GHz','Turbo Boost 4.7 GHz','12MB');
insert into Processor values ('Intel Core i5 Tiger Lake - 11400H',6,12,'2.70 GHz','Turbo Boost 4,5 GHZ','12MB');
insert into Processor values ('Intel Core i5 Comet Lake - 10500H',6,12,'2.50 GHz','Turbo Boost 4,5 GHZ','12MB');
insert into Processor values ('Intel Core i7 Comet Lake - 10510U',4,8,'1.80 GHz','Turbo Boost 4.9 GHz','8MB');
insert into Processor values ('Intel Core i5 Tiger Lake - 1135G7',4,8,'2.40 GHz','Turbo Boost 4.2 GHz','8 MB');
insert into Processor values ('Intel Core i5 Comet Lake - 10210U',4,8,'1.6GHz','Turbo Boost 4.2 GHz','6 MB');
insert into Processor values ('Intel Core i3 Ice Lake - 1005G1',2,4,'1.2 GHz','Turbo Boost 3.4 GHz','4 MB');
insert into Processor values ('Apple M1','','','','','');
insert into Processor values ('Intel Core i5 Tiger Lake - 1135G7',4,8,'2.40 GHz','Turbo Boost 4.2 GHz','8 MB');
insert into Processor values ('Intel Core i3 Tiger Lake - 1115G4',2,4,'3 GHz','Turbo Boost 4.1 GHz','6 MB');
insert into Processor values ('Intel Core i5 Tiger Lake - 1135G7',4,8,'2.40 GHz','Turbo Boost 4.2 GHz','8MB');
insert into Processor values ('AMD Ryzen 5 - 5600H',6,12,'3.30 GHz','Turbo Boost 4.2 GHz','16 MB');

 ------------ Insert [Product] ---------------

insert into Product values (10,14,22,01,'Laptop Apple MacBook Air M1 2020 8GB/256GB/7-core GPU (MGN63SA/A)',3,'27490000','/HinhAnh/SP001.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (10,14,23,01,'Laptop Apple MacBook Pro M1 2020 16GB/512GB (Z11C)',5,'44990000','/HinhAnh/SP002.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (10,14,23,01,'Laptop Apple MacBook Pro M1 2020 16GB/512GB (Z11A)',5,'44990001','/HinhAnh/SP003.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (12,14,24,01,'Laptop Apple MacBook Pro 16 M1 Max 2021 10 core-CPU/32GB/1TB SSD/32 core-GPU (MK1A3SA/A)',4,'90990000','/HinhAnh/SP004.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (11,14,24,01,'Laptop Apple MacBook Pro 16 M1 Max 2021 10 core-CPU/32GB/1TB SSD/32 core-GPU (MK1A3SA/A)',3,'88990000','/HinhAnh/SP005.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (12,14,24,01,'Laptop Apple MacBook Pro 16 M1 Pro 2021 10 core-CPU/16GB/1TB SSD/16 core-GPU (MK193SA/A)',4,'45112000','/HinhAnh/SP006.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (10,14,23,01,'Laptop Apple MacBook Pro M1 2020 16GB/1TB SSD (Z11C000CJ)',2,'50990000','/HinhAnh/SP007.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (15,15,25,02,'Laptop Asus ZenBook UX325EA i5 1135G7/8GB/512GB/ OLED/Cáp/Túi/Win10 (KG363T)',5,'23790000','/HinhAnh/SP008.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (15,15,25,02,'Laptop Asus ZenBook UX325EA i5 1135G7/8GB/512GB/ OLED/Cáp/Túi/Win10 (KG363A)',10,'23790000','/HinhAnh/SP009.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (20,07,25,02,'Laptop Asus TUF Gaming FX516PC i7 11370H/8GB/512GB/4GB RTX3050/144Hz/Win10 (HN001T)',10,'23790000','/HinhAnh/SP010.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (20,07,04,02,'Laptop Asus ZenBook Flip UX363EA i7 1165G7/16GB/512GB/ OLED/Touch/Pen/Cáp/Túi/Win10 (HP548T) ',12,'24020000','/HinhAnh/SP011.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (18,06,02,02,'Laptop Asus TUF Gaming FX506HC i5 11400H/8GB/512GB/4GB RTX3050/144Hz/Win10 (HN002T)',10,'33490000','/HinhAnh/SP012.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (10,18,14,03,'Laptop HP Gaming VICTUS 16 e0175AX R5 5600H/8GB/512GB/4GB RTX3050/144Hz/Win10 (4R0U8PA)',10,'24290000','/HinhAnh/SP016.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (17,10,18,03,'Laptop HP Omen 15 ek0078TX i7 10750H/16GB/1TB SSD/8GB RTX2070 Max-Q/300Hz/Office H&S2019/Win10 (26Y68PA)',10,'50490000','/HinhAnh/SP017.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (17,10,19,03,'Laptop HP EliteBook X360 1030 G8 i7 1165G7/16GB/512GB/ Touch/Pen/Win10 Pro (3G1C4PA)',3,'49090000','/HinhAnh/SP018.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (18,17,27,03,'Laptop HP Pavilion 15 eg0505TU i5 1135G7/8GB/512GB/Win10 (46M02PA)',5,'18790000','/HinhAnh/SP019.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (17,17,27,03,'Laptop HP Pavilion 15 eg0505TU i5 1135G7/8GB/512GB/Win10 (46M02PB)',3,'18790000','/HinhAnh/SP020.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (13,05,09,04,'Laptop Lenovo Yoga 9 14ITL5 i7/1185G7/16GB/1TB SSD/Touch/Pen/Win10 (82BG006EVN)',5,'29990000','/HinhAnh/SP021.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (14,10,10,04,'Laptop Lenovo Yoga Duet 7 13IML05 i7 10510U/8GB/512GB/ Touch/Pen/Win10 (82AS007CVN)',4,'29990000','/HinhAnh/SP022.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (15,11,07,04,'Laptop Lenovo YOGA Slim 7 Carbon 13ITL5 i5 1135G7/16GB/512GB/Win10 (82EV0016VN)',4,'28990000','/HinhAnh/SP023.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (16,07,11,04,'Laptop Lenovo Yoga 7 14ITL5 i7 1165G7/8GB/512GB/ Touch/Pen/Win10 (82BH00CKVN)',4,'28990000','/HinhAnh/SP024.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (16,07,12,04,'Laptop Lenovo ThinkBook 14s Yoga ITL i7 1165G7/8GB/512GB/ Touch/Pen/Win10 (20WE004EVN)',5,'27890000','/HinhAnh/SP025.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (14,12,10,04,'Laptop Lenovo Yoga Duet 7 13IML05 i5 10210U/8GB/512GB/ Touch/Pen/Win10 (82AS007BVN)',5,'26990000','/HinhAnh/SP026.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (17,06,13,04,'Laptop Lenovo Ideapad Gaming 3 15I05 i7 10750H/8GB/512GB/4GB GTX1650Ti/120Hz/Win10 (81Y4013UVN)',3,'26990000','/HinhAnh/SP027.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (16,11,04,04,'Laptop Lenovo ThinkBook 14s Yoga ITL i5 1135G7/16GB/512GB/ Touch/Pen/Win10 (20WE004DVN)',3,'25890000','/HinhAnh/SP028.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (16,11,12,04,'Laptop Lenovo ThinkBook 14s Yoga ITL i5 1135G7/8GB/512GB/ Touch/Pen/Win10 (20WE004CVN)',3,'24790000','/HinhAnh/SP029.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (16,07,12,04,'Laptop Lenovo ThinkBook 14 G2 ITL i7 1165G7/8GB/512GB/Win10 (20VD003LVN)',4,'21990000','/HinhAnh/SP030.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (20,08,20,05,'Laptop Acer Nitro 5 Gaming AN515 57 54AF i5 11400H/16GB/512GB/4GB RTX3050/144Hz/Win11 (NH.QENSV.004)',4,'28990000','/HinhAnh/SP031.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (18,13,16,05,'Laptop Acer Aspire A315 56 308N i3 1005G1/4GB/256GB/Win10 (NX.HS5SV.00C)',4,'11790000','/HinhAnh/SP032.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (20,03,15,05,'Laptop Acer Predator Helios PH315 54 78W5 i7 11800H/8GB/512GB/4GB RTX3050Ti/144Hz/Balo/Win10 (NH.QC5SV.001)',4,'32990000','/HinhAnh/SP033.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (20,03,15,05,'Laptop Acer Nitro 5 Gaming AN515 57 727J i7 11800H/8GB/512GB/4GB RTX3050Ti/144Hz/Win10 (NH.QD9SV.005.)',4,'29990000','/HinhAnh/SP034.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (22,03,20,05,'Laptop Acer Predator Triton 300 PT315 53 71DJ i7 11800H/16GB/512GB/8GB RTX3070/165Hz/Win10 (NH.QDSSV.001)',4,'44990000','/HinhAnh/SP035.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (19,11,17,06,'Laptop Dell Vostro 3400 i5 1135G7/8GB/256GB//OfficeH&S 2019/Win10 (70253900)',4,'18890000','/HinhAnh/SP036.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (18,11,18,06,'Laptop Dell Vostro 3500 i5 1135G7/8GB/512GB/Office H&S2019/Win10 (7G3983)',5,'20990000','/HinhAnh/SP037.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (21,06,19,06,'Laptop Dell Gaming G3 15 i7 10750H/16GB/512GB/6GB GTX1660Ti/120Hz/Win10 (P89F002BWH)',3,'31990000','/HinhAnh/SP038.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (21,06,19,06,'Laptop Dell Gaming G3 i7 10750H/16GB/512GB/6GB GTX1660Ti/120Hz/Win10 (P89F002G3500B)',6,'31990000','/HinhAnh/SP039.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (21,06,19,06,'Laptop Dell Gaming G3 15 i7 10750H/16GB/512GB/4GB GTX1650Ti/120Hz/Win10 (P89F002DBL)',5,'29990000','/HinhAnh/SP040.jpg',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (01,01,01,07,'Laptop MSI Gaming GF65 Thin 10UE i5 10500H/16GB/512GB/6GB RTX3060 Max-Q/144Hz/Balo/Win10 (286VN)',5,'18990000','/HinhAnh/SP041.png',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (02,02,02,07,'Laptop MSI Modern 15 A11MU i5 1155G7/8GB/512GB/Túi/Chuột/Win10 (680VN)',5,'79990000','/HinhAnh/SP042.png',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');
insert into Product values (03,03,03,07,'Laptop MSI Gaming GE66 Raider 11UH i7 11800H/32GB/2TB SSD/16GB RTX3080/240Hz/Balo/Chuột/Win10 (259VN)',4,'64990000','/HinhAnh/SP043.png',0,'2022-01-01T00:00:00.0000000','2022-01-01T00:00:00.0000000');

