using application.useCases.tagInteractions.FollowManyTags;
using application.useCases.tagInteractions.QueryTags.QueryAllTags;
using application.useCases.tagInteractions.QueryTags.SearchTags;
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

    [HttpGet("tags")]
    public async Task<IActionResult> GetTags([FromQuery] SearchTagsQuery query)
    {
        try
        {
            var response = await _sender.Send(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }

    [HttpGet("allTags")]
    public async Task<IActionResult> GetAllTags()
    {
        try
        {
            var response = await _sender.Send(new GetAllTagsQuery());
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }   
    }
}