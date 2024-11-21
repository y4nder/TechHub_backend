using application.useCases.clubInteractions.CreateClub;
using application.useCases.clubInteractions.JoinClub;
using application.useCases.clubInteractions.QueryClub.CategorizedClubsQuery;
using application.useCases.clubInteractions.QueryClub.FeaturedClubsQuery;
using application.useCases.clubInteractions.QueryClub.SingleClubQuery;
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
    public async Task<IActionResult> JoinClub(JoinClubCommand command)
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

}


