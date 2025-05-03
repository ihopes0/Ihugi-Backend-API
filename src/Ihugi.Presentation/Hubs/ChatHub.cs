using Ihugi.Application.Abstractions;
using Ihugi.WebApi.Dtos;
using Ihugi.WebApi.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Ihugi.WebApi.Hubs;

public class ChatHub : Hub<IChatClient>
{
    private readonly ICacheService _cacheService;

    private const string ChatConnectionPrefix = "ChatConnection-";

    public ChatHub(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task JoinChatAsync(UserConnectionDto userConnectionDto)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnectionDto.ChatName);

        await _cacheService.SetAsync($"{ChatConnectionPrefix}{Context.ConnectionId}", userConnectionDto);

        await Clients.Group(userConnectionDto.ChatName)
            .ReceiveAdminMessageAsync("Admin", $"{userConnectionDto.UserName} присоединился к чату.");
    }

    public async Task SendMessageAsync(string message)
    {
        var connection = await GetConnectionAsync();

        if (connection is not null)
        {
            await Clients
                .Group(connection.ChatName)
                .ReceiveMessageAsync(connection.UserName, message);
        }
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connection = await GetConnectionAsync();

        if (connection is not null)
        {
            await _cacheService.RemoveAsync($"{ChatConnectionPrefix}{Context.ConnectionId}");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatName);

            await Clients
                .Group(connection.ChatName)
                .ReceiveAdminMessageAsync("Admin", $"Пользователь {connection.UserName} вышел из чата");
        }

        await base.OnDisconnectedAsync(exception);
    }
    
    private async Task<UserConnectionDto?> GetConnectionAsync()
    {
        return await _cacheService.GetAsync<UserConnectionDto>($"{ChatConnectionPrefix}{Context.ConnectionId}");
    }
}