using Ihugi.Common.ErrorWork;
using MediatR;

namespace Ihugi.Application.Abstractions;

/// <summary>
/// Запрос с ответом TResponse
/// </summary>
/// <typeparam name="TResponse">Тип ответа</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}