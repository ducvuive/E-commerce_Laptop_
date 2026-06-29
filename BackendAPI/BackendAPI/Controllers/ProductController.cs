using BackendAPI.Application.Abstractions;
using BackendAPI.Application.Common.Errors;
using BackendAPI.Application.Common.Results;
using BackendAPI.Application.UseCases.Products.CreateProduct;
using BackendAPI.Application.UseCases.Products.DisableProduct;
using BackendAPI.Application.UseCases.Products.GetProduct;
using BackendAPI.Application.UseCases.Products.GetProductAdmin;
using BackendAPI.Application.UseCases.Products.SearchProducts;
using BackendAPI.Application.UseCases.Products.UpdateProduct;
using BackendAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender sender;

        public ProductController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<ProductDTO>>> GetProduct(CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SearchProductsQuery(
                Page: 1,
                Limit: 100,
                IncludeDisabled: true), cancellationToken);

            return ToActionResult(result, paging => paging.Products);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("[action]")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductAdmin(CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SearchProductsQuery(
                Page: 1,
                Limit: 100,
                IncludeDisabled: true), cancellationToken);

            return ToActionResult(result, paging => paging.Products);
        }

        [HttpGet("entities")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductEntities(CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SearchProductsQuery(
                Page: 1,
                Limit: 100,
                IncludeDisabled: true), cancellationToken);

            return ToActionResult(result, paging => paging.Products);
        }

        [HttpGet]
        [Route("month")]
        public async Task<ActionResult<List<ProductDTO>>> GetNewestProducts(CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SearchProductsQuery(
                Page: 1,
                Limit: 6), cancellationToken);

            return ToActionResult(result, paging => paging.Products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetProductQuery(id), cancellationToken);
            return ToActionResult(result);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("admin_product/{id}")]
        public async Task<ActionResult<ProductAdminDTO>> GetProductAdmin(int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetProductAdminQuery(id), cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("GetProductsByPage/{page}")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductsByPage(int page = 1, CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new SearchProductsQuery(
                Page: page,
                Limit: 12,
                Sort: ProductSort.PriceDesc), cancellationToken);

            return ToActionResult(result, paging => paging.Products);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("[action]")]
        public async Task<ActionResult<ProductPagingDTO>> GetProductWithPage(
            [FromQuery] PaginationParameters paginationParameters,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SearchProductsQuery(
                Page: paginationParameters.Page,
                Limit: paginationParameters.Limit), cancellationToken);

            return ToActionResult(result);
        }

        [HttpGet("GetProductsByCategoryPage/{category}/{page}")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductsByCategoryPage(
            int category,
            int page,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SearchProductsQuery(
                Page: page,
                Limit: 12,
                CategoryId: category), cancellationToken);

            return ToActionResult(result, paging => paging.Products);
        }

        [HttpGet("GetProductsByCategory/{categoryId}/")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductsByCategory(
            int categoryId,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SearchProductsQuery(
                Page: 1,
                Limit: 100,
                CategoryId: categoryId), cancellationToken);

            return ToActionResult(result, paging => paging.Products);
        }

        [HttpGet("GetProductsByName/{name}/")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductsByName(
            string name,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SearchProductsQuery(
                Page: 1,
                Limit: 100,
                Name: name), cancellationToken);

            return ToActionResult(result, paging => paging.Products);
        }

        [HttpGet("GetProductsByNamePage/{name}/{page}")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductsByNamePage(
            string name,
            int page,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SearchProductsQuery(
                Page: page,
                Limit: 12,
                Name: name), cancellationToken);

            return ToActionResult(result, paging => paging.Products);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(
            int id,
            ProductAdminDTO productDTO,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateProductCommand(id, productDTO), cancellationToken);
            if (result.IsFailure)
            {
                return ToActionResult((Result)result);
            }

            return Ok("Update success");
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<ProductAdminDTO>> PostProduct(
            ProductAdminDTO productDTO,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateProductCommand(productDTO), cancellationToken);
            if (result.IsFailure)
            {
                return ToActionResult((Result)result);
            }

            return CreatedAtAction(nameof(GetProduct), new { id = result.Value!.ProductId }, result.Value);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new DisableProductCommand(id), cancellationToken);
            return result.IsSuccess ? NoContent() : ToActionResult(result);
        }

        private ActionResult<TValue> ToActionResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return ToActionResult((Result)result);
        }

        private ActionResult<TValue> ToActionResult<TSource, TValue>(
            Result<TSource> result,
            Func<TSource, TValue> map)
        {
            if (result.IsSuccess)
            {
                return Ok(map(result.Value!));
            }

            return ToActionResult((Result)result);
        }

        private ObjectResult ToActionResult(Result result)
        {
            var firstError = result.FirstError;
            var statusCode = firstError?.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status400BadRequest
            };

            return StatusCode(statusCode, result.Errors.Select(error => error.Message));
        }
    }
}
