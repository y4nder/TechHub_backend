using application.useCases.clubInteractions.CreateClub;
using application.useCases.clubInteractions.JoinClub;
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
    public async Task<IActionResult> CreateClub(CreateClubCommand command)
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
}


