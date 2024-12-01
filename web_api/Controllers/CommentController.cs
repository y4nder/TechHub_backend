using application.useCases.commentInteractions.CreateAComment;
using application.useCases.commentInteractions.DownVoteComment;
using application.useCases.commentInteractions.QueryComments.QueryCommentsForSingleArticle;
using application.useCases.commentInteractions.QueryComments.QueryUserReplies;
using application.useCases.commentInteractions.RemoveVoteComment;
using application.useCases.commentInteractions.ReplyAComment;
using application.useCases.commentInteractions.UpvoteComment;
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

    [HttpGet("getComments")]
    public async Task<IActionResult> GetComments([FromQuery] GetArticleCommentsQuery query)
    {
        try
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }

    [HttpPost("upvoteComment")]
    public async Task<IActionResult> UpvoteComment([FromBody] UpvoteCommentCommand command)
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
    
    [HttpPost("downVoteComment")]
    public async Task<IActionResult> DownVoteComment([FromBody] DownVoteCommentCommand command)
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
    
    [HttpDelete("removeCommentVote")]
    public async Task<IActionResult> RemoveCommentVote([FromBody] RemoveVoteCommentCommand command)
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
    
    [HttpGet("getUserReplies")]
    public async Task<IActionResult> GetComments([FromQuery] UserRepliesQuery query)
    {
        try
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }
}