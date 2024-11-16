using application.useCases.tagInteractions.FollowManyTags;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using web_api.utils;

namespace web_api.Controllers;

public class TagController : Controller
{
    private readonly ISender _sender;

    public TagController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("followManyTags")]
    public async Task<IActionResult> FollowManyTags([FromBody] FollowManyTagsCommand command)
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