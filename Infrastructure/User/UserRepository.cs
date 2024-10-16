using Application.User.Abstraction;
using Infrastructure.Common;

namespace Infrastructure.User;

public class UserRepository : RepositoryBase<Domain.Entity.User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Domain.Entity.User?> GetById(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entity.User?> GetByEmailAsNoTracking(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistByEmail(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}