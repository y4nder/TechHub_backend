using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.SearchedArticles;

public class SearchArticlesQueryHandler : IRequestHandler<SearchArticlesQuery, SearchArticlesResponse>
{
    private readonly IArticleRepository _articleRepository;

    public SearchArticlesQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<SearchArticlesResponse> Handle(SearchArticlesQuery request, CancellationToken cancellationToken)
    {
        if(request.Query == null || string.IsNullOrWhiteSpace(request.Query))
            throw new InvalidOperationException("Query is empty");
        
        var homeArticles = await _articleRepository
            .GetPaginatedArticlesBySearchQueryAsync(
                request.Query,
                request.UserId, 
                request.PageNumber, 
                request.PageSize);
        
        return new SearchArticlesResponse
        {
            Message = "success",
            QueriedArticles = homeArticles
        };

    }
}