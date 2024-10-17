using Domain.Primitives;
using Infrastructure.Common.Abstraction;

namespace Infrastructure.Common;

public abstract class RepositoryBase<T> where T : Entity
{
    protected readonly IApplicationDbContext DbContext;

    protected RepositoryBase(IApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }
    
    public virtual async Task Create(T entity, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public virtual void Update(T entity)
    {
        DbContext.Set<T>().Update(entity);
    }

    public virtual void Delete(T entity)
    {
        DbContext.Set<T>().Remove(entity);
    }
}