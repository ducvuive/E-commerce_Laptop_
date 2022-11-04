using AutoMapper;
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
        //private readonly UserDbContext _context;
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
        public async Task<ActionResult<List<DanhMucSanPhamDTO>>> GetDanhMucSanPham()
        {
            /*var dmsp = await _context.DanhMucSanPham.ToListAsync();
            return _mapper.Map<List<DanhMucSanPhamDTO>>(dmsp);*/
            List<DanhMucSanPham> dmsp = await _danhMucSanPhamRepository.GetDanhMucSanPham();
            return Ok(_mapper.Map<List<DanhMucSanPhamDTO>>(dmsp));
        }

        // GET: api/DanhMucSanPhams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucSanPhamDTO>> GetDanhMucSanPham(int id)
        {
            DanhMucSanPham dmsp = await _danhMucSanPhamRepository.GetDanhMucSanPham(id);
            var mapper = _mapper.Map<DanhMucSanPhamDTO>(dmsp);
            return mapper;
        }

        // POST: api/DanhMucSanPhams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DanhMucSanPhamDTO>> PostDanhMucSanPham(DanhMucSanPhamDTO danhMucSanPham)
        {
            DanhMucSanPham _danhMucSanPham = _mapper.Map<DanhMucSanPham>(danhMucSanPham);
            await _danhMucSanPhamRepository.PostDanhMucSanPham(_danhMucSanPham);
            return Ok("Create success");
        }

        // PUT: api/DanhMucSanPhams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDanhMucSanPham(int id, DanhMucSanPhamDTO danhMucSanPhamDTO)
        {
            var dmsp = await _danhMucSanPhamRepository.GetDanhMucSanPham(id);
            dmsp.TenDM = danhMucSanPhamDTO.TenDM;
            dmsp.Description = danhMucSanPhamDTO.Description;
            dmsp.DMSPId = danhMucSanPhamDTO.DMSPId;

            /*            if (id != danhMucSanPhamDTO.DMSPId)
                        {
                            await _danhMucSanPhamRepository.UpdateDanhMucSanPham(id, dmsp);
                        }*/

            //var boNhoRam = await _context.BoXuLy.FindAsync(id);

            //DanhMucSanPham dmsp = _mapper.Map<DanhMucSanPham>(danhMucSanPhamDTO);
            //_context.Entry(dmsp).State = EntityState.Modified;
            if (dmsp == null)
            {
                return NotFound();
            }
            else
            {
                await _danhMucSanPhamRepository.UpdateDanhMucSanPham(id, dmsp);
                return Ok("Cap nhat thanh cong");
            }
            /*
                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!DanhMucSanPhamExists(id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }*/

            return NoContent();
        }

        // DELETE: api/DanhMucSanPhams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanhMucSanPham(int id)
        {
            var dmsp = await _danhMucSanPhamRepository.GetDanhMucSanPham(id);
            if (dmsp == null)
            {
                return NotFound();
            }

            //_context.DanhMucSanPham.Remove(danhMucSanPham);
            await _danhMucSanPhamRepository.DeleteDanhMucSanPham(dmsp);
            return Ok("Cap nhat thanh cong");
            //return NoContent();
        }

        /*        private bool DanhMucSanPhamExists(int id)
                {
                    return _context.DanhMucSanPham.Any(e => e.DMSPId == id);
                }*/
    }
}
