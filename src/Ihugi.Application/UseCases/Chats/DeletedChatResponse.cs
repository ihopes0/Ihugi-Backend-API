namespace Ihugi.Application.UseCases.Chats;

/// <summary>
/// DTO ответа на удаление чата
/// </summary>
/// <param name="Message">Сообщение</param>
public sealed record DeletedChatResponse(string Message);