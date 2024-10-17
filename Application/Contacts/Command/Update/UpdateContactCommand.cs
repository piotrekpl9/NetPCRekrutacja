using Application.Common.Abstraction;

namespace Application.Contacts.Command.Update;

public sealed record UpdateContactCommand(
    Guid ContactId,
    string Name, 
    string Surname, 
    string Email, 
    Guid CategoryId, 
    DateTime BirthDate,
    string Password, 
    string PhoneNumber) : ICommand;