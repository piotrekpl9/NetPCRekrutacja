using Application.Authentication.Abstraction;
using Application.Common;
using Application.Common.Abstraction;
using Application.User.Abstraction;
using Application.User.Command.Create;
using Application.User.Model.Error;
using AutoMapper;
using Domain.Common.Result;
using Moq;

namespace Application.UnitTests.User;
using Domain.Entity;

public class CreateUserCommandHandlerTests
{
  private readonly Mock<IAuthenticationService> _authenticationServiceMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _authenticationServiceMock = new Mock<IAuthenticationService>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new CreateUserCommandHandler(
            _authenticationServiceMock.Object,
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserWithGivenEmailAlreadyExists()
    {
        // Arrange
        var request = new CreateUserCommand("newuser@example.com","password123");
        _userRepositoryMock.Setup(repo => repo.ExistByEmail(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserError.UserWithGivenEmailAlreadyExists, result.Error);
        _userRepositoryMock.Verify(repo => repo.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        _authenticationServiceMock.Verify(auth => auth.Register(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUserCreatedAndRegistrationSucceeds()
    {
        // Arrange
        var request = new CreateUserCommand("newuser@example.com","password123");
        _userRepositoryMock.Setup(repo => repo.ExistByEmail(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        _authenticationServiceMock.Setup(auth => auth.Register(It.IsAny<Guid>(), request.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        _userRepositoryMock.Verify(repo => repo.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        _authenticationServiceMock.Verify(auth => auth.Register(It.IsAny<Guid>(), request.Password, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}