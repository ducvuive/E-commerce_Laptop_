using AutoMapper;
using BackendAPI.Persistence.Data;
using BackendAPI.Domain.Entities;
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

        // GET: api/Products
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<ProductDTO>>> GetProduct()
        {
            var product = await _context.Product.ToListAsync();
            return Ok(_mapper.Map<List<ProductDTO>>(product));
        }
        // GET: api/Products
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("[action]")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductAdmin()
        {
            var product = await _context.Product.ToListAsync();
            return Ok(_mapper.Map<List<ProductDTO>>(product));
        }

        [HttpGet("entities")]
        public async Task<ActionResult<List<Product>>> GetProductEntities()
        {
            var product = await _context.Product.ToListAsync();
            return product;
        }

        [HttpGet]
        [Route("month")]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<ActionResult<List<ProductDTO>>> GetNewestProducts()
        {
            var results = _context.Product.OrderByDescending(x => x.PublishedDate).Take(6);
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<ProductDTO>>(results);
            return Ok(mapper);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _context.Product.Where(p => p.ProductId == id)
                                                .Include(p => p.Ratings)
                                                .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<ProductDTO>(product);
            if (mapper.Ratings is not null && product.Ratings.Any())
            {
                var customerIds = product.Ratings
                    .Select(r => r.CustomerId)
                    .Where(id => id is not null)
                    .Distinct()
                    .ToList();

                var customers = await _context.UserIdentity
                    .Where(u => customerIds.Contains(u.Id))
                    .ToDictionaryAsync(u => u.Id);

                for (var index = 0; index < product.Ratings.Count && index < mapper.Ratings.Count; index++)
                {
                    var customerId = product.Ratings[index].CustomerId;
                    if (customerId is not null && customers.TryGetValue(customerId, out var customer))
                    {
                        mapper.Ratings[index].Customer = _mapper.Map<UserIdentityDTO>(customer);
                    }
                }
            }

            return mapper;
        }
        // GET: api/Products/5
        //[Route("api/Products/admin_product")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("admin_product/{id}")]
        public async Task<ActionResult<ProductAdminDTO>> GetProductAdmin(int id)
        {
            /*            var product = await _context.Product
                                                            .Where(p => p.ProductId == id)
                                                            .Include(p => p.Rating)
                                                            .ThenInclude(r => r.Customer)
                                                            .FirstOrDefaultAsync();*/
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            /*            var mapper = new ProductDTO_Admin()
                        {
                            ProductId = product.ProductId,
                            CategoryName = product.CategoryId.categoryName,
                        }*/
            var mapper = _mapper.Map<ProductAdminDTO>(product);
            //var mapper = new 
            return Ok(product);
        }
        // GET: api/Products/5
        [HttpGet("GetProductsByPage/{page}")]
        public async Task<ActionResult> GetProductsByPage(int page = 1)
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




        // GET: api/Products/5
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
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

        [HttpGet("GetProductsByCategoryPage/{category}/{page}")]
        public async Task<ActionResult> GetProductsByCategoryPage(int category, int page)
        {
            var skip = 12 * (page - 1);
            var results = _context.Product.Where(s => s.CategoryId == category).Skip(skip).Take(12);
            ;
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<ProductDTO>>(results);
            return Ok(mapper);
        }

        [HttpGet("GetProductsByCategory/{categoryId}/")]
        public async Task<ActionResult<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            var results = _context.Product.Where(s => s.CategoryId == categoryId);
            ;
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<ProductDTO>>(results);
            return Ok(mapper);
        }

        [HttpGet("GetProductsByName/{name}/")]
        public async Task<ActionResult> GetProductsByName(string name)
        {
            var results = _context.Product.Where(s => s.NameProduct.Contains(name));
            ;
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<ProductDTO>>(results);
            return Ok(mapper);
        }

        [HttpGet("GetProductsByNamePage/{name}/{page}")]
        public async Task<ActionResult> GetProductsByNamePage(string name, int page)
        {
            var skip = 12 * (page - 1);
            var results = _context.Product.Where(s => s.NameProduct.Contains(name)).Skip(skip).Take(12);
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<ProductDTO>>(results);
            return Ok(mapper);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductAdminDTO productDTO)
        {
            var product = await _context.Product.Where(p => p.ProductId == id).FirstOrDefaultAsync();
            if (product != null)
            {
                product.ProductId = productDTO.ProductId;
                product.ProcessorId = productDTO.ProcessorId;
                product.CategoryId = productDTO.CategoryId;
                product.RamId = productDTO.RamId;
                product.ScreenId = productDTO.ScreenId;
                product.NameProduct = productDTO.NameProduct;
                product.Price = productDTO.Price;
                product.UpdatedDate = productDTO.UpdatedDate;
                product.Quantity = productDTO.Quantity;

                //Product product = _mapper.Map<Product>(productDTO);
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Update success");
            }
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductAdminDTO productDTO)
        {
            var product = new Product
            {
                //ProductId = productDTO.ProductId,
                ProcessorId = productDTO.ProcessorId,
                //CongKetNoiId = productDTO.CongKetNoiId,
                CategoryId = productDTO.CategoryId,
                RamId = productDTO.RamId,
                ScreenId = productDTO.ScreenId,
                NameProduct = productDTO.NameProduct,
                Price = productDTO.Price,
                UpdatedDate = productDTO.UpdatedDate,
                PublishedDate = productDTO.PublishedDate,
                Quantity = productDTO.Quantity,
                Rating = 0,
            };
            //Product product = _mapper.Map<Product>(productDTO);
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
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

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
