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
            /* Console.WriteLine("abc");*/
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
