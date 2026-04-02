using Ihugi.Common.ErrorWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ihugi.Presentation.Abstractions;

/// <summary>
/// Basic API controller class.
/// </summary>
/// <param name="sender">A sender to handle incoming requests</param>
public abstract class ApiController(ISender sender) : ControllerBase
{
    /// <summary>
    /// MediatR sender to handle incoming requests
    /// </summary>
    protected readonly ISender Sender = sender;

    protected ProblemDetails CreateApiProblem(Error error, int statusCode, string? title = null)
    {
        return new ProblemDetails
        {
            Title = title ?? error.Code,
            Detail = error.Message,
            Status = statusCode,
            Instance = HttpContext.Request.Path
        };
    }
}