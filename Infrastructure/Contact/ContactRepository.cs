using Application.Contacts.Abstraction;
using Infrastructure.Common;
using Infrastructure.Common.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contact;

public class ContactRepository : RepositoryBase<Domain.Entity.Contact>,IContactRepository 
{
    public ContactRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Domain.Entity.Contact?> GetById(Guid contactId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Contacts.FirstOrDefaultAsync(contact => contact.Id == contactId, cancellationToken: cancellationToken);
    }

    public async Task<List<Domain.Entity.Contact>> GetAll(CancellationToken cancellationToken = default)
    {
        return await DbContext.Contacts.ToListAsync(cancellationToken);

    }

    public async Task<bool> ExistByEmail(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Contacts.AnyAsync(contact => contact.Email == email, cancellationToken: cancellationToken);
    }

}