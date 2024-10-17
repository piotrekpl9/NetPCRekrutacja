namespace Domain.Entity;
using Primitives;
public class Category : Entity
{
    public Category()
    {
        
    }
    
    public Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}