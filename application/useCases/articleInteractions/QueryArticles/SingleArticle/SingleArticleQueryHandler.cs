using application.Events.ArticleEvents;
using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.SingleArticle;

public class SingleArticleQueryHandler : IRequestHandler<SingleArticleQuery, SingleQueryDto>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IArticleBodyRepository _articleBodyRepository;
    private readonly IUserArticleVoteRepository _userArticleVoteRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IArticleBookmarkRepository _articleBookmarkRepository;
    private readonly IMediator _mediator; 

    public SingleArticleQueryHandler(
        IArticleRepository articleRepository,
        IArticleBodyRepository articleBodyRepository,
        IUserRepository userRepository,
        IMediator mediator, IUserArticleVoteRepository userArticleVoteRepository, ICommentRepository commentRepository, IArticleBookmarkRepository articleBookmarkRepository)
    {
        _articleRepository = articleRepository;
        _articleBodyRepository = articleBodyRepository;
        _userRepository = userRepository;
        _mediator = mediator;
        _userArticleVoteRepository = userArticleVoteRepository;
        _commentRepository = commentRepository;
        _articleBookmarkRepository = articleBookmarkRepository;
    }

    public async Task<SingleQueryDto> Handle(SingleArticleQuery request, CancellationToken cancellationToken)
    {
        if( ! await _userRepository.CheckIdExists(request.UserId))
            throw new UnauthorizedAccessException("Invalid user id");
        
        var article = await _articleRepository.QuerySingleArticleByIdAsync(request.ArticleId)??
                      throw new KeyNotFoundException("Article not found");
        
        if(article.Archived)
            throw new InvalidOperationException("Article is archived");
        
        var articleBody = await _articleBodyRepository.GetArticleBodyByIdAsync(request.ArticleId)??
                          throw new KeyNotFoundException("ArticleBody not found");
        
        int articleVoteCount = await _userArticleVoteRepository.GetArticleVoteCount(request.ArticleId);

        int commentCount = await _commentRepository.GetTotalCommentsByArticleId(request.ArticleId);

        int voteType = await _userArticleVoteRepository.GetArticleVoteType(request.ArticleId);

        bool bookMarked = await _articleBookmarkRepository
            .BookmarkExist(UserArticleBookmark.Create(request.UserId, request.ArticleId));
        
        var singleQueryDto = SingleQueryDto.Create(request.UserId, 
            article, 
            articleBody, 
            articleVoteCount,
            commentCount,
            voteType,
            bookMarked
        );
        
        await _mediator.Publish(new SingleArticleQueriedEvent(request.UserId, request.ArticleId), cancellationToken);
            
        return singleQueryDto;
    }
}