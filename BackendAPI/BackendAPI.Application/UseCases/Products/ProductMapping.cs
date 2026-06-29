using BackendAPI.Domain.Entities;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Products;

internal static class ProductMapping
{
    public static ProductDTO ToProductDto(this Product product)
    {
        return new ProductDTO
        {
            ProductId = product.ProductId,
            ScreenId = product.ScreenId,
            ProcessorId = product.ProcessorId,
            RamId = product.RamId,
            CategoryId = product.CategoryId,
            NameProduct = product.NameProduct,
            Quantity = product.Quantity,
            IsDisable = product.IsDisable,
            Price = product.Price,
            Image = product.Image,
            Rating = product.Rating,
            PublishedDate = product.PublishedDate,
            UpdatedDate = product.UpdatedDate,
            Ratings = product.Ratings.Select(rating => new RatingDTO
            {
                RatingID = rating.RatingID,
                Rate = rating.Rate,
                Comments = rating.Comments,
                PublishedDate = rating.PublishedDate
            }).ToList()
        };
    }

    public static ProductAdminDTO ToProductAdminDto(this Product product)
    {
        return new ProductAdminDTO
        {
            ProductId = product.ProductId,
            ScreenId = product.ScreenId,
            ProcessorId = product.ProcessorId,
            RamId = product.RamId,
            CategoryId = product.CategoryId,
            NameProduct = product.NameProduct,
            Quantity = product.Quantity,
            IsDisable = product.IsDisable,
            Price = product.Price,
            Image = product.Image,
            Rating = product.Rating,
            PublishedDate = product.PublishedDate,
            UpdatedDate = product.UpdatedDate
        };
    }

    public static void ApplyAdminDto(this Product product, ProductAdminDTO dto)
    {
        product.ScreenId = dto.ScreenId;
        product.ProcessorId = dto.ProcessorId;
        product.CategoryId = dto.CategoryId;
        product.RamId = dto.RamId;
        product.NameProduct = dto.NameProduct?.Trim();
        product.Price = dto.Price;
        product.UpdatedDate = dto.UpdatedDate ?? DateTime.UtcNow;
        product.PublishedDate = dto.PublishedDate;
        product.Quantity = dto.Quantity;
        product.Image = dto.Image;
        product.IsDisable = dto.IsDisable;
    }
}
