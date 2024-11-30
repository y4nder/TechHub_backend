using application.useCases.articleInteractions.ArchiveArticle;
using application.useCases.articleInteractions.CreateArticle;
using application.useCases.articleInteractions.DownVoteArticle;
using application.useCases.articleInteractions.QueryArticles.ClubArticles;
using application.useCases.articleInteractions.QueryArticles.DiscoverArticles;
using application.useCases.articleInteractions.QueryArticles.HomeArticles;
using application.useCases.articleInteractions.QueryArticles.SearchedArticles;
using application.useCases.articleInteractions.QueryArticles.SingleArticle;
using application.useCases.articleInteractions.RemoveArticleVote;
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
    public async Task<IActionResult> AddArticle(
        [FromForm]CreateArticleCommand command
        )
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
    public async Task<IActionResult> AddArticle([FromBody] ArchiveArticleCommand command)
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
    
    [HttpGet("searchArticles")]
    public async Task<IActionResult> GetArticles([FromQuery]SearchArticlesQuery searchArticleQuery)
    {
        var articles = await _mediator.Send(searchArticleQuery);
        return Ok(articles);
    }

    [HttpDelete("removeArticleVote")]
    public async Task<IActionResult> RemoveArticleVote(RemoveArticleVoteCommand command)
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
    
    [HttpGet("discoverArticles")]
    public async Task<IActionResult> RemoveArticleVote(DiscoverArticleQuery query)
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

    [HttpGet("clubArticles")]
    public async Task<IActionResult> GetClubArticles([FromQuery] GetClubArticlesQuery query)
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
}