using BackendAPI.Application.Abstractions;
using BackendAPI.Domain.Entities;
using BackendAPI.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;

namespace BackendAPI.Persistence.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly UserDbContext context;

    public ProductRepository(UserDbContext context)
    {
        this.context = context;
    }

    public Task<Product?> GetByIdAsync(int productId, bool includeRatings, CancellationToken cancellationToken)
    {
        IQueryable<Product> query = context.Product;
        if (includeRatings)
        {
            query = query.Include(product => product.Ratings);
        }

        return query.FirstOrDefaultAsync(product => product.ProductId == productId, cancellationToken);
    }

    public async Task<IReadOnlyDictionary<string, UserIdentityDTO>> GetCustomersByIdsAsync(
        IReadOnlyCollection<string> customerIds,
        CancellationToken cancellationToken)
    {
        if (customerIds.Count == 0)
        {
            return new Dictionary<string, UserIdentityDTO>();
        }

        return await context.UserIdentity
            .Where(customer => customerIds.Contains(customer.Id))
            .Select(customer => new UserIdentityDTO
            {
                Id = customer.Id,
                Email = customer.Email,
                UserName = customer.UserName
            })
            .ToDictionaryAsync(customer => customer.Id, cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> SearchAsync(
        ProductSearchCriteria criteria,
        CancellationToken cancellationToken)
    {
        return await ApplySearch(criteria)
            .Skip((criteria.Page - 1) * criteria.Limit)
            .Take(criteria.Limit)
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync(ProductSearchCriteria criteria, CancellationToken cancellationToken)
    {
        return ApplyFilters(criteria).CountAsync(cancellationToken);
    }

    public Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        return context.Product.AddAsync(product, cancellationToken).AsTask();
    }

    private IQueryable<Product> ApplySearch(ProductSearchCriteria criteria)
    {
        var query = ApplyFilters(criteria);

        return criteria.Sort switch
        {
            ProductSort.PriceDesc => query.OrderByDescending(product => product.Price),
            _ => query.OrderByDescending(product => product.PublishedDate)
        };
    }

    private IQueryable<Product> ApplyFilters(ProductSearchCriteria criteria)
    {
        var query = context.Product.AsQueryable();

        if (!criteria.IncludeDisabled)
        {
            query = query.Where(product => !product.IsDisable);
        }

        if (criteria.CategoryId.HasValue)
        {
            query = query.Where(product => product.CategoryId == criteria.CategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(criteria.Name))
        {
            var name = criteria.Name.Trim();
            query = query.Where(product => product.NameProduct != null && product.NameProduct.Contains(name));
        }

        return query;
    }
}
