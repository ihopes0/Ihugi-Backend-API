namespace Ihugi.Application.Abstractions;

// TODO: XML docs
public interface IRealTimeCommunicationService
{
    Task SendMessageToUserAsync(string userId, string message);

    Task SendMessageToGroupAsync(string groupName, string message);

    Task AddToGroupAsync(string userId, string groupName);

    Task RemoveFromGroupAsync(string userId, string groupName);
}