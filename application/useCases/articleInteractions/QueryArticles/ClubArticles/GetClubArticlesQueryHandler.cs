using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.ClubArticles;

public class GetClubArticlesQueryHandler : IRequestHandler<GetClubArticlesQuery, GetClubArticlesResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IClubRepository _clubRepository;
    private readonly IArticleRepository _articleRepository;

    public GetClubArticlesQueryHandler(IUserRepository userRepository, IClubRepository clubRepository, IArticleRepository articleRepository)
    {
        _userRepository = userRepository;
        _clubRepository = clubRepository;
        _articleRepository = articleRepository;
    }

    public async Task<GetClubArticlesResponse> Handle(GetClubArticlesQuery request, CancellationToken cancellationToken)
    {
        if( ! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        if( ! await _clubRepository.ClubIdExists(request.ClubId))
            throw new KeyNotFoundException("Club not found");
        
        var articles = await _articleRepository
            .GetPaginatedArticlesByClubIdAsync(request.ClubId, request.PageNumber, request.PageSize, request.UserId);

        return new GetClubArticlesResponse
        {
            Message = "Articles retrieved",
            Articles = articles
        };
    }
}