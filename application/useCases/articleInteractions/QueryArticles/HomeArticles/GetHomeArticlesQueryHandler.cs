using domain.entities;
using domain.interfaces;
using domain.pagination;
using infrastructure.UserContext;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace application.useCases.articleInteractions.QueryArticles.HomeArticles;

public class GetHomeArticlesQueryHandler : IRequestHandler<GetHomeArticlesQuery, HomeArticleResponse>
{
    private readonly IUserContext _userContext;
    private readonly IArticleRepository _articleRepository;
    private readonly IUserAdditionalInfoRepository _userAdditionalInfoRepository;
    private readonly IUserTagFollowRepository _userTagFollowRepository;

    public GetHomeArticlesQueryHandler(IArticleRepository articleRepository,
                                       IUserTagFollowRepository userTagFollowRepository,
                                       IUserAdditionalInfoRepository userAdditionalInfoRepository,
                                       IUserContext userContext)
    {
        _articleRepository = articleRepository;
        _userTagFollowRepository = userTagFollowRepository;
        _userAdditionalInfoRepository = userAdditionalInfoRepository;
        _userContext = userContext;
    }

    public async Task<HomeArticleResponse> Handle(GetHomeArticlesQuery request, CancellationToken cancellationToken)
    {
        if(request.PageNumber > request.PageSize || request.PageNumber <= 0)
            throw new InvalidOperationException("Invalid page number");

        var userId = _userContext.GetUserId();

       
        var userTagFollowRecords = await _userTagFollowRepository.GetFollowedTags(userId);
        
        if(userTagFollowRecords == null || userTagFollowRecords.Count == 0)
            throw new InvalidOperationException("user doesn't have followed tags");

        var tagIds = userTagFollowRecords.Select(t => t.TagId);
        
        var paginatedArticles = await _articleRepository
            .GetPaginatedHomeArticlesByTagIdsAsync(userId, tagIds.ToList(), request.PageNumber, request.PageSize);
        
        return new HomeArticleResponse
        {
            Message = "Home articles found",
            Articles = paginatedArticles
        };
    }
}