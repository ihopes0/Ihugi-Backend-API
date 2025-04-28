using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Users.Commands.CreateUser;

// TODO: XML docs
public sealed record CreateUserCommand(
    string Name,
    string Password,
    string Email) : ICommand<CreateUserResponse>;