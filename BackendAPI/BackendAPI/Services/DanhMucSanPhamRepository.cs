using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Services
{
    public class DanhMucSanPhamRepository : IDanhMucSanPhamRepository
    {
        private readonly UserDbContext _context;
        public DanhMucSanPhamRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategories()
        {
            var dmsp = await _context.DanhMucSanPham.Where(s => s.isValid == 1).ToListAsync();
            return dmsp;
        }
        public async Task<Category> GetCategory(int id)
        {
            Category dmsp = await _context.DanhMucSanPham.FindAsync(id);

            if (dmsp == null)
            {
                return null;
            }
            return dmsp;
        }

        public async Task CreateCategory(Category danhMucSanPham)
        {
            _context.DanhMucSanPham.Add(danhMucSanPham);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateCategory()
        {
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCategory(Category danhMucSanPham)
        {
            danhMucSanPham.isValid = 0;
            //_context.DanhMucSanPham.Remove(danhMucSanPham);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
