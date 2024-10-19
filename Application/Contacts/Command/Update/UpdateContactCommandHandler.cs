using Application.Authentication.Abstraction;
using Application.Category.Abstraction;
using Application.Category.Model.Error;
using Application.Common.Abstraction;
using Application.Contacts.Abstraction;
using Application.Contacts.Model.Error;
using Application.Subcategory.Abstraction;
using Application.Subcategory.Model.Error;
using Domain.Common.Result;

namespace Application.Contacts.Command.Update;

public sealed class UpdateContactCommandHandler : ICommandHandler<UpdateContactCommand>
{
    private readonly IContactRepository _contactRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateContactCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IAuthenticationService authenticationService, ISubcategoryRepository subcategoryRepository)
    {
        _contactRepository = contactRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _authenticationService = authenticationService;
        _subcategoryRepository = subcategoryRepository;
    }

    public async Task<Result> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetById(request.ContactId, cancellationToken);
        if (contact is null)
        {
            return Result.Failure(ContactError.ContactNotFound);
        }

        if (contact.Category.Name != request.CategoryName)
        {
            var category = await _categoryRepository.GetByName(request.CategoryName, cancellationToken);
            if (category is null)
            {
                return Result.Failure(CategoryError.CategoryNotFound);
            }

            if (category.Name != "Private")
            {
                if (request.SubcategoryName != contact.Subcategory?.Name)
                {
                    var subcategory = await _subcategoryRepository.GetByName(request.SubcategoryName, cancellationToken);

                    if (category.Name == "Business")
                    {
                        if (subcategory is null || !subcategory.IsDefault)
                        {
                            subcategory = null;
                        }
                        
                    }
                    else if(category.Name == "Other")
                    {
                        if (subcategory is null)
                        {
                            subcategory = new Domain.Entity.Subcategory(Guid.NewGuid(), category.Id ,request.SubcategoryName, false);
                            await _subcategoryRepository.Create(subcategory, cancellationToken);
                        }
                    }
                    
                    contact.Subcategory = subcategory;
                }
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
        
        contact.BirthDate = request.BirthDate.ToUniversalTime();
        contact.PhoneNumber = request.PhoneNumber;
        contact.Name = request.Name;
        contact.Surname = request.Surname;
        contact.Password = _authenticationService.HashPassword(request.Password);
        
        _contactRepository.Update(contact);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}