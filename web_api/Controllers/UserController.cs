using application.useCases.notificationInteractions.notificationQueries;
using application.useCases.userInteractions.Queries.SelfMinimalQuery;
using application.useCases.userInteractions.Queries.UserAdditionalInfoQuery;
using application.useCases.userInteractions.recommendedUsers;
using application.useCases.userInteractions.updateUserInfo;
using application.utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace web_api.Controllers;

public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/me")]
    public async Task<IActionResult> GetMe([FromQuery] SelfMinimalQuery query)
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
    
    [HttpGet("/me/profile")]
    public async Task<IActionResult> GetUserAdditionalInfo()
    {
        try
        {
            var response = await _mediator.Send(new SelfUserAdditionalInfoQuery());
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }

    [Authorize]
    [HttpPatch("/me/profile/update")]
    public async Task<IActionResult> UpdateUserAdditinalInfo([FromBody] UpdateUserCommand command)
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
    
    // [HttpGet("/me/profile/get")]
    // public async Task<IActionResult> GetUserDetails([FromQuery] SelfUserAdditionalInfoQuery query)
    // {
    //     try
    //     {
    //         var response = await _mediator.Send(query);
    //         return Ok(response);
    //     }
    //     catch (Exception ex)
    //     {
    //         return ErrorFactory.CreateErrorResponse(ex);
    //     }
    // }

    [HttpGet("/usernameChecker")]
    public async Task<IActionResult> CheckUsernameValidity([FromQuery] UsernameChecker query)
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
    
    [HttpGet("/getAllNotifications")]
    public async Task<IActionResult> CheckUsernameValidity([FromQuery] AllNotificationQuery query)
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

    [HttpGet("getRecommendedUsers")]
    public async Task<IActionResult> GetRecommendedUsers()
    {
        try
        {
            var response = await _mediator.Send(new RecommendedUsersQuery());
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }
}