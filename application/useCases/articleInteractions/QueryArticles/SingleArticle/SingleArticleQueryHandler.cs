using application.Events.ArticleEvents;
using application.utilities.UserContext;
using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.SingleArticle;

public class SingleArticleQueryHandler : IRequestHandler<SingleArticleQuery, SingleQueryDto>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserContext _userContext;
    private readonly IArticleBodyRepository _articleBodyRepository;
    private readonly IUserArticleVoteRepository _userArticleVoteRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IArticleBookmarkRepository _articleBookmarkRepository;
    private readonly IUserFollowRepository _userFollowRepository;
    private readonly IMediator _mediator;

    public SingleArticleQueryHandler(
        IArticleRepository articleRepository,
        IArticleBodyRepository articleBodyRepository,
        IMediator mediator,
        IUserArticleVoteRepository userArticleVoteRepository,
        ICommentRepository commentRepository,
        IArticleBookmarkRepository articleBookmarkRepository,
        IUserContext userContext,
        IUserFollowRepository userFollowRepository)
    {
        _articleRepository = articleRepository;
        _articleBodyRepository = articleBodyRepository;

        _mediator = mediator;
        _userArticleVoteRepository = userArticleVoteRepository;
        _commentRepository = commentRepository;
        _articleBookmarkRepository = articleBookmarkRepository;
        _userContext = userContext;
        _userFollowRepository = userFollowRepository;
    }

    public async Task<SingleQueryDto> Handle(SingleArticleQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        var article = await _articleRepository.QuerySingleArticleByIdAsync(request.ArticleId)??
                      throw new KeyNotFoundException("Article not found");
        
        if(article.Archived)
            throw new InvalidOperationException("Article is archived");
        
        var articleHtmlContent = await _articleBodyRepository.GetArticleHtmlContentByIdAsync(request.ArticleId)??
                          throw new KeyNotFoundException("ArticleBody not found");
        
        int articleVoteCount = await _userArticleVoteRepository.GetArticleVoteCount(request.ArticleId);

        int commentCount = await _commentRepository.GetTotalCommentsByArticleId(request.ArticleId);

        int voteType = await _userArticleVoteRepository.GetArticleVoteType(request.ArticleId, userId);

        bool bookMarked = await _articleBookmarkRepository
            .BookmarkExist(UserArticleBookmark.Create(userId, request.ArticleId));

        bool followed = await _userFollowRepository.CheckUserFollowRecord(userId, article.ArticleAuthorId);

        bool isOwned = article.ArticleAuthorId == userId;
        
        var singleQueryDto = SingleQueryDto.Create(
            userId, 
            article, 
            articleHtmlContent, 
            articleVoteCount,
            commentCount,
            voteType,
            bookMarked,
            followed,
            isOwned
        );
        
        await _mediator.Publish(new SingleArticleQueriedEvent(userId, request.ArticleId), cancellationToken);
            
        return singleQueryDto;
    }
}