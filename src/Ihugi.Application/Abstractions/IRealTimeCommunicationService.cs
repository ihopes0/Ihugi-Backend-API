using Microsoft.AspNetCore.SignalR;

namespace Ihugi.Application.Abstractions;

/// <summary>
/// RTC сервис
/// </summary>
public interface IRealTimeCommunicationService
{
    /// <summary>
    /// Отправить сообщение пользователю
    /// </summary>
    /// <param name="userId">ID пользователя</param>
    /// <param name="message">Сообщение</param>
    Task SendMessageToUserAsync(string userId, string message);

    /// <summary>
    /// Отправить сообщение группе
    /// </summary>
    /// <param name="groupName">Название группы</param>
    /// <param name="userName">Имя отправителя сообщения</param>
    /// <param name="message">Сообщение</param>
    Task SendMessageToGroupAsync(string groupName, string userName, string message);

    /// <summary>
    /// Добавить пользователя в группу
    /// </summary>
    /// <param name="userId">ID пользователя</param>
    /// <param name="groupName">Название группы</param>
    Task AddToGroupAsync(string userId, string groupName);

    /// <summary>
    /// Удалить пользователя из группы
    /// </summary>
    /// <param name="userId">ID пользователя</param>
    /// <param name="groupName">Название группы</param>
    Task RemoveFromGroupAsync(string userId, string groupName);
}