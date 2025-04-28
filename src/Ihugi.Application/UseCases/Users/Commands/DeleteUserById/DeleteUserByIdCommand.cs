using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;

namespace Ihugi.Application.UseCases.Users.Commands.DeleteUserById;

// TODO: XML docs
public sealed record DeleteUserByIdCommand(Guid UserId) : ICommand;