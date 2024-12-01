using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.UserArticles;

public class GetUserArticlesQueryHandler : IRequestHandler<GetUserArticlesQuery, GetUserArticlesResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository; 

    public GetUserArticlesQueryHandler(IUserRepository userRepository, IArticleRepository articleRepository)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
    }

    public async Task<GetUserArticlesResponse> Handle(GetUserArticlesQuery request, CancellationToken cancellationToken)
    {
        if(! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        var articles = await _articleRepository
            .GetPaginatedArticlesByUserId(request.UserId, request.PageNumber, request.PageSize);

        return new GetUserArticlesResponse
        {
            Message = "Success",
            Articles = articles
        };
    }
}