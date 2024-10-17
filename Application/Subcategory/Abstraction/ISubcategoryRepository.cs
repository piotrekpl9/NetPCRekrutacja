namespace Application.Subcategory.Abstraction;
using Domain.Entity;
public interface ISubcategoryRepository
{
    public Task Create(Subcategory subcategory, CancellationToken cancellationToken = default);
    public Task<Subcategory?> GetById(Guid subcategoryId, CancellationToken cancellationToken = default);
    public Task<Subcategory?> GetByName(string name, CancellationToken cancellationToken = default);
}