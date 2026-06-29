using BackendAPI.Domain.Entities;
using ShareView.DTO;

namespace BackendAPI.Application.Abstractions;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int productId, bool includeRatings, CancellationToken cancellationToken);

    Task<IReadOnlyDictionary<string, UserIdentityDTO>> GetCustomersByIdsAsync(
        IReadOnlyCollection<string> customerIds,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Product>> SearchAsync(
        ProductSearchCriteria criteria,
        CancellationToken cancellationToken);

    Task<int> CountAsync(ProductSearchCriteria criteria, CancellationToken cancellationToken);

    Task AddAsync(Product product, CancellationToken cancellationToken);
}

public sealed record ProductSearchCriteria(
    int Page,
    int Limit,
    int? CategoryId = null,
    string? Name = null,
    bool IncludeDisabled = false,
    ProductSort Sort = ProductSort.PublishedDateDesc);

public enum ProductSort
{
    PublishedDateDesc,
    PriceDesc
}
