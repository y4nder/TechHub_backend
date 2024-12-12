using application.useCases.tagInteractions.QueryTags.QuerySuggestedTags;
using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.SuggestedArticles;

public class SuggestedArticlesQuery : IRequest<SuggestedArticlesQueryReponse>
{
    public string SearchTerm { get; set; } = null!;
}

public class SuggestedArticlesQueryReponse
{
    public string Message { get; set; } = null!;
    public List<ArticleSuggestionDto> Articles { get; set; } = new();
}

public class SuggestedArticlesQueryHandler : IRequestHandler<SuggestedArticlesQuery, SuggestedArticlesQueryReponse>
{
    private readonly IArticleRepository _articleRepository;

    public SuggestedArticlesQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<SuggestedArticlesQueryReponse> Handle(SuggestedArticlesQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.SearchTerm))
            return new SuggestedArticlesQueryReponse
            {
                Message = $"Please enter a search term!",
            };
        
        var suggestedArticles = await _articleRepository.GetArticleSuggestions(request.SearchTerm);

        return new SuggestedArticlesQueryReponse
        {
            Message = "Suggested articles",
            Articles = suggestedArticles
        };
    }
}