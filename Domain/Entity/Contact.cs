namespace Domain.Entity;
using Primitives;

public class Contact : Entity
{
    public Contact(Guid id) : base(id)
    {
    }
    
    public Contact(Guid id, string name, string surname, string email, Category category, DateTime birthDate, string phoneNumber, string password) : base(id)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Category = category;
        PhoneNumber = phoneNumber;
        Password = password;
        BirthDate = birthDate;
    }
    
    public Contact(Guid id, string name, string surname, string email, Category category, Subcategory subcategory, DateTime birthDate, string phoneNumber, string password) : base(id)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Category = category;
        PhoneNumber = phoneNumber;
        Password = password;
        BirthDate = birthDate;
        Subcategory = subcategory;
    }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Category Category { get; set; }
    public Subcategory? Subcategory { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }
}