using Ihugi.Common.ErrorWork;
using MediatR;

namespace Ihugi.Application.Abstractions;

// TODO: XML docs
public interface ICommand : IRequest<Result>
{
}

// TODO: XML docs
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}