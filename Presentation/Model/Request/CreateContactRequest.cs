namespace Presentation.Model.Request;

public sealed record CreateContactRequest(string Name, 
    string Surname, 
    string Email, 
    string CategoryName, 
    string? SubcategoryName, 
    DateTime BirthDate, 
    string Password, 
    string PhoneNumber);