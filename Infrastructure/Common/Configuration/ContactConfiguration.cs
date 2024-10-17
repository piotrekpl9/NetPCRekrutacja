using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Common.Configuration;
using Domain.Entity;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(contact => contact.Id);
        builder.HasIndex(contact => contact.Email).IsUnique();
        builder.HasOne<Category>(contact => contact.Category).WithMany();
        builder.HasOne<Subcategory>(contact => contact.Subcategory).WithMany();
    }
}