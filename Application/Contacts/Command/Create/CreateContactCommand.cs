using Application.Common.Abstraction;

namespace Application.Contacts.Command.Create;

public sealed record CreateContactCommand(
    string Name, 
    string Surname, 
    string Email, 
    string CategoryName, 
    string? SubcategoryName, 
    DateTime BirthDate, 
    string Password, 
    string PhoneNumber) : ICommand;