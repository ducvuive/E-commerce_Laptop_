using BackendAPI.Persistence.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Persistence.Configurations;

public sealed class ProcessedMessageConfig : IEntityTypeConfiguration<ProcessedMessage>
{
    public void Configure(EntityTypeBuilder<ProcessedMessage> builder)
    {
        builder.ToTable("ProcessedMessages");

        builder.HasKey(x => new { x.MessageId, x.ConsumerName });

        builder.Property(x => x.MessageId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ConsumerName)
            .HasMaxLength(200)
            .IsRequired();
    }
}
