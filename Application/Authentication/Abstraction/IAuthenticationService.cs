using Application.Authentication.Model;
using Domain.Common.Result;

namespace Application.Authentication.Abstraction;

public interface IAuthenticationService
{
    public Task<Result> Register(Guid userId, string password, CancellationToken cancellationToken = default);
    public Task<Result<LoginResultDto>> Login(string email, string password, CancellationToken cancellationToken = default);
    public string HashPassword(string password);
    public bool VerifyPasswordsMatch(string password, string otherPassword);
}