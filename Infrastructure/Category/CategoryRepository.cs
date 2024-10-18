using Application.Category.Abstraction;
using Infrastructure.Common;
using Infrastructure.Common.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Category;

public class CategoryRepository : RepositoryBase<Domain.Entity.Category>, ICategoryRepository
{
    public CategoryRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<Domain.Entity.Category?> GetById(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Categories.FirstOrDefaultAsync(category => category.Id == categoryId, cancellationToken);
    }

    public async Task<Domain.Entity.Category?> GetByName(string name, CancellationToken cancellationToken = default)
    {
        return await DbContext.Categories.FirstOrDefaultAsync(category => category.Name.ToLower()==name.ToLower(), cancellationToken);

    }
}