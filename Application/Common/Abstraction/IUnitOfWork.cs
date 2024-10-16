namespace Application.Common.Abstraction;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync();
}