using Ihugi.Domain.Entities;

namespace Ihugi.Application.Abstractions;

/// <summary>
/// Провайдер JWT-токена аутентификации
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// Сгенерировать JWT-токен
    /// </summary>
    /// <param name="user">Пользователь</param>
    string Generate(User user);
}