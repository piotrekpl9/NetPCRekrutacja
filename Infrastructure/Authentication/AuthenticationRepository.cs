using Infrastructure.Authentication.Abstraction;
using Infrastructure.Authentication.Model;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication;

public class AuthenticationRepository : RepositoryBase<AuthenticationData>, IAuthenticationRepository
{
    public AuthenticationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<AuthenticationData?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.AuthenticationData.FirstOrDefaultAsync(data => data.UserId == id, cancellationToken);
    }
}