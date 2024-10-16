using Infrastructure.Authentication.Model;

namespace Infrastructure.Authentication.Abstraction;

public interface IAuthenticationRepository
{
    public Task<AuthenticationData?> GetById(Guid id, CancellationToken cancellationToken = default);
    public Task Create(AuthenticationData authenticationData, CancellationToken cancellationToken = default);
    public void Update(AuthenticationData authenticationData);
    public void Delete(AuthenticationData authenticationData);
}