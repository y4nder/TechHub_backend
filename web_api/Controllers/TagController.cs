using application.useCases.tagInteractions.FollowManyTags;
using application.useCases.tagInteractions.FollowOneTag;
using application.useCases.tagInteractions.QueryTags;
using application.useCases.tagInteractions.QueryTags.QueryAllTags;
using application.useCases.tagInteractions.QueryTags.QuerySuggestedTags;
using application.useCases.tagInteractions.QueryTags.QueryTagPage;
using application.useCases.tagInteractions.QueryTags.QueryTrendingTags;
using application.useCases.tagInteractions.QueryTags.SearchTags;
using application.useCases.tagInteractions.UnfollowOneTag;
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
    
    
    [HttpGet("trendingTags")]
    public async Task<IActionResult> GetTrendingTags()
    {
        try
        {
            var response = await _sender.Send(new TrendingTagsQuery());
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }   
    }
    
    [HttpGet("discoverTags")]
    public async Task<IActionResult> GetDiscoverTags()
    {
        try
        {
            var response = await _sender.Send(new GroupedTagsQuery());
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }   
    }
    
    [HttpGet("tagPage")]
    public async Task<IActionResult> GetTagPage([FromQuery] TagPageQuery query)
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
    
    [HttpPost("followTag")]
    public async Task<IActionResult> FollowTag([FromBody] FollowTagCommand command)
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
    
    
    [HttpPost("unfollowTag")]
    public async Task<IActionResult> UnFollowTag([FromBody] UnfollowTagCommand command)
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
    
    [HttpGet("getSuggestedTags")]
    public async Task<IActionResult> GetSuggestedTags([FromQuery] SuggestedTagsQuery q)
    {
        try
        {
            var response = await _sender.Send(q);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }   
    }
}

