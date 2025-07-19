using Ihugi.Common.ErrorWork;
using MediatR;

namespace Ihugi.Application.Abstractions;

/// <summary>
/// Команда без ответа
/// </summary>
public interface ICommand : IRequest<Result>
{
}

/// <summary>
/// Команда с ответом TResponse
/// </summary>
/// <typeparam name="TResponse">Тип ответа</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}