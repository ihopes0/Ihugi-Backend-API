namespace Ihugi.Application.Abstractions;

/// <summary>
/// Сервис для управления кэшем
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Получить закэшированное значение в виде экземпляра типа T
    /// </summary>
    /// <param name="key">Ключ кэша</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
    /// <typeparam name="T">Тип кэшируемого объекта</typeparam>
    /// <returns>Экземпляр типа Т</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    /// Получить закэшированное значение в виде экземпляра типа T или создать новый с помощью делегата factory
    /// </summary>
    /// <param name="key">Ключ кэша</param>
    /// <param name="factory">Делегат для создания экземпляра типа T</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
    /// <typeparam name="T">Тип кэшируемого объекта</typeparam>
    /// <returns>Экземпляр типа Т</returns>
    Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    /// Задать кэш
    /// </summary>
    /// <param name="key">Ключ кэша</param>
    /// <param name="value">Значение кэша</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
    /// <typeparam name="T">Тип кэшируемого объекта</typeparam>
    Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    /// Удалить закэшированное значение по ключу кэша
    /// </summary>
    /// <param name="key">Ключ кэша</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить все закэшированные значения с определенным префиксом
    /// </summary>
    /// <param name="prefixKey">Префикс ключа кэша</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
    Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все закэшированные значения с определенным префиксом
    /// </summary>
    /// <param name="prefixKey">Префикс ключа кэша</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
    /// <typeparam name="T">Тип кэшируемого объекта</typeparam>
    /// <returns>Экземпляр типа Т</returns>
    Task<IEnumerable<T>> GetByPrefixAsync<T>(string prefixKey, CancellationToken cancellationToken = default)
        where T: class;
}