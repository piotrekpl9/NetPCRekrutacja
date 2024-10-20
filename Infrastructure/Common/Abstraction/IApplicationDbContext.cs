using Domain.Entity;
using Infrastructure.Authentication.Entity;
using Infrastructure.Authentication.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Abstraction;
using Domain.Entity;
public interface IApplicationDbContext
{
    public DbSet<AuthenticationData> AuthenticationData { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Domain.Entity.User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }
    public DbSet<T> Set<T>() where T : class;
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
}