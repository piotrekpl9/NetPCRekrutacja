using Application.Common.Abstraction;

namespace Application.Contacts.Command.Delete;

public sealed record DeleteContactCommand(Guid ContactId) : ICommand;