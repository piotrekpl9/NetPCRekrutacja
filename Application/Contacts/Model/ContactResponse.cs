    namespace Application.Contacts.Model;

    public record ContactResponse(
        Guid Id,
        string Name,
        string Surname,
        string Email,
        string CategoryName,
        string SubcategoryName,
        DateTime BirthDate,
        string PhoneNumber
    );
