using Domain.Common.Result;
using MediatR;

namespace Application.Common.Abstraction;

public interface IQuery <TResponse> : IRequest<Result<TResponse>>
{
    
}