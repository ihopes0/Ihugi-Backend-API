using Ihugi.Common.ErrorWork;
using MediatR;

namespace Ihugi.Application.Abstractions;

// TODO: XML docs
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}