using application.utilities.UserContext;
using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.DiscoverArticles;

public class DiscoverArticleQueryHandler : IRequestHandler<DiscoverArticleQuery, DiscoverArticleResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserContext _userContext;

    public DiscoverArticleQueryHandler(IArticleRepository articleRepository, IUserContext userContext)
    {
        _articleRepository = articleRepository;
        _userContext = userContext;
    }

    public async Task<DiscoverArticleResponse> Handle(DiscoverArticleQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        var articles = await _articleRepository
            .GetPaginatedDiscoverArticlesAsync(userId, request.PageNumber, request.PageSize);

        return new DiscoverArticleResponse
        {
            Message = "discover articles",
            Articles = articles
        };
    }
}