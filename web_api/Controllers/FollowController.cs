using application.useCases.userInteractions.followUser;
using application.useCases.userInteractions.unfollowUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using web_api.utils;

namespace web_api.Controllers;

public class FollowController : Controller
{
    private readonly ISender _sender;

    public FollowController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("follow")]
    public async Task<IActionResult> Follow([FromBody] FollowUserCommand command)
    {
        try
        {
            var response = await _sender.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }
    
    [HttpPost("unfollow")]
    public async Task<IActionResult> Follow([FromBody] UnfollowUserCommand command)
    {
        try
        {
            var response = await _sender.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }
}