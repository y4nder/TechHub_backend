using domain.entities;
using domain.interfaces;
using domain.pagination;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.ClubArticles;

public class GetClubArticlesForModQuery : IRequest<GetClubArticlesForModQueryResponse>
{
    public int ClubId { get; set; }
    public int UserId { get; set; }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetClubArticlesForModQueryResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<ArticleResponseDto> Articles { get; set; } = null!;
}

public class GetClubArticlesForModQueryHandler : IRequestHandler<GetClubArticlesForModQuery, GetClubArticlesForModQueryResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IClubRepository _clubRepository;
    private readonly IArticleRepository _articleRepository;


    public GetClubArticlesForModQueryHandler(IUserRepository userRepository, IClubRepository clubRepository, IArticleRepository articleRepository)
    {
        _userRepository = userRepository;
        _clubRepository = clubRepository;
        _articleRepository = articleRepository;
    }

    public async Task<GetClubArticlesForModQueryResponse> Handle(GetClubArticlesForModQuery request, CancellationToken cancellationToken)
    {
        if( ! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        if( ! await _clubRepository.ClubIdExists(request.ClubId))
            throw new KeyNotFoundException("Club not found");
        
        var articles = await _articleRepository
            .GetPaginatedArticlesByClubIdForModeratorAsync(request.ClubId, request.PageNumber, request.PageSize, request.UserId);

        return new GetClubArticlesForModQueryResponse
        {
            Message = "Articles retrieved",
            Articles = articles
        };
    }
}