using Application.Authentication.Abstraction;
using Application.Category.Abstraction;
using Application.Category.Model.Error;
using Application.Common.Abstraction;
using Application.Contacts.Abstraction;
using Application.Contacts.Command.Create;
using Application.Contacts.Model.Error;
using Application.Subcategory.Abstraction;
using Application.Subcategory.Model.Error;
using Domain.Entity;
using Moq;

namespace Application.UnitTests.Contacts;

public class CreateContactCommandHandlerTests
{
     private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly Mock<ISubcategoryRepository> _subcategoryRepositoryMock;
    private readonly Mock<IAuthenticationService> _authenticationServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateContactCommandHandler _handler;

    public CreateContactCommandHandlerTests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _subcategoryRepositoryMock = new Mock<ISubcategoryRepository>();
        _authenticationServiceMock = new Mock<IAuthenticationService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new CreateContactCommandHandler(
            _contactRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _categoryRepositoryMock.Object,
            _authenticationServiceMock.Object,
            _subcategoryRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenContactWithGivenEmailAlreadyExists()
    {
        // Arrange
        var request =new CreateContactCommand(
            "John",             
            "Doe",              
            "johndoe@example.com",
            "Business",        
            "Client",               
            new DateTime(1985, 6, 15), 
            "password123",      
            "123456789"       
        );
        _contactRepositoryMock.Setup(repo => repo.ExistByEmail(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(ContactError.ContactWithGivenEmailAlreadyExists, result.Error);
        _contactRepositoryMock.Verify(repo => repo.Create(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCategoryNotFound()
    {
        // Arrange
        var request =new CreateContactCommand(
            "John",             
            "Doe",              
            "johndoe@example.com",
            "Nonexistingcategorylalala",        
            "",               
            new DateTime(1985, 6, 15), 
            "password123",      
            "123456789"       
        );
        _contactRepositoryMock.Setup(repo => repo.ExistByEmail(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _categoryRepositoryMock.Setup(repo => repo.GetByName(request.CategoryName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(()=>null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(CategoryError.CategoryNotFound, result.Error);
        _contactRepositoryMock.Verify(repo => repo.Create(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSubcategoryNotFoundForBusinessCategory()
    {
        // Arrange
        var request =new CreateContactCommand(
            "John",             
            "Doe",              
            "johndoe@example.com",
            "Business",        
            "Client",               
            new DateTime(1985, 6, 15), 
            "password123",      
            "123456789"       
        );

        var category = new Domain.Entity.Category(Guid.Parse("e669fad2-79f6-4a65-bf80-8f6799663dbf"), "Business");


        _contactRepositoryMock.Setup(repo => repo.ExistByEmail(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _categoryRepositoryMock.Setup(repo => repo.GetByName(request.CategoryName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);
        _subcategoryRepositoryMock.Setup(repo => repo.GetByName(request.SubcategoryName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(()=>null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(SubcategoryError.SubcategoryNotFound, result.Error);
        _contactRepositoryMock.Verify(repo => repo.Create(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldCreateNewContact_WhenAllConditionsAreMet()
    {
        // Arrange
        var request =new CreateContactCommand(
            "John",             
            "Doe",              
            "johndoe@example.com",
            "Private",        
            "",               
            new DateTime(1985, 6, 15), 
            "password123",      
            "123456789"       
        );
        var category = new Domain.Entity.Category(Guid.Parse("6b2fe5c0-2b92-4fc7-84ab-d5f4885bc907"), "Private");

        _contactRepositoryMock.Setup(repo => repo.ExistByEmail(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _categoryRepositoryMock.Setup(repo => repo.GetByName(request.CategoryName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);
        _authenticationServiceMock.Setup(auth => auth.HashPassword(request.Password))
            .Returns("hashedpassword");

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        _contactRepositoryMock.Verify(repo => repo.Create(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateSubcategoryForOtherCategory_WhenSubcategoryDoesNotExist()
    {
        // Arrange
        var request =new CreateContactCommand(
            "John",             
            "Doe",              
            "johndoe@example.com",
            "Other",        
            "IT",               
            new DateTime(1985, 6, 15), 
            "password123",      
            "123456789"       
        );
        var category = new Domain.Entity.Category(Guid.Parse("0d6c2e2a-0f2e-4f05-af8e-f2fc8ef3ac11"), "Other");

        _contactRepositoryMock.Setup(repo => repo.ExistByEmail(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _categoryRepositoryMock.Setup(repo => repo.GetByName(request.CategoryName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);
        _subcategoryRepositoryMock.Setup(repo => repo.GetByName(request.SubcategoryName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(()=>null);
        _authenticationServiceMock.Setup(auth => auth.HashPassword(request.Password))
            .Returns("hashedpassword");

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        _subcategoryRepositoryMock.Verify(repo => repo.Create(It.IsAny<Domain.Entity.Subcategory>(), It.IsAny<CancellationToken>()), Times.Once);
        _contactRepositoryMock.Verify(repo => repo.Create(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}