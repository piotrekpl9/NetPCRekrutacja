using Application.Authentication.Abstraction;
using Application.Authentication.Model;
using Application.User.Abstraction;
using Application.User.Model.Error;
using Domain.Common.Result;
using Infrastructure.Authentication.Abstraction;
using Infrastructure.Authentication.Model;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<AuthenticationData> _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    public AuthenticationService(IAuthenticationRepository authenticationRepository, IPasswordHasher<AuthenticationData> passwordHasher, IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _authenticationRepository = authenticationRepository;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result> Register(Guid userId, string password, CancellationToken cancellationToken = default)
    {
        
        await _authenticationRepository.Create(new AuthenticationData(userId, _passwordHasher.HashPassword(default,password)),cancellationToken);
        return Result.Success();
    }

    public async Task<Result<LoginResultDto>> Login(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsNoTracking(email, cancellationToken);
        if (user is null)
        {
            return Result<LoginResultDto>.Failure(UserError.UserNotFound);
        }
        var userAuthData = await _authenticationRepository.GetById(user.Id,cancellationToken);
        if (userAuthData is null)
        {
            return Result<LoginResultDto>.Failure(AuthenticationDataError.AuthenticationDataNotFound);
        }
        
        var passwordCheck = (_passwordHasher.VerifyHashedPassword(
                    default, 
                    userAuthData.Password, 
                    password) == PasswordVerificationResult.Success
            );
        
        if (user.Email != email || !passwordCheck)
        {
            return Result<LoginResultDto>.Failure(AuthenticationDataError.BadAuthenticationData);
        }
        var token = _jwtTokenGenerator.GenerateToken(user.Id, email);

        return Result<LoginResultDto>.Success(new LoginResultDto(user.Id, token));
    }
}