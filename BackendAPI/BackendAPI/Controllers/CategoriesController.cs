using AutoMapper;
using BackendAPI.Domain.Entities;
using BackendAPI.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // GET: api/Categories
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<CategoryAdminDTO>>> GetCategories()
        {
            List<Category> categories = await _categoryRepository.GetCategories();
            return Ok(_mapper.Map<List<CategoryAdminDTO>>(categories));
        }

        [HttpGet]
        [Route("GetCate")]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories_User()
        {
            List<Category> categories = await _categoryRepository.GetCategories();
            return Ok(_mapper.Map<List<CategoryDTO>>(categories));
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            Category category = await _categoryRepository.GetCategory(id);
            var mapper = _mapper.Map<CategoryDTO>(category);
            if (mapper is null)
            {
                return BadRequest("Category not found");
            }
            return Ok(mapper);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryAdminDTO categoryDTO)
        {
            //Category _category = _mapper.Map<Category>(category);
            Category category = new Category()
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description,
                isDisabled = 1,
            };
            //_context.Category.Add(_category);
            //await _context.SaveChangesAsync();
            await _categoryRepository.CreateCategory(category);
            //return _category;
            //return categories;
            return Ok("Category created successfully");
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryAdminDTO categoryDTO)
        {
            var category = await _categoryRepository.GetCategory(id);
            category.Name = categoryDTO.Name;
            category.Description = categoryDTO.Description;
            category.CategoryId = categoryDTO.CategoryId;
            category.isDisabled = 1;
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                await _categoryRepository.UpdateCategory();
                return Ok("Category updated successfully");
            }
            return NoContent();
        }

        // DELETE: api/Categories/5
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryRepository.DeleteCategory(category);
            return Ok("Category deleted successfully");
        }
    }
}
