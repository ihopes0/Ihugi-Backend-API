using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ihugi.Presentation.Abstractions;

// TODO: XML docs
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }
}