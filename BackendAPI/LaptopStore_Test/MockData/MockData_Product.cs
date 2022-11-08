using ShareView.DTO;

namespace LaptopStore_Test.MockData
{
    public static class MockData_Product
    {
        public static List<SanPhamDTO> GetCategoriesDTO()
        {
            return new List<SanPhamDTO>
            {
                new SanPhamDTO
                {
                     SanPhamId =  1,
                     ManHinhId = 10,
                     BoXuLyId = 14,
                     RamId =  22,
                     CongKetNoiId= 26,
                     DMSPId= 1,
                     TenSP= "Laptop Apple MacBook Air M1 2020 8GB/256GB/7-core GPU (MGN63SA/A)",
                     SoLuong= 3,
                     MauSac= "Trắng",
                     OCung= "256 GB SSD",
                     CardManHinh= "Card tích hợp7 nhân GPU",
                     DacBiet= "",
                     HDH= "Mac OS",
                     ThietKe= "Vỏ kim loại nguyên khối",
                     KichThuocTrongLuong= "Dài 304.1 mm - Rộng 212.4 mm - Dày 4.1 mm đến 16.1 mm - Nặng 1.29 kg",
                     Webcam= "",
                     Pin= "Khoảng 10 tiếng",
                     RaMat= 2020,
                     MoTa= "",
                     DonGia= 27490000,
                     HinhAnh= "/HinhAnh/SP001.jpg",
                     DanhGia= 0,
                },
            };
        }
        /*        public static List<DanhMucSanPham> GetCategories()
                {
                    return new List<DanhMucSanPham>
                    {
                        new DanhMucSanPham
                        {
                            DMSPId = 1,
                            TenDM = "Danh muc 1",
                            Description = "Day la danh muc 1",
                        },
                        new DanhMucSanPham
                        {
                            DMSPId = 2,
                            TenDM = "Danh muc 2",
                            Description = "Day la danh muc 2",
                        },
                    };
                }

                public static List<DanhMucSanPhamDTO_Admin> CreateProductDTO()
                {
                    return new List<DanhMucSanPhamDTO_Admin>
                    {
                        new DanhMucSanPhamDTO_Admin
                        {
                            DMSPId = 1,
                            TenDM = "Danh muc 1",
                            Description = "Day la danh muc 1",
                        },
                        new DanhMucSanPhamDTO_Admin
                        {
                            DMSPId = 2,
                            TenDM = "Danh muc 2",
                            Description = "Day la danh muc 2",
                        },
                    };
                }
                public static List<DanhMucSanPhamDTO_Admin> ListProductDTO()
                {
                    return new List<DanhMucSanPhamDTO_Admin>
                    {
                        new DanhMucSanPhamDTO_Admin
                        {
                            DMSPId = 1,
                            TenDM = "Danh muc 1",
                            Description = "Day la danh muc 1",
                        },
                        new DanhMucSanPhamDTO_Admin
                        {
                            DMSPId = 2,
                            TenDM = "Danh muc 2",
                            Description = "Day la danh muc 2",
                        },
                    };
                }*/
    }
}