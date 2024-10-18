using Application.Common.Abstraction;
using Application.Contacts.Model;

namespace Application.Contacts.Query.GetAll;

public sealed record GetAllContactsQuery() : IQuery<List<ContactResponse>>;