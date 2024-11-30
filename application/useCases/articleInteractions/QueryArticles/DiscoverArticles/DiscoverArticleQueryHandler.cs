using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.DiscoverArticles;

public class DiscoverArticleQueryHandler : IRequestHandler<DiscoverArticleQuery, DiscoverArticleResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserRepository _userRepository;

    public DiscoverArticleQueryHandler(IArticleRepository articleRepository, IUserRepository userRepository)
    {
        _articleRepository = articleRepository;
        _userRepository = userRepository;
    }

    public async Task<DiscoverArticleResponse> Handle(DiscoverArticleQuery request, CancellationToken cancellationToken)
    {
        if(! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        var articles = await _articleRepository
            .GetPaginatedDiscoverArticlesAsync(request.UserId, request.PageNumber, request.PageSize);

        return new DiscoverArticleResponse
        {
            Message = "discover articles",
            Articles = articles
        };
    }
}