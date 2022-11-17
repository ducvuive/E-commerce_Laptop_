using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using BackendAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        /*        public DanhMucSanPhamsController(UserDbContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }*/
        public CategoriesController(ICategoryRepository danhMucSanPhamRepository, IMapper mapper)
        {
            _categoryRepository = danhMucSanPhamRepository;
            //_context = context;
            _mapper = mapper;
        }

        // GET: api/DanhMucSanPhams
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

        // GET: api/DanhMucSanPhams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            Category category = await _categoryRepository.GetCategory(id);
            var mapper = _mapper.Map<CategoryDTO>(category);
            if (mapper is null)
            {
                return BadRequest("Khong tim thay danh muc");
            }
            return Ok(mapper);
        }

        // POST: api/DanhMucSanPhams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryAdminDTO danhMucSanPham)
        {
            //DanhMucSanPham _danhMucSanPham = _mapper.Map<DanhMucSanPham>(danhMucSanPham);
            Category category = new Category()
            {
                TenDM = danhMucSanPham.TenDM,
                Description = danhMucSanPham.Description,
                isValid = 1,
            };
            //_context.DanhMucSanPham.Add(_danhMucSanPham);
            //await _context.SaveChangesAsync();
            await _categoryRepository.CreateCategory(category);
            //return _danhMucSanPham;
            //return dmsp;
            return Ok("Cap nhat thanh cong");
        }

        // PUT: api/DanhMucSanPhams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryAdminDTO danhMucSanPhamDTO)
        {
            var category = await _categoryRepository.GetCategory(id);
            category.TenDM = danhMucSanPhamDTO.TenDM;
            category.Description = danhMucSanPhamDTO.Description;
            category.DMSPId = danhMucSanPhamDTO.DMSPId;
            category.isValid = 1;
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                await _categoryRepository.UpdateCategory();
                return Ok("Cap nhat thanh cong");
            }
            return NoContent();
        }

        // DELETE: api/DanhMucSanPhams/5
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            //category.isValid = 0;
            //await _context.SaveChangesAsync();
            await _categoryRepository.DeleteCategory(category);
            return Ok("Xoa thanh cong");
        }
    }
}
