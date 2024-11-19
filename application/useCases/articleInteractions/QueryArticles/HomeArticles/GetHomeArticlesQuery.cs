using domain.entities;
using domain.pagination;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.HomeArticles;

public class GetHomeArticlesQuery : IRequest<HomeArticleResponse>
{
    public int UserId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class HomeArticleResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<HomeArticle> HomeArticles { get; set; } = null!;
}



public class HomeArticleTag(int id, string name)
{
    public int TagId { get; private set; } = id;
    public string TagName { get; private set; } = name;
}