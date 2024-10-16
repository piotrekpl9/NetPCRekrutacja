using Domain.Entity;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Common.Configuration;

public class AuthenticationDataConfiguration : IEntityTypeConfiguration<AuthenticationData>
{
    public void Configure(EntityTypeBuilder<AuthenticationData> builder)
    {
        builder.HasKey(data => data.UserId);
        builder.HasOne<Domain.Entity.User>().WithOne().HasForeignKey<AuthenticationData>(authenticationData =>  authenticationData.UserId);
    }
}