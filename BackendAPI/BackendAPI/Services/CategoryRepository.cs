using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly UserDbContext _context;
        public CategoryRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategories()
        {
            var dmsp = await _context.Category.Where(s => s.isDisabled == 1).ToListAsync();
            return dmsp;
        }
        public async Task<Category> GetCategory(int id)
        {
            Category dmsp = await _context.Category.FindAsync(id);

            if (dmsp == null)
            {
                return null;
            }
            return dmsp;
        }

        public async Task CreateCategory(Category danhMucSanPham)
        {
            var category = new Category
            {
                CategoryId = danhMucSanPham.CategoryId,
                Name = danhMucSanPham.Name,
                Description = danhMucSanPham.Description,
                isDisabled = 1
            };
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateCategory()
        {
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCategory(Category danhMucSanPham)
        {
            danhMucSanPham.isDisabled = 0;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
