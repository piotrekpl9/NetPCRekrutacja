using Application.Common.Abstraction;
using Application.Contacts.Abstraction;
using Application.Contacts.Model;
using AutoMapper;
using Domain.Common.Result;

namespace Application.Contacts.Query.GetAll;

public class GetAllContactsQueryHandler : IQueryHandler<GetAllContactsQuery,List<ContactResponse>>
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public GetAllContactsQueryHandler(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ContactResponse>>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _contactRepository.GetAll(cancellationToken);
        return Result<List<ContactResponse>>.Success(_mapper.Map<List<ContactResponse>>(contacts));
    }
}