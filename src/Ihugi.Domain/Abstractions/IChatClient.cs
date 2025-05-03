namespace Ihugi.Domain.Abstractions;

public interface IChatClient
{
    public Task ReceiveMessageAsync(string userName, string message);
    public Task ReceiveAdminMessageAsync(string userName, string message);
}