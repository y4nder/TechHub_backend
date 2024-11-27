using domain.entities;
using domain.interfaces;
using domain.pagination;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.HomeArticles;

public class GetHomeArticlesQueryHandler : IRequestHandler<GetHomeArticlesQuery, HomeArticleResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserAdditionalInfoRepository _userAdditionalInfoRepository;
    private readonly IUserTagFollowRepository _userTagFollowRepository;

    public GetHomeArticlesQueryHandler(
        IArticleRepository articleRepository,
        IUserRepository userRepository,
        IUserTagFollowRepository userTagFollowRepository, IUserAdditionalInfoRepository userAdditionalInfoRepository)
    {
        _articleRepository = articleRepository;
        _userRepository = userRepository;
        _userTagFollowRepository = userTagFollowRepository;
        _userAdditionalInfoRepository = userAdditionalInfoRepository;
    }

    public async Task<HomeArticleResponse> Handle(GetHomeArticlesQuery request, CancellationToken cancellationToken)
    {
        if(request.PageNumber > request.PageSize || request.PageNumber <= 0)
            throw new InvalidOperationException("Invalid page number");
        
        if(! await _userRepository.CheckIdExists(request.UserId))
            throw new UnauthorizedAccessException("You do not have permission to access the resource");
        
        var userTagFollowRecords = await _userTagFollowRepository.GetFollowedTags(request.UserId);
        
        if(userTagFollowRecords == null || userTagFollowRecords.Count == 0)
            throw new InvalidOperationException("user doesn't have followed tags");

        var tagIds = userTagFollowRecords.Select(t => t.TagId);
        
        var paginatedArticles = await _articleRepository
            .GetPaginatedHomeArticlesByTagIdsAsync(request.UserId, tagIds.ToList(), request.PageNumber, request.PageSize);
        
        return new HomeArticleResponse
        {
            Message = "Home articles found",
            HomeArticles = paginatedArticles
        };
    }
}