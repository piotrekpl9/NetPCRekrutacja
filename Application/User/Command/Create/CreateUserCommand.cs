using Application.Common.Abstraction;

namespace Application.User.Command.Create;

public sealed record CreateUserCommand(string Email, string Password) : ICommand;