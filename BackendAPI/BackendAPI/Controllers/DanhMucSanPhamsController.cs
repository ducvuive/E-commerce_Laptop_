using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucSanPhamsController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDanhMucSanPhamRepository _danhMucSanPhamRepository;
        /*        public DanhMucSanPhamsController(UserDbContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }*/
        public DanhMucSanPhamsController(IDanhMucSanPhamRepository danhMucSanPhamRepository, IMapper mapper)
        {
            _danhMucSanPhamRepository = danhMucSanPhamRepository;
            //_context = context;
            _mapper = mapper;
        }

        // GET: api/DanhMucSanPhams
        //[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<DanhMucSanPhamDTO>>> GetCategories()
        {
            List<DanhMucSanPham> categories = await _danhMucSanPhamRepository.GetCategories();
            return Ok(_mapper.Map<List<DanhMucSanPhamDTO>>(categories));
        }

        // GET: api/DanhMucSanPhams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucSanPhamDTO>> GetCategory(int id)
        {
            DanhMucSanPham category = await _danhMucSanPhamRepository.GetCategory(id);
            var mapper = _mapper.Map<DanhMucSanPhamDTO>(category);
            if (mapper is null)
            {
                return BadRequest("Khong tim thay danh muc");
            }
            return Ok(mapper);
        }

        // POST: api/DanhMucSanPhams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> CreateCategory(DanhMucSanPhamDTO_Admin danhMucSanPham)
        {
            //DanhMucSanPham _danhMucSanPham = _mapper.Map<DanhMucSanPham>(danhMucSanPham);
            DanhMucSanPham category = new DanhMucSanPham()
            {
                TenDM = danhMucSanPham.TenDM,
                Description = danhMucSanPham.Description,
            };
            //_context.DanhMucSanPham.Add(_danhMucSanPham);
            //await _context.SaveChangesAsync();
            await _danhMucSanPhamRepository.CreateCategory(category);
            //return _danhMucSanPham;
            //return dmsp;
            return Ok("Cap nhat thanh cong");
        }

        // PUT: api/DanhMucSanPhams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, DanhMucSanPhamDTO_Admin danhMucSanPhamDTO)
        {
            var category = await _danhMucSanPhamRepository.GetCategory(id);
            category.TenDM = danhMucSanPhamDTO.TenDM;
            category.Description = danhMucSanPhamDTO.Description;
            category.DMSPId = danhMucSanPhamDTO.DMSPId;
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                await _danhMucSanPhamRepository.UpdateCategory();
                return Ok("Cap nhat thanh cong");
            }
            return NoContent();
        }

        // DELETE: api/DanhMucSanPhams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _danhMucSanPhamRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            await _danhMucSanPhamRepository.DeleteCategory(category);
            return Ok("Xoa thanh cong");
        }
    }
}
