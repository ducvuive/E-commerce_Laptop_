using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;
using System.Data;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        public ProductController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/SanPhams
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<ProductDTO>>> GetProduct()
        {
            var product = await _context.Product.ToListAsync();
            return Ok(_mapper.Map<List<ProductDTO>>(product));
        }
        // GET: api/SanPhams
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("[action]")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductAdmin()
        {
            var product = await _context.Product.ToListAsync();
            return Ok(_mapper.Map<List<ProductDTO>>(product));
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetSanPhamAdmin()
        {
            var product = await _context.Product.ToListAsync();
            return product;
        }

        [HttpGet]
        [Route("month")]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<ActionResult<List<ProductDTO>>> GetSanPhamTopRaMat()
        {
            var results = _context.Product.OrderByDescending(x => x.PublishedDate).Take(6);
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<ProductDTO>>(results);
            return Ok(mapper);
        }

        // GET: api/SanPhams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetSanPham(int id)
        {
            var product = await _context.Product.Where(p => p.ProductId == id)
                                                .Include(p => p.Ratings)
                                                .ThenInclude(r => r.Customer)
                                                .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<ProductDTO>(product);
            return mapper;
        }
        // GET: api/SanPhams/5
        //[Route("api/SanPhams/admin_product")]
        [HttpGet("admin_product/{id}")]
        public async Task<ActionResult<ProductAdminDTO>> GetSanPhamAdmin(int id)
        {
            /*            var sanPham = await _context.SanPham
                                                            .Where(p => p.SanPhamId == id)
                                                            .Include(p => p.Rating)
                                                            .ThenInclude(r => r.KhachHang)
                                                            .FirstOrDefaultAsync();*/
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            /*            var mapper = new SanPhamDTO_Admin()
                        {
                            SanPhamId = sanPham.SanPhamId,
                            TenDM = sanPham.DMSPId.tenDM,
                        }*/
            var mapper = _mapper.Map<ProductAdminDTO>(product);
            //var mapper = new 
            return Ok(product);
        }
        // GET: api/SanPhams/5
        [HttpGet("GetSanPhamTheoTrang/{page}")]
        public async Task<ActionResult> GetSanPhamTheoTrang(int page = 1)
        {
            var skip = 12 * (page - 1);
            var results = _context.Product.OrderByDescending(x => x.Price).Skip(skip).Take(12);
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<ProductDTO>>(results);
            return Ok(mapper);
        }




        // GET: api/SanPhams/5
        [HttpGet("[action]")]
        public ProductPagingDTO GetProductWithPage([FromQuery] PaginationParameters paginationParameters)
        {
            var query = _context.Product.Where(d => d.IsDisable == false);
            var total = query.Count();
            var results = query.Skip((paginationParameters.Page - 1) * paginationParameters.Limit).Take(paginationParameters.Limit);
            var productDTO = _mapper.Map<List<ProductDTO>>(results.ToList());

            return new ProductPagingDTO
            {
                Products = productDTO,
                TotalItem = total,
                Page = paginationParameters.Page,
                LastPage = (int)Math.Ceiling(Decimal.Divide(total, paginationParameters.Limit))
            };
        }





        [HttpGet("GetSanPhamTheoDmTheoTrang/{dm}/{page}")]
        public async Task<ActionResult> GetSanPhamTheoDmTheoTrang(int dm, int page)
        {
            var skip = 12 * (page - 1);
            var results = _context.Product.Where(s => s.CategoryId == dm).Skip(skip).Take(12);
            ;
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<ProductDTO>>(results);
            return Ok(mapper);
        }

        [HttpGet("GetSanPhamTheoDm/{dm}/")]
        public async Task<ActionResult<ProductDTO>> GetSanPhamTheoDm(int dm)
        {
            var results = _context.Product.Where(s => s.CategoryId == dm);
            ;
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<ProductDTO>>(results);
            return Ok(mapper);
        }

        [HttpGet("GetSanPhamTheoTen/{ten}/")]
        public async Task<ActionResult> GetSanPhamTheoTen(string ten)
        {
            var results = _context.Product.Where(s => s.NameProduct.Contains(ten));
            ;
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<ProductDTO>>(results);
            return Ok(mapper);
        }

        [HttpGet("GetSanPhamTheoTenTheoTrang/{ten}/{page}")]
        public async Task<ActionResult> GetSanPhamTheoTenTheoTrang(string ten, int page)
        {
            var skip = 12 * (page - 1);
            var results = _context.Product.Where(s => s.NameProduct.Contains(ten)).Skip(skip).Take(12);
            ;
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<ProductDTO>>(results);
            return Ok(mapper);
        }

        // PUT: api/SanPhams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanPham(int id, ProductAdminDTO sanPhamDTO)
        {
            var product = await _context.Product.Where(p => p.ProductId == id).FirstOrDefaultAsync();
            if (product != null)
            {
                product.ProductId = sanPhamDTO.ProductId;
                product.ProcessorId = sanPhamDTO.ProcessorId;
                product.CategoryId = sanPhamDTO.CategoryId;
                product.RamId = sanPhamDTO.RamId;
                product.ScreenId = sanPhamDTO.ScreenId;
                product.NameProduct = sanPhamDTO.NameProduct;
                product.Price = sanPhamDTO.Price;
                product.UpdatedDate = sanPhamDTO.UpdatedDate;
                product.Quantity = sanPhamDTO.Quantity;

                //SanPham sanPham = _mapper.Map<SanPham>(sanPhamDTO);
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Update success");
            }
            return NoContent();
        }

        // POST: api/SanPhams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostSanPham(ProductAdminDTO sanPhamDTO)
        {
            var product = new Product
            {
                //ProductId = sanPhamDTO.SanPhamId,
                ProcessorId = sanPhamDTO.ProcessorId,
                //CongKetNoiId = sanPhamDTO.CongKetNoiId,
                CategoryId = sanPhamDTO.CategoryId,
                RamId = sanPhamDTO.RamId,
                ScreenId = sanPhamDTO.ScreenId,
                NameProduct = sanPhamDTO.NameProduct,
                Price = sanPhamDTO.Price,
                UpdatedDate = sanPhamDTO.UpdatedDate,
                PublishedDate = sanPhamDTO.PublishedDate,
                Quantity = sanPhamDTO.Quantity,
                Rating = 0,
            };
            //SanPham sanPham = _mapper.Map<SanPham>(sanPhamDTO);
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSanPham", new { id = product.ProductId }, product);
        }

        // DELETE: api/SanPhams/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.IsDisable = true;
            await _context.SaveChangesAsync();
            /*            _context.Product.Remove(product);
                        await _context.SaveChangesAsync();*/

            return NoContent();
        }

        private bool SanPhamExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
