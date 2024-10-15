namespace Domain.Entity;

public class Subcategory
{
    public Subcategory()
    {
        
    }
    
    public Subcategory(Guid id, Guid categoryId, string name, bool isDefault)
    {
        Id = id;
        CategoryId = categoryId;
        Name = name;
        IsDefault = isDefault;
    }

    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
}