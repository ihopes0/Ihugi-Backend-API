using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Users.Queries.GetUsers;

// TODO: XML docs
public sealed record GetUsersQuery() : IQuery<GetUsersResponse>;