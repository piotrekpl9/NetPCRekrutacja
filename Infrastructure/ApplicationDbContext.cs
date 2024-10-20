using Application.Category.Consts;
using Application.Subcategory.Consts;
using Infrastructure.Authentication.Entity;
using Infrastructure.Common.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<AuthenticationData> AuthenticationData { get; set; }
    public DbSet<Domain.Entity.Contact> Contacts { get; set; }
    public DbSet<Domain.Entity.User> Users { get; set; }
    public DbSet<Domain.Entity.Category> Categories { get; set; }
    public DbSet<Domain.Entity.Subcategory> Subcategories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        var workNumberId = Guid.Parse("e669fad2-79f6-4a65-bf80-8f6799663dbf");
        modelBuilder.Entity<Domain.Entity.Category>().HasData(
            new Domain.Entity.Category(workNumberId, CategoryConstants.Business ),
            new Domain.Entity.Category(Guid.Parse("6b2fe5c0-2b92-4fc7-84ab-d5f4885bc907"), CategoryConstants.Private ),
            new Domain.Entity.Category(Guid.Parse("0d6c2e2a-0f2e-4f05-af8e-f2fc8ef3ac11"), CategoryConstants.Other)
        );
        
        modelBuilder.Entity<Domain.Entity.Subcategory>().HasData(
            new Domain.Entity.Subcategory(Guid.Parse("080de9db-ed11-4d44-ab6e-4931fbcce785"),workNumberId,SubcategoryConstants.Boss, true),
            new Domain.Entity.Subcategory(Guid.Parse("b8fb7cf0-3658-4ddd-8992-5340d7e559b0"),workNumberId, SubcategoryConstants.Client, true)
        );
    }

}