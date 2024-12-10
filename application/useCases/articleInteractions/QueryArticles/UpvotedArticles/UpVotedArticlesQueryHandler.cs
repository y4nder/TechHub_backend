using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.UpvotedArticles;

public class UpVotedArticlesQueryHandler : IRequestHandler<UpVotedArticlesQuery, UpVotedArticlesResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;

    public UpVotedArticlesQueryHandler(IUserRepository userRepository, IArticleRepository articleRepository)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
    }

    public async Task<UpVotedArticlesResponse> Handle(UpVotedArticlesQuery request, CancellationToken cancellationToken)
    {
        if(! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        var articles = await _articleRepository
            .GetPaginatedUpVotedArticles(request.UserId, request.PageNumber, request.PageSize);

        return new UpVotedArticlesResponse
        {
            Message = "Upvoted articles",
            Articles = articles
        };
    }
}