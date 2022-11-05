using BackendAPI.Models;

namespace BackendAPI.Services
{
    public interface IDanhMucSanPhamRepository
    {
        Task<List<DanhMucSanPham>> GetDanhMucSanPham();
        Task<DanhMucSanPham> GetDanhMucSanPham(int id);
        //public Task<DanhMucSanPham> PostDanhMucSanPham(DanhMucSanPham danhMucSanPham);
        public Task<DanhMucSanPham> PostDanhMucSanPham(DanhMucSanPham danhMucSanPham);
        public Task<bool> UpdateDanhMucSanPham(int id, DanhMucSanPham danhMucSanPham);
        public Task<bool> DeleteDanhMucSanPham(DanhMucSanPham danhMucSanPham);
    }
}
