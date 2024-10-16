namespace Application.User.Abstraction;
using Domain.Entity;

public interface IUserRepository
{
    public Task Create(User user, CancellationToken cancellationToken = default);
    public Task<User?> GetById(Guid userId, CancellationToken cancellationToken = default);
    public Task<User?> GetByEmailAsNoTracking(string email, CancellationToken cancellationToken = default);
    public Task<bool> ExistByEmail(string email, CancellationToken cancellationToken = default);
}