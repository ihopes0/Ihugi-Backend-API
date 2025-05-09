using Ihugi.Application.Abstractions;
using Ihugi.Common.Constants;
using Ihugi.Domain.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Ihugi.Infrastructure.RealTime;

// TODO: XML docs
public sealed class RtcService<THub> : IRealTimeCommunicationService
    where THub : Hub
{
    private readonly IHubContext<THub> _hubContext;
    private readonly IConnectionManager _connectionManager;

    public RtcService(IHubContext<THub> hubContext, IConnectionManager connectionManager)
    {
        _hubContext = hubContext;
        _connectionManager = connectionManager;
    }

    // TODO: SendMessageToUserAsync
    public Task SendMessageToUserAsync(string userId, string message)
    {
        throw new NotImplementedException();
    }

    public async Task SendMessageToGroupAsync(string groupName, string userName, string message)
    {
        await _hubContext.Clients.Group(groupName).SendAsync(SignalRConstants.ReceiveMessage, userName, message);
    }

    // TODO: AddToGroupAsync
    public Task AddToGroupAsync(string userId, string groupName)
    {
        throw new NotImplementedException();
    }

    // TODO: RemoveFromGroupAsync
    public Task RemoveFromGroupAsync(string userId, string groupName)
    {
        throw new NotImplementedException();
    }
}