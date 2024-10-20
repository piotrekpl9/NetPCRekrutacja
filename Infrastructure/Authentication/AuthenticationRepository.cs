using Infrastructure.Authentication.Abstraction;
using Infrastructure.Authentication.Entity;
using Infrastructure.Authentication.Model;
using Infrastructure.Common;
using Infrastructure.Common.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication;

public class AuthenticationRepository : RepositoryBase<AuthenticationData>, IAuthenticationRepository
{
    public AuthenticationRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<AuthenticationData?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.AuthenticationData.FirstOrDefaultAsync(data => data.Id == id, cancellationToken);
    }
}