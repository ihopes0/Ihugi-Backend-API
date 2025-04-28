using Ihugi.Application.UseCases.Users.Queries.GetUserById;
using Ihugi.Domain.Entities;

namespace Ihugi.Application.UseCases.Users.Queries.GetUsers;

// TODO: XML docs
public sealed record GetUsersResponse(UserResponse[] Users);