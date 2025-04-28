using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Entities;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Users.Commands.UpdateUserPut;

internal sealed class UpdateUserPutCommandHandler : ICommandHandler<UpdateUserPutCommand, UpdateUserPutResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserPutCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateUserPutResponse>> Handle(UpdateUserPutCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UpdateUserPutResponse>(DomainErrors.User.NotFound);
        }

        user.Update(
            request.Name,
            request.Password,
            request.Email);

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateUserPutResponse(
            user.Id,
            user.Name,
            user.Password,
            user.Email);

        return Result.Success(response);
    }
}