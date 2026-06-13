using BackendAPI.Domain.Entities;

namespace BackendAPI.Application.Abstractions
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        //public Task<Category> PostCategory(Category category);
        public Task CreateCategory(Category category);
        public Task<bool> UpdateCategory();
        public Task<bool> DeleteCategory(Category category);
    }
}
