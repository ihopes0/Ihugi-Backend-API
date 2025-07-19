namespace Ihugi.Application.UseCases.Chats;

/// <summary>
/// DTO ответа списка чатов
/// </summary>
/// <param name="Chats">Массив с DTO ChatResponse</param>
public sealed record ChatsResponse(ChatResponse[] Chats);