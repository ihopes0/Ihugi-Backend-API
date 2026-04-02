using System.Net.Mime;
using Ihugi.Application.UseCases.Chats;
using Ihugi.Application.UseCases.Users.Commands.CreateUser;
using Ihugi.Application.UseCases.Users.Commands.DeleteUserById;
using Ihugi.Application.UseCases.Users.Commands.Login;
using Ihugi.Application.UseCases.Users.Commands.UpdateUserPut;
using Ihugi.Application.UseCases.Users.Queries.GetUserById;
using Ihugi.Application.UseCases.Users.Queries.GetUsers;
using Ihugi.Domain.Entities;
using Ihugi.Domain.Errors;
using Ihugi.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ihugi.Presentation.Controllers;

// TODO: XML docs
// TODO: Поменять ответы с IActionResult на ProblemDetails
// TODO: Добавить Swagger API документацию
[ApiController]
[Route("api/users")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesErrorResponseType(typeof(ProblemDetails))]
public class UsersController(ISender sender, ILogger<UsersController> logger) : ApiController(sender)
{
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        if (result.IsFailure)
        {
            var apiProblem = CreateApiProblem(result.Error, 400, "Error creating user.");
            logger.LogError("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return Created($"api/users/{result.Value!.Id}", result.Value);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(UserResponse[]), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetUsersQuery(), cancellationToken);

        if (result.IsFailure)
        {
            var apiProblem = CreateApiProblem(result.Error, 400, "Error deleting user.");
            logger.LogError("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return Ok(result.Value!.Users);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            int statusCode;
            if (result.Error == DomainErrors.User.NotFound(id))
                statusCode = 404;
            else
                statusCode = 400;

            var apiProblem = CreateApiProblem(result.Error, statusCode, "Error getting user.");
            logger.LogError("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return Ok(result.Value);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUserById(Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteUserByIdCommand(id);

        var result = await Sender.Send(request, cancellationToken);

        if (result.IsFailure && result.Error != DomainErrors.User.NoContent(id))
        {
            var apiProblem = CreateApiProblem(result.Error, 400, "Error deleting user.");
            logger.LogError("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return result.IsSuccess ? Ok() : NoContent();        
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(UpdateUserPutResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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

        if (!result.IsSuccess)
        {
            var apiProblem = CreateApiProblem(result.Error, 400, "Error updating user.");
            logger.LogError("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return Ok(result.Value);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            var apiProblem = CreateApiProblem(result.Error, 400, "Error during user log in.");
            logger.LogError("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return Ok(result.Value);
    }
}