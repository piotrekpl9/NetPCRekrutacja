using Domain.Common.Result;
using MediatR;

namespace Application.Common.Abstraction;

public interface IQueryHandler <TQuery, TResponse> : IRequestHandler<TQuery,Result<TResponse>> 
    where TQuery : IQuery<TResponse>
{
    
}