using Application.Authentication.Abstraction;
using Application.Category.Abstraction;
using Application.Category.Model.Error;
using Application.Common.Abstraction;
using Application.Contacts.Abstraction;
using Application.Contacts.Model.Error;
using Domain.Common.Result;

namespace Application.Contacts.Command.Update;

public sealed class UpdateContactCommandHandler : ICommandHandler<UpdateContactCommand>
{
    private readonly IContactRepository _contactRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateContactCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IAuthenticationService authenticationService)
    {
        _contactRepository = contactRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _authenticationService = authenticationService;
    }

    public async Task<Result> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetById(request.ContactId, cancellationToken);
        if (contact is null)
        {
            return Result.Failure(ContactError.ContactNotFound);
        }

        if (contact.Category.Id != request.CategoryId)
        {
            //TODO Do something with subcategory after category update
            var category = await _categoryRepository.GetById(request.CategoryId, cancellationToken);
            if (category is null)
            {
                return Result.Failure(CategoryError.CategoryNotFound);
            }
            contact.Category = category;
        }

        if (contact.Email != request.Email)
        {
            var emailExist = await _contactRepository.ExistByEmail(request.Email, cancellationToken);
            if (emailExist)
            {
                return Result.Failure(ContactError.ContactWithGivenEmailAlreadyExists);
            }
            contact.Email = request.Email;
        }
        contact.BirthDate = request.BirthDate;
        contact.PhoneNumber = request.PhoneNumber;
        contact.Name = request.Name;
        contact.Surname = request.Surname;
        contact.Password = _authenticationService.HashPassword(request.Password);
        
        _contactRepository.Update(contact);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}