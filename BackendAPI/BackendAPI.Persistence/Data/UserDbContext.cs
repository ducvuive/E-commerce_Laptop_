using BackendAPI.Domain.Entities;
using BackendAPI.Persistence.Identity;
using BackendAPI.Persistence.Configurations;
using BackendAPI.Persistence.Messaging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Persistence.Data;

public class UserDbContext : IdentityDbContext<UserIdentity>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ram> Ram { get; set; }
    public virtual DbSet<Processor> Processor { get; set; }
    //public virtual DbSet<Connect> Connect { get; set; }
    public virtual DbSet<InvoiceDetail> InvoiceDetail { get; set; }
    public virtual DbSet<Category> Category { get; set; }
    public virtual DbSet<Invoice> Invoice { get; set; }
    public virtual DbSet<Screen> Screen { get; set; }
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<Rating> Rating { get; set; }
    public virtual DbSet<UserIdentity> UserIdentity { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    public virtual DbSet<OutboxMessage> OutboxMessages { get; set; }
    public virtual DbSet<ProcessedMessage> ProcessedMessages { get; set; }
    public virtual DbSet<EmailOutboxMessage> EmailOutboxMessages { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfiguration(new RamConfig());

        builder.ApplyConfiguration(new ProcessorConfig());

        //builder.ApplyConfiguration(new ConnectConfig());

        builder.ApplyConfiguration(new InvoiceDetailConfig());

        builder.ApplyConfiguration(new CategoriesConfig());

        builder.ApplyConfiguration(new InvoiceConfig());

        builder.ApplyConfiguration(new RatingConfig());

        builder.ApplyConfiguration(new ScreenConfig());

        builder.ApplyConfiguration(new ProductConfig());

        builder.ApplyConfiguration(new RefreshTokenConfig());

        builder.ApplyConfiguration(new OutboxMessageConfig());

        builder.ApplyConfiguration(new ProcessedMessageConfig());

        builder.ApplyConfiguration(new EmailOutboxMessageConfig());

        base.OnModelCreating(builder);

    }
}
