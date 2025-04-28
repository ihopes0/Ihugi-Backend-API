namespace Ihugi.Application.UseCases.Users.Commands.UpdateUserPut;

public sealed record UpdateUserPutResponse(
    Guid Id,
    string Name,
    string Password,
    string Email);