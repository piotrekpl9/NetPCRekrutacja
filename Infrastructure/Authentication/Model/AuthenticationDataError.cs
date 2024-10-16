using Domain.Common.Result;

namespace Infrastructure.Authentication.Model;

public static class AuthenticationDataError
{
    public static Error AuthenticationDataNotFound => new ("AuthenticationData_0", "AuthenticationDataNotFound");
    public static Error BadAuthenticationData => new ("AuthenticationData_1", "BadAuthenticationData");
}