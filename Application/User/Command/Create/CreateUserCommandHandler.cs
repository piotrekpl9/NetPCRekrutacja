using Application.Authentication.Abstraction;
using Application.Common.Abstraction;
using Application.User.Abstraction;
using Application.User.Model.Error;
using Domain.Common.Result;

namespace Application.User.Command.Create;
using Domain.Entity;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateUserCommandHandler(IAuthenticationService authenticationService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailCheck = await _userRepository.ExistByEmail(request.Email, cancellationToken);
        if (emailCheck)
        {
            return Result.Failure(UserError.UserWithGivenEmailAlreadyExists);
        }

        var user = new User(Guid.NewGuid(), request.Email);
        await _userRepository.Create(user, cancellationToken);
        
        var registrationRes = await _authenticationService.Register(user.Id, request.Password, cancellationToken);
        if (registrationRes.IsFailure)
        {
            return registrationRes;
        }
        
        await _unitOfWork.SaveChangesAsync();
        return registrationRes;
    }
}