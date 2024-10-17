namespace Application.Contacts.Model.Error;
using Domain.Common.Result;

public class ContactError
{
    public static Error ContactNotFound => new ("Contact_0", "ContactNotFound");
    public static Error ContactWithGivenEmailAlreadyExists => new ("Contact_1", "ContactWithGivenEmailAlreadyExists");
}