using Domain.Entity;
using Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Common.Configuration;

public class AuthenticationDataConfiguration : IEntityTypeConfiguration<AuthenticationData>
{
    public void Configure(EntityTypeBuilder<AuthenticationData> builder)
    {
        builder.HasKey(data => data.ContactId);
        builder.HasOne<Contact>().WithOne().HasForeignKey<AuthenticationData>(authenticationData =>  authenticationData.ContactId);
    }
}