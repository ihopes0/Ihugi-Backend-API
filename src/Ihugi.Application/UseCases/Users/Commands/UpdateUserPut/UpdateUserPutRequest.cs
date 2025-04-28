namespace Ihugi.Application.UseCases.Users.Commands.UpdateUserPut;

public sealed record UpdateUserPutRequest(
    string Name,
    string Password,
    string Email
);