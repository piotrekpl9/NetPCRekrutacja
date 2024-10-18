namespace Domain.Entity;
using Primitives;

public class Subcategory : Entity
{
    //TODO Consider switching to VO
    public Subcategory(Guid id) : base(id)
    {
        
    }
    
    public Subcategory(Guid id, Guid categoryId, string? name, bool isDefault) : base(id)
    {
        CategoryId = categoryId;
        Name = name;
        IsDefault = isDefault;
    }

    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public bool IsDefault { get; set; }
    
    public override string? ToString()
    {
        return Name;
    }
}