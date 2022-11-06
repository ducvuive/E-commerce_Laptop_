using BackendAPI.Models;

namespace BackendAPI.Services
{
    public interface IDanhMucSanPhamRepository
    {
        Task<List<DanhMucSanPham>> GetCategories();
        Task<DanhMucSanPham> GetCategory(int id);
        //public Task<DanhMucSanPham> PostDanhMucSanPham(DanhMucSanPham danhMucSanPham);
        public Task CreateCategory(DanhMucSanPham danhMucSanPham);
        public Task<bool> UpdateCategory();
        public Task<bool> DeleteCategory(DanhMucSanPham danhMucSanPham);
    }
}
