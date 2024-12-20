using application.useCases.articleInteractions.QueryArticles.ClubArticles;
using application.useCases.clubInteractions.QueryClub.ClubForEditQuery;
using application.useCases.clubInteractions.QueryClub.GetClubModerators;
using application.useCases.clubInteractions.UpdateClub;
using application.useCases.ModeratorInteractions.ClubAnalytics;
using application.useCases.ModeratorInteractions.ClubMembers;
using application.useCases.ModeratorInteractions.EvaluateArticle;
using application.useCases.ModeratorInteractions.GetReportedArticles;
using application.useCases.ModeratorInteractions.PinActions;
using application.useCases.ModeratorInteractions.UpdateArticleStatus;
using application.useCases.ModeratorInteractions.UpdateClub;
using application.useCases.ModeratorInteractions.UpdateUserRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using web_api.utils;

namespace web_api.Controllers;

public class ModeratorController : Controller
{
    private readonly ISender _sender;

    public ModeratorController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("clubAnalytics")]
    public async Task<IActionResult> ClubAnalytics([FromQuery] ClubAnalyticsQuery query)
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

    [HttpGet("getModerators")]
    public async Task<IActionResult> ClubModerators([FromQuery] ClubModeratorsQuery query)
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
    
    [HttpGet("getModeratorsFull")]
    public async Task<IActionResult> ClubModeratorsFull([FromQuery] ClubModeratorsFullQuery query)
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
    
    [HttpGet("getReportedArticles")]
    public async Task<IActionResult> GetReportedArticles([FromQuery] ReportedArticlesQuery query)
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
    
    [HttpGet("getTenReportedArticles")]
    public async Task<IActionResult> GetTenReportedArticles([FromQuery] ReportedArticlesTakeTenQuery query)
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
    
    [HttpPost("removeArticle")]
    public async Task<IActionResult> RemoveArticle([FromBody] RemoveArticleCommand command)
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
    
    [HttpPost("publishArticle")]
    public async Task<IActionResult> PublishArticle([FromBody] PublishArticleCommand command)
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
    
    [HttpGet("getClubArticlesForModerator")]
    public async Task<IActionResult> GetClubArticlesForModerator([FromQuery] GetClubArticlesForModQuery query)
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
    
    [HttpPost("setPin")]
    public async Task<IActionResult> PinArticle([FromBody] ArticlePinCommand command)
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
    
    [HttpGet("getClubMembersForModerator")]
    public async Task<IActionResult> ClubUsers([FromQuery] ClubMembersQuery query)
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
    
    [HttpPost("updateUserRoles")]
    public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
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
    
    [HttpGet("getReportsForArticle")]
    public async Task<IActionResult> ClubUsers([FromQuery] GetReportsForSingleArticleQuery query)
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
    
    [HttpPost("evaluateArticle")]
    public async Task<IActionResult> EvaluateArticle([FromBody] EvaluateArticleCommand command)
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
    
    [HttpGet("getClubForEdit")]
    public async Task<IActionResult> ClubUsers([FromQuery] ClubForEditQuery query)
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

    [HttpPost("updateClubForMod")]
    public async Task<IActionResult> UpdateClub([FromForm] UpdateClubCommandVersion2 command)
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
