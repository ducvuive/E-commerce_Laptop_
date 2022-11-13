

using BackendAPI.Models;
using ShareView.DTO;

namespace LaptopStore_Test.MockData
{
    public static class MockData_Categories
    {
        public static List<DanhMucSanPhamDTO> GetCategoriesDTO()
        {
            return new List<DanhMucSanPhamDTO>
            {
                new DanhMucSanPhamDTO
                {
                    DMSPId = 1,
                    TenDM = "Danh muc 1",
                    Description = "Day la danh muc 1",
                },
                new DanhMucSanPhamDTO
                {
                    DMSPId = 2,
                    TenDM = "Danh muc 2",
                    Description = "Day la danh muc 2",
                }
            };
        }
        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    DMSPId = 1,
                    TenDM = "Danh muc 1",
                    Description = "Day la danh muc 1",
                },
                new Category
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
        }
    }
}