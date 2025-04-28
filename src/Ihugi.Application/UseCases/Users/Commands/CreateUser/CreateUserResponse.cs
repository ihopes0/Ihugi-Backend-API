namespace Ihugi.Application.UseCases.Users.Commands.CreateUser;

public sealed record CreateUserResponse(Guid Id, string Name, string Password, string Email);