using Domain.Entity;

namespace Application.Contacts.Abstraction;

public interface IContactRepository
{
    public Task Create(Contact contact, CancellationToken cancellationToken = default);
    public void Update(Contact contact);
    public void Delete(Contact contact);
    
    public Task<Contact?> GetById(Guid contactId,CancellationToken cancellationToken = default);
    public Task<List<Contact>> GetAll(CancellationToken cancellationToken = default);
    public Task<bool> ExistByEmail(string email, CancellationToken cancellationToken = default);
}