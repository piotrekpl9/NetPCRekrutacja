namespace Domain.Entity;
using Primitives;
public class Category : Entity
{
    public Category(Guid id) : base(id)
    {
        
    }
    
    public Category(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}