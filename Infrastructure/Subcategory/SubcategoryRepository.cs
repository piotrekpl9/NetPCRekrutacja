using Application.Subcategory.Abstraction;
using Infrastructure.Common;
using Infrastructure.Common.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Subcategory;

public class SubcategoryRepository : RepositoryBase<Domain.Entity.Subcategory>,ISubcategoryRepository
{
    public SubcategoryRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<Domain.Entity.Subcategory?> GetById(Guid subcategoryId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Subcategories.FirstOrDefaultAsync(subcategory => subcategory.Id == subcategoryId, cancellationToken: cancellationToken);
    }

    public async Task<Domain.Entity.Subcategory?> GetByName(string name, CancellationToken cancellationToken = default)
    {
        return await DbContext.Subcategories.FirstOrDefaultAsync(subcategory => subcategory.Name == name, cancellationToken: cancellationToken);

    }
}