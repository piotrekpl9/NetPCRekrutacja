using Infrastructure.Authentication.Entity;
using Infrastructure.Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Common.Configuration;

public class AuthenticationDataConfiguration : IEntityTypeConfiguration<AuthenticationData>
{
    public void Configure(EntityTypeBuilder<AuthenticationData> builder)
    {
        builder.HasKey(data => data.Id);
        builder.HasOne<Domain.Entity.User>().WithOne().HasForeignKey<AuthenticationData>(authenticationData =>  authenticationData.Id);
    }
}