namespace Presentation.Model.Request;

public record UpdateContactRequest(
    string Name, 
    string Surname, 
    string Email, 
    string CategoryName, 
    string? SubcategoryName, 
    DateTime BirthDate, 
    string Password, 
    string PhoneNumber);