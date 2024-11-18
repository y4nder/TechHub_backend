using application.useCases.articleInteractions.ArchiveArticle;
using application.useCases.articleInteractions.CreateArticle;
using application.useCases.articleInteractions.DownVoteArticle;
using application.useCases.articleInteractions.QueryArticles.HomeArticles;
using application.useCases.articleInteractions.QueryArticles.SingleArticle;
using application.useCases.articleInteractions.UpvoteArticle;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using web_api.utils;

namespace web_api.Controllers;

public class ArticleController : Controller
{
    private readonly IMediator _mediator;

    public ArticleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("addArticle")]
    public async Task<IActionResult> AddArticle(CreateArticleCommand command)
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
    
    [HttpPost("archiveArticle")]
    public async Task<IActionResult> AddArticle(ArchiveArticleCommand command)
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

    [HttpGet("getHomeArticles")]
    public async Task<IActionResult> GetHomeArticles([FromQuery] GetHomeArticlesQuery query)
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
    
    [HttpGet("getSingleArticle")]
    public async Task<IActionResult> GetSingleArticle([FromQuery] SingleArticleQuery query)
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

    [HttpPost("upvoteArticle")]
    public async Task<IActionResult> UpVoteArticle(UpVoteArticleCommand command)
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
    
    [HttpPost("downVoteArticle")]
    public async Task<IActionResult> DownVoteArticle(DownVoteArticleCommand command)
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
}