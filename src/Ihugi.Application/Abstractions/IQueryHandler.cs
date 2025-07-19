using Ihugi.Common.ErrorWork;
using MediatR;

namespace Ihugi.Application.Abstractions;

/// <summary>
/// Хэндлер запроса
/// </summary>
/// <typeparam name="TQuery">Запрос</typeparam>
/// <typeparam name="TResponse">Тип ответа</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}