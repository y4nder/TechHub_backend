using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.ReadArticles;

public class UserReadArticlesQueryHandler : IRequestHandler<UserReadArticlesQuery, UserReadArticlesResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;

    public UserReadArticlesQueryHandler(IUserRepository userRepository, IArticleRepository articleRepository)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
    }

    public async Task<UserReadArticlesResponse> Handle(UserReadArticlesQuery request, CancellationToken cancellationToken)
    {
        if(! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        var articles = await _articleRepository
            .GetPaginatedReadArticles(request.UserId,request.PageNumber, request.PageSize);

        return new UserReadArticlesResponse
        {
            Message = "Success",
            Articles = articles
        };
    }
}