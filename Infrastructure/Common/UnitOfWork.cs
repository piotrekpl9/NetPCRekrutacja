using Application.Common.Abstraction;
using Infrastructure.Common.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}