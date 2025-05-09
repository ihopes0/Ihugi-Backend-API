using Microsoft.AspNetCore.SignalR;

namespace Ihugi.Domain.Abstractions;

public interface IChatClient : IClientProxy
{
    public Task ReceiveMessageAsync(string userName, string message);
    public Task ReceiveAdminMessageAsync(string userName, string message);
}