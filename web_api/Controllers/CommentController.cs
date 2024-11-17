using application.useCases.commentInteractions.CreateAComment;
using application.useCases.commentInteractions.ReplyAComment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using web_api.utils;

namespace web_api.Controllers;

public class CommentController : Controller
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("createComment")]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }
    
    [HttpPost("replyComment")]
    public async Task<IActionResult> ReplyComment([FromBody] ReplyCommentCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }
}