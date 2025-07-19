using Ihugi.Common.ErrorWork;
using MediatR;

namespace Ihugi.Application.Abstractions;

/// <summary>
/// Хэндлер команды без ответа
/// </summary>
/// <typeparam name="TCommand">Команда</typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

/// <summary>
/// Хэндлер команды с ответом
/// </summary>
/// <typeparam name="TCommand">Команда</typeparam>
/// <typeparam name="TResponse">Тип ответа</typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}