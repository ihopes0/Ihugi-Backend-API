namespace Ihugi.Application.UseCases.Chats.Commands.UpdateChatPut;

/// <summary>
/// DTO Тела запроса для обновления чата
/// </summary>
/// <param name="Name">Название чата</param>
public sealed record UpdateChatPutRequest(string Name);