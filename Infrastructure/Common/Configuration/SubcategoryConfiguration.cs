using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Common.Configuration;

public class SubcategoryConfiguration : IEntityTypeConfiguration<Domain.Entity.Subcategory>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Subcategory> builder)
    {
        builder.HasKey(subcategory => subcategory.Id);
        builder.HasIndex(subcategory => subcategory.Name).IsUnique();
    }
}