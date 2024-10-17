using Domain.Common.Result;
using MediatR;

namespace Application.Common.Abstraction;

public interface ICommand : IRequest<Result>
{
    
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
    
}