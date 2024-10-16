namespace Application.User.Model.Error;
using Domain.Common.Result;

public static class UserError
{
    public static Error UserNotFound => new ("User_0", "UserNotFound");
    public static Error UserWithGivenEmailAlreadyExists => new ("User_1", "UserWithGivenEmailAlreadyExists");
}