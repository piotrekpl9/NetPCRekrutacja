using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Common.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<Domain.Entity.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.HasIndex(user => user.Email).IsUnique();
    }
}