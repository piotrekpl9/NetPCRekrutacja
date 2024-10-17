using Application.Common.Abstraction;
using Application.Contacts.Abstraction;
using Application.Contacts.Model.Error;
using Domain.Common.Result;

namespace Application.Contacts.Command.Delete;

public sealed class DeleteContactCommandHandler : ICommandHandler<DeleteContactCommand>
{
    private readonly IContactRepository _contactRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteContactCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork)
    {
        _contactRepository = contactRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetById(request.ContactId, cancellationToken);
        if (contact is null)
        {
            return Result.Failure(ContactError.ContactNotFound);
        }

        _contactRepository.Delete(contact);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}