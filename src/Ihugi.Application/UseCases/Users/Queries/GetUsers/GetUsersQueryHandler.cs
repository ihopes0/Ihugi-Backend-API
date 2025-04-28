using Ihugi.Application.Abstractions;
using Ihugi.Application.UseCases.Users.Queries.GetUserById;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Users.Queries.GetUsers;

// TODO: XML docs
internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, GetUsersResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        var response = new GetUsersResponse(
            users
                .Select(u => new UserResponse(u.Id, u.Name))
                .ToArray());

        return Result.Success(response);
    }
}