using BackendAPI.Areas.Identity.Data;

namespace BackendAPI.Services
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly UserDbContext _context;
        public SanPhamRepository(UserDbContext context)
        {
            _context = context;
        }
        /*        public async Task<List<SanPham>> GetProducts()
                {
                    var dmsp = await _context.DanhMucSanPham.ToListAsync();
                    return dmsp;
                }*/

    }
}
