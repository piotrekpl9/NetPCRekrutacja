namespace Domain.Entity;
using Primitives;

public class Contact : Entity
{
    public Contact()
    {
        
    }
    
    public Contact(Guid id, string name, string surname, string email, Category category, DateTime birthDate, string phoneNumber)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Category = category;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }
    
    public Contact(Guid id, string name, string surname, string email, Category category, Subcategory subcategory, DateTime birthDate, string phoneNumber)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Category = category;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        Subcategory = subcategory;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public Category Category { get; set; }
    public Subcategory? Subcategory { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }
}