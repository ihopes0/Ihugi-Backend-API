using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Users.Commands.UpdateUserPut;

public sealed record UpdateUserPutCommand(
    Guid Id,
    string Name,
    string Password,
    string Email)
    : ICommand<UpdateUserPutResponse>;