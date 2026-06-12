using BackendAPI.Domain.Entities;

namespace BackendAPI.Application.Abstractions
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        //public Task<DanhMucSanPham> PostDanhMucSanPham(DanhMucSanPham danhMucSanPham);
        public Task CreateCategory(Category danhMucSanPham);
        public Task<bool> UpdateCategory();
        public Task<bool> DeleteCategory(Category danhMucSanPham);
    }
}
