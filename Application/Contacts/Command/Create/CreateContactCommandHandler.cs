using Application.Authentication.Abstraction;
using Application.Category.Abstraction;
using Application.Category.Model.Error;
using Application.Common.Abstraction;
using Application.Contacts.Abstraction;
using Application.Contacts.Model.Error;
using Application.Subcategory.Abstraction;
using Application.Subcategory.Model.Error;
using Domain.Common.Result;
using Domain.Entity;

namespace Application.Contacts.Command.Create;
using Domain.Entity;
public sealed class CreateContactCommandHandler : ICommandHandler<CreateContactCommand>
{
    private readonly IContactRepository _contactRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateContactCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IAuthenticationService authenticationService, ISubcategoryRepository subcategoryRepository)
    {
        _contactRepository = contactRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _authenticationService = authenticationService;
        _subcategoryRepository = subcategoryRepository;
    }

    public async Task<Result> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var emailExist = await _contactRepository.ExistByEmail(request.Email, cancellationToken);
        if (emailExist)
        {
            return Result.Failure(ContactError.ContactWithGivenEmailAlreadyExists);
        }

        var category = await _categoryRepository.GetByName(request.CategoryName, cancellationToken);
        if (category is null)
        {
            return Result.Failure(CategoryError.CategoryNotFound);
        }
        
        var contact = new Contact(
            Guid.NewGuid(),
            request.Name,
            request.Surname,
            request.Email,
            category, 
            request.BirthDate.ToUniversalTime(), 
            request.PhoneNumber,
            _authenticationService.HashPassword(request.Password));
        
        if (category.Name == "Business" && request.SubcategoryName is not null  && request.SubcategoryName.Length > 0)
        {
            var subcategory = await _subcategoryRepository.GetByName(request.SubcategoryName, cancellationToken);
            if (subcategory is null)
            {
                return Result.Failure(SubcategoryError.SubcategoryNotFound);
            }

            if (subcategory.IsDefault)
            {
                contact.Subcategory = subcategory;
            }
        }
        
        if (category.Name == "Other" && request.SubcategoryName is not null  && request.SubcategoryName.Length > 0)
        {
            var subcategory = await _subcategoryRepository.GetByName(request.SubcategoryName, cancellationToken);
            if (subcategory is null)
            {
                subcategory = new Subcategory(Guid.NewGuid(), category.Id ,request.SubcategoryName, false);
                await _subcategoryRepository.Create(subcategory, cancellationToken);
            }

            contact.Subcategory = subcategory;
        }
        
       
        await _contactRepository.Create(contact, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}