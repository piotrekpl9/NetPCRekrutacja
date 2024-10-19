using Application.Authentication.Abstraction;
using Application.Category.Abstraction;
using Application.Category.Model.Error;
using Application.Common.Abstraction;
using Application.Contacts.Abstraction;
using Application.Contacts.Command.Update;
using Application.Contacts.Model.Error;
using Application.Subcategory.Abstraction;
using Domain.Entity;
using Moq;

namespace Application.UnitTests.Contacts;

public class UpdateContactCommandHandlerTests
{
      private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly Mock<ISubcategoryRepository> _subcategoryRepositoryMock;
    private readonly Mock<IAuthenticationService> _authenticationServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateContactCommandHandler _handler;

    public UpdateContactCommandHandlerTests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _subcategoryRepositoryMock = new Mock<ISubcategoryRepository>();
        _authenticationServiceMock = new Mock<IAuthenticationService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new UpdateContactCommandHandler(
            _contactRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _categoryRepositoryMock.Object,
            _authenticationServiceMock.Object,
            _subcategoryRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenContactNotFound()
    {
        // Arrange
        var request =new UpdateContactCommand(
            Guid.NewGuid(),
            "John",             
            "Doe",              
            "newemail@example.com",
            "Other",        
            "IT",               
            new DateTime(1985, 6, 15), 
            "password123",      
            "123456789"       
        );
        _contactRepositoryMock.Setup(repo => repo.GetById(request.ContactId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(ContactError.ContactNotFound, result.Error);
        _contactRepositoryMock.Verify(repo => repo.Update(It.IsAny<Contact>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCategoryNotFound()
    {
        // Arrange
        var contact = new Contact(Guid.NewGuid(), "John", "Doe", "existinguser@example.com", new Domain.Entity.Category(Guid.Parse("6b2fe5c0-2b92-4fc7-84ab-d5f4885bc907"), "Private"), DateTime.UtcNow, "123456789", "hashedPassword");
        var request =new UpdateContactCommand(
            contact.Id,
            "John",             
            "Doe",              
            "johndoe@example.com",
            "NonExistentCategory",        
            "",               
            new DateTime(1985, 6, 15), 
            "password123",      
            "123456789"       
        );
        _contactRepositoryMock.Setup(repo => repo.GetById(request.ContactId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(contact);
        _categoryRepositoryMock.Setup(repo => repo.GetByName(request.CategoryName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(CategoryError.CategoryNotFound, result.Error);
        _contactRepositoryMock.Verify(repo => repo.Update(It.IsAny<Contact>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenEmailAlreadyExists()
    {
        // Arrange
        var contact = new Contact(Guid.NewGuid(), "John", "Doe", "existinguser@example.com", new Domain.Entity.Category(Guid.Parse("6b2fe5c0-2b92-4fc7-84ab-d5f4885bc907"), "Private"), DateTime.UtcNow, "123456789", "hashedPassword");

        var request =new UpdateContactCommand(
            contact.Id,
            "John",             
            "Doe",              
            "duplicate@example.com",
            "Private",        
            "",               
            new DateTime(1985, 6, 15), 
            "password123",      
            "123456789"       
        );
        _contactRepositoryMock.Setup(repo => repo.GetById(request.ContactId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(contact);
        _contactRepositoryMock.Setup(repo => repo.ExistByEmail(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(ContactError.ContactWithGivenEmailAlreadyExists, result.Error);
        _contactRepositoryMock.Verify(repo => repo.Update(It.IsAny<Contact>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldUpdateContact_WhenAllConditionsAreMet()
    {
        // Arrange
        var contact = new Contact(Guid.NewGuid(), "John", "Doe", "existinguser@example.com", new Domain.Entity.Category(Guid.Parse("6b2fe5c0-2b92-4fc7-84ab-d5f4885bc907"), "Private"), DateTime.UtcNow, "123456789", "hashedPassword");
        
        var request =new UpdateContactCommand(
            contact.Id,
            "Jane",             
            "Doe",              
            "newemail@example.com",
            "Business",        
            "Client",               
            new DateTime(1985, 6, 15), 
            "password123",      
            "987654321"       
        );

        var newCategory = new Domain.Entity.Category(Guid.Parse("e669fad2-79f6-4a65-bf80-8f6799663dbf"), "Business");
        var subcategory = new Domain.Entity.Subcategory(Guid.NewGuid(), newCategory.Id, "IT", true);

        _contactRepositoryMock.Setup(repo => repo.GetById(request.ContactId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(contact);
        _contactRepositoryMock.Setup(repo => repo.ExistByEmail(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _categoryRepositoryMock.Setup(repo => repo.GetByName(request.CategoryName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(newCategory);
        _subcategoryRepositoryMock.Setup(repo => repo.GetByName(request.SubcategoryName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(subcategory);
        _authenticationServiceMock.Setup(auth => auth.HashPassword(request.Password))
            .Returns("hashedNewPassword");

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("newemail@example.com", contact.Email);
        Assert.Equal("Jane", contact.Name);
        Assert.Equal("Doe", contact.Surname);
        Assert.Equal("987654321", contact.PhoneNumber);
        Assert.Equal("hashedNewPassword", contact.Password);
        Assert.Equal(newCategory, contact.Category);
        Assert.Equal(subcategory, contact.Subcategory);

        _contactRepositoryMock.Verify(repo => repo.Update(It.IsAny<Contact>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}