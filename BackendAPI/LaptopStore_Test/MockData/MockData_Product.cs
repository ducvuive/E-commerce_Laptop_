using ShareView.DTO;

namespace LaptopStore_Test.MockData
{
    public static class MockData_Product
    {
        public static List<ProductDTO> GetCategoriesDTO()
        {
            return new List<ProductDTO>
            {
                new ProductDTO
                {
                    ProductId = 1,
                    ScreenId = 10,
                    ProcessorId = 14,
                    RamId = 22,
                    CategoryId = 1,
                    NameProduct = "Laptop Apple MacBook Air M1 2020 8GB/256GB/7-core GPU (MGN63SA/A)",
                    Quantity = 3,
                    Price = 27490000,
                    Image = "/ProductImages/SP001.jpg",
                    Rating = 0,
                    PublishedDate = new DateTime(2020, 1, 1),
                    Ratings = new List<RatingDTO>(),
                },
            };
        }
    }
}
