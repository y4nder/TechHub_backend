using application.useCases.clubInteractions.AssignModerator;
using application.useCases.clubInteractions.CreateClub;
using application.useCases.clubInteractions.JoinClub;
using application.useCases.clubInteractions.LeaveClub;
using application.useCases.clubInteractions.QueryClub.CategorizedClubsQuery;
using application.useCases.clubInteractions.QueryClub.CategoryQuery;
using application.useCases.clubInteractions.QueryClub.FeaturedClubsQuery;
using application.useCases.clubInteractions.QueryClub.JoinedClubsQuery;
using application.useCases.clubInteractions.QueryClub.SingleCategoryClubsQuery;
using application.useCases.clubInteractions.QueryClub.SingleClubQuery;
using application.useCases.clubInteractions.QueryClub.SuggestedClubs;
using application.useCases.clubInteractions.RemoveModerator;
using application.useCases.ModeratorInteractions.UpdateClub;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using web_api.utils;

namespace web_api.Controllers;

public class ClubController : Controller
{
    private readonly ISender _sender;

    public ClubController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("createClub")]
    public async Task<IActionResult> CreateClub([FromForm] CreateClubCommand command)
    {
        try
        {
            var response = await _sender.Send(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }

    [HttpPost("joinClub")]
    public async Task<IActionResult> JoinClub([FromBody] JoinClubCommand command)
    {
        try
        {
            var response = await _sender.Send(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }
    
    [HttpPost("leaveClub")]
    public async Task<IActionResult> JoinClub([FromBody] LeaveClubCommand command)
    {
        try
        {
            var response = await _sender.Send(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }

    [HttpGet("GetCategorizedClubs")]
    public async Task<IActionResult> GetCategorizedClubs()
    {
        try
        {
            var response = await _sender.Send(new CategorizedClubsQuery());
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }
    
    [HttpGet("GetFeaturedClubs")]
    public async Task<IActionResult> GetFeaturedClubs()
    {
        try
        {
            var response = await _sender.Send(new FeaturedClubsQuery());
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }
    
    [HttpGet("GetSingleClub")]
    public async Task<IActionResult> GetSingleClub([FromQuery] SingleClubQuery query)
    {
        try
        {
            var response = await _sender.Send(query);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }

    [HttpGet("GetJoinedClubs")]
    public async Task<IActionResult> GetJoinedClubs([FromQuery] JoinedClubsQuery query)
    {
        try
        {
            var response = await _sender.Send(query);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }

    [HttpGet("GetAllClubCategories")]
    public async Task<IActionResult> GetAllClubCategories()
    {
        try
        {
            var response = await _sender.Send(new ClubCategoryQuery());
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }
    
    [HttpGet("GetSingleCategoryClubs")]
    public async Task<IActionResult> GetSingleCategoryClubs([FromQuery] SingleCategoryClubsQuery query)
    {
        try
        {
            var response = await _sender.Send(query);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }

    [HttpGet("GetSuggestedClubs")]
    public async Task<IActionResult> GetSuggestedClubs([FromQuery] SuggestedClubsQuery query)
    {
        try
        {
            var response = await _sender.Send(query);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }

    [HttpPost("assignModerator")]
    public async Task<IActionResult> AssignModerator([FromBody] AssignModeratorCommand command)
    {
        try
        {
            var response = await _sender.Send(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }
    
    [HttpDelete("removeModerator")]
    public async Task<IActionResult> RemoveModerator([FromBody] RemoveModeratorCommand command)
    {
        try
        {
            var response = await _sender.Send(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }
    
    [HttpPut("updateClub")]
    public async Task<IActionResult> UpdateClub([FromBody] UpdateClubCommand command)
    {
        try
        {
            var response = await _sender.Send(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ErrorFactory.CreateErrorResponse(e);
        }
    }
    
}


