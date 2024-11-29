using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.DiscoverArticles;

public class DiscoverArticleQueryHandler : IRequestHandler<DiscoverArticleQuery, DiscoverArticleResponse>
{
    private readonly IArticleRepository _articleRepository;

    public DiscoverArticleQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<DiscoverArticleResponse> Handle(DiscoverArticleQuery request, CancellationToken cancellationToken)
    {
        var articles = await _articleRepository
            .GetPaginatedDiscoverArticlesAsync(request.PageNumber, request.PageSize);

        return new DiscoverArticleResponse
        {
            Message = "discover articles",
            Articles = articles
        };
    }
}