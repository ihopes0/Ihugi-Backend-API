using Ihugi.Application.Abstractions;
using Ihugi.Application.Dtos;
using Ihugi.Common.Constants;
using Ihugi.Domain.Abstractions;
using Ihugi.Domain.ValueObjects;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using SignalRSwaggerGen.Enums;

namespace Ihugi.Presentation.Hubs;

[SignalRHub]
public class ChatHub : Hub<IChatClient>
{
    private readonly IConnectionManager _connectionManager;

    private const string ChatConnectionPrefix = SignalRConstants.ChatConnectionCachePrefix;

    public ChatHub(IConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    [SignalRMethod(summary: "Establish connection with particular chat.")]
    public async Task JoinChatAsync(UserConnectionDto userConnectionDto)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnectionDto.ChatName);

        await _connectionManager.AddConnectionAsync(
            $"{ChatConnectionPrefix}{Context.ConnectionId}",
            new UserConnection(
                userConnectionDto.UserName,
                userConnectionDto.ChatName
            ));
        
        await Clients.Group(userConnectionDto.ChatName)
            .ReceiveAdminMessageAsync("Admin", $"{userConnectionDto.UserName} joined chat.");
    }

    [SignalRMethod(summary: "Send message to all chat members.")]
    public async Task SendMessageAsync(string message)
    {
        var connection = await GetConnectionAsync();

        if (connection is not null)
        {
            System.Console.WriteLine(connection?.ChatName + "+" + connection?.UserName);

            await Clients
                .Group(connection.ChatName)
                .ReceiveMessageAsync(connection.UserName, message);
        }
    }

    [SignalRHidden]
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connection = await GetConnectionAsync();

        if (connection is not null)
        {
            await _connectionManager.RemoveConnectionAsync($"{ChatConnectionPrefix}{Context.ConnectionId}");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatName);

            await Clients
                .Group(connection.ChatName)
                .ReceiveAdminMessageAsync("Admin", $"Пользователь {connection.UserName} вышел из чата");
        }

        await base.OnDisconnectedAsync(exception);
    }

    private async Task<UserConnection?> GetConnectionAsync()
    {
        return await _connectionManager
            .GetConnectionAsync($"{ChatConnectionPrefix}{Context.ConnectionId}");
    }
}