using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Users.Commands.Login;

public sealed record LoginCommand(string Email) : ICommand<LoginResponse>;