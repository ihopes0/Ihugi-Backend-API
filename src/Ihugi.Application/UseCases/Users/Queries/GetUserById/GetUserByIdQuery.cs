using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Users.Queries.GetUserById;

// TODO: XML docs
public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;