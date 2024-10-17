using Application.User.Abstraction;
using Infrastructure.Common;
using Infrastructure.Common.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.User;

public class UserRepository : RepositoryBase<Domain.Entity.User>, IUserRepository
{
    public UserRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Domain.Entity.User?> GetById(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Users.FirstOrDefaultAsync(user => user.Id == userId,
            cancellationToken: cancellationToken);
    }

    public async Task<Domain.Entity.User?> GetByEmailAsNoTracking(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken: cancellationToken);
    }

    public async Task<bool> ExistByEmail(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Users.AnyAsync(user => user.Email == email, cancellationToken: cancellationToken);
    }
}