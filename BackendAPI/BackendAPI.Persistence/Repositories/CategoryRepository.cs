using BackendAPI.Application.Abstractions;
using BackendAPI.Domain.Entities;
using BackendAPI.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Persistence.Repositories
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
            var categories = await _context.Category.Where(s => s.isDisabled == 1).ToListAsync();
            return categories;
        }
        public async Task<Category> GetCategory(int id)
        {
            Category categories = await _context.Category.FindAsync(id);

            if (categories == null)
            {
                return null;
            }
            return categories;
        }

        public async Task CreateCategory(Category inputCategory)
        {
            var category = new Category
            {
                CategoryId = inputCategory.CategoryId,
                Name = inputCategory.Name,
                Description = inputCategory.Description,
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
        public async Task<bool> DeleteCategory(Category category)
        {
            category.isDisabled = 0;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
