using System.Net.Mime;
using Ihugi.Application.UseCases.Users.Commands.CreateUser;
using Ihugi.Application.UseCases.Users.Commands.DeleteUserById;
using Ihugi.Application.UseCases.Users.Commands.UpdateUserPut;
using Ihugi.Application.UseCases.Users.Queries.GetUserById;
using Ihugi.Application.UseCases.Users.Queries.GetUsers;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Errors;
using Ihugi.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ihugi.Presentation.Controllers;

// TODO: XML docs
// TODO: Поменять ответы с IActionResult на ProblemDetails
// TODO: Добавить Swagger API документацию
[Route("api/users")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class UsersController : ApiController
{
    public UsersController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(request) : BadRequest(result.Error);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetUsersResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetUsersQuery(), cancellationToken);

        return result.IsSuccess ? Ok(result.Value!.Users) : BadRequest(result.Error);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(UserResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    // TODO: Заменить DeleteUserByIdCommand на id из пути
    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    [ProducesResponseType(typeof(IActionResult), 204)]
    public async Task<IActionResult> DeleteUserById(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        if (result.IsFailure && result.Error == DomainErrors.User.NoContent)
        {
            return NoContent();
        }

        if (result.IsSuccess)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(UpdateUserPutResponse), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> UpdateUserPut(
        Guid id,
        [FromBody] UpdateUserPutRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateUserPutCommand(
            id,
            request.Name,
            request.Password,
            request.Email
        );

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
}