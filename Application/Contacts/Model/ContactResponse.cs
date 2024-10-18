    namespace Application.Contacts.Model;

    public record ContactResponse(
        Guid Id,
        string Name,
        string Surname,
        string Email,
        string Category,
        string Subcategory,
        DateTime BirthDate,
        string PhoneNumber
    );
