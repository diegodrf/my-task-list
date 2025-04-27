using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyTaskList.Application.Entities;

namespace MyTaskList.Persistence.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.Done)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasData([
            new Item
            {
                Id = new Guid("8643806F-5759-48CB-9CE6-7802CB346B33"),
                Name = "Sample",
                CreatedAt = new DateTime(2025, 4, 27, 2, 44,0, DateTimeKind.Utc),
                Done = false
            }
        ]);
    }
}