namespace Domain.Primitives;

public abstract class Entity(Guid id)
{
    public Guid Id { get; init; } = id;
    public override bool Equals(object? obj)
    {
        if (obj is not Entity e)
        {
            return false;
        }

        return e.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}