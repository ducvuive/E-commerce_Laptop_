using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Services
{
    public class DanhMucSanPhamRepository : IDanhMucSanPhamRepository
    {
        private readonly UserDbContext _context;
        //private readonly IMapper _mapper;
        public DanhMucSanPhamRepository(UserDbContext context)
        {
            _context = context;
            //_mapper = mapper;
        }
        /*        public void DeleteDanhMucSanPham(int id)
                {
                    throw new NotImplementedException();
                }*/
        /*        List<DanhMucSanPhamDTO> IDanhMucSanPhamRepository.GetDanhMucSanPham()
                {
                    throw new NotImplementedException();
                }*/
        public async Task<List<DanhMucSanPham>> GetDanhMucSanPham()
        {
            var dmsp = await _context.DanhMucSanPham.ToListAsync();
            //return _mapper.Map<List<DanhMucSanPhamDTO>>(dmsp);
            return dmsp;
        }


        public async Task<DanhMucSanPham> GetDanhMucSanPham(int id)
        {
            DanhMucSanPham dmsp = await _context.DanhMucSanPham.FindAsync(id);

            if (dmsp == null)
            {
                return null;
            }
            return dmsp;
        }

        public async Task<DanhMucSanPham> PostDanhMucSanPham(DanhMucSanPham danhMucSanPham)
        {
            _context.DanhMucSanPham.Add(danhMucSanPham);
            await _context.SaveChangesAsync();
            return danhMucSanPham;
        }

        public async Task<bool> UpdateDanhMucSanPham(int id, DanhMucSanPham danhMucSanPham)
        {
            /*            var dmsp = await _context.DanhMucSanPham.FindAsync(id);
                        dmsp.TenDM = danhMucSanPhamDTO.TenDM;
                        dmsp.Description = danhMucSanPhamDTO.Description;
                        dmsp.DMSPId = danhMucSanPhamDTO.DMSPId;*/

            await _context.SaveChangesAsync();
            return true;

            /*            if (id != danhMucSanPham.DMSPId)
                        {
                            return BadRequest();
                        }*/

            //var boNhoRam = await _context.BoXuLy.FindAsync(id);

            //DanhMucSanPham dmsp = _mapper.Map<DanhMucSanPham>(danhMucSanPhamDTO);
            /*            _context.Entry(dmsp).State = EntityState.Modified;
                        if (dmsp == null)
                        {
                            return NotFound();
                        }

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
                        }

                        return NoContent();*/
        }
        public async Task<bool> DeleteDanhMucSanPham(DanhMucSanPham danhMucSanPham)
        {
            /*            var dmsp = await _context.DanhMucSanPham.FindAsync(id);
                        dmsp.TenDM = danhMucSanPhamDTO.TenDM;
                        dmsp.Description = danhMucSanPhamDTO.Description;
                        dmsp.DMSPId = danhMucSanPhamDTO.DMSPId;*/

            _context.DanhMucSanPham.Remove(danhMucSanPham);
            await _context.SaveChangesAsync();
            return true;

        }

    }
}
