namespace Application.Category.Abstraction;
using Domain.Entity;
public interface ICategoryRepository
{
    public Task<Category?> GetById(Guid categoryId, CancellationToken cancellationToken = default);
    public Task<Category?> GetByName(string name, CancellationToken cancellationToken = default);
    public Task Create(Category category, CancellationToken cancellationToken = default);
    public void Update(Category category);
    public void Delete(Category category);
}