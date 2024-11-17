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
        
        var userAdditionalInfo = await _userAdditionalInfoRepository.GetAdditionalInfoAsync(request.UserId)??
                                 throw new KeyNotFoundException("You do not have permission to access the resource");
        
        var userTagFollowRecords = await _userTagFollowRepository.GetFollowedTags(request.UserId);
        
        if(userTagFollowRecords == null || userTagFollowRecords.Count == 0)
            throw new InvalidOperationException("user doesn't have followed tags");
        
        var tagIds = userTagFollowRecords.Select(t => t.TagId).ToList();
        
        var paginatedArticles = await _articleRepository
            .GetPaginatedArticlesByTagIdsAsync(tagIds, request.PageNumber, request.PageSize);
        
        var dtoPaginatedArticles = PaginatedResultMapper.Map<Article, HomeArticle>(paginatedArticles, 
            article => new HomeArticle
            {
                ArticleId = article.ArticleId,
                ClubImageUrl = article.Club!.ClubImageUrl!,
                UserImageUrl = userAdditionalInfo.UserProfilePicUrl,
                ArticleTitle = article.ArticleTitle,
                Tags = article.Tags.Select(t => new HomeArticleTag(t.TagId, t.TagName)).ToList(),
                CreatedDateTime = article.CreatedDateTime,
                ArticleThumbnailUrl = article.ArticleThumbnailUrl!
            });
        
        return new HomeArticleResponse
        {
            Message = "Home articles found",
            HomeArticles = dtoPaginatedArticles
        };
    }
}