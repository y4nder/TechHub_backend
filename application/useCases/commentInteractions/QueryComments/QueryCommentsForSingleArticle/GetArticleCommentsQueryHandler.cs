using domain.interfaces;
using MediatR;

namespace application.useCases.commentInteractions.QueryComments.QueryCommentsForSingleArticle;

public class GetArticleCommentsQueryHandler : IRequestHandler<GetArticleCommentsQuery, ArticleCommentsResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICommentRepository _commentRepository;

    public GetArticleCommentsQueryHandler(
        IArticleRepository articleRepository, 
        ICommentRepository commentRepository)
    {
        _articleRepository = articleRepository;
        _commentRepository = commentRepository;
    }

    public async Task<ArticleCommentsResponse> Handle(GetArticleCommentsQuery request, CancellationToken cancellationToken)
    {
        if (! await _articleRepository.ArticleExistsByIdIgnoreArchived(request.ArticleId))
            throw new KeyNotFoundException("Article does not exist");
        
        if(request.PageNumber <= 0 || request.PageSize <= 0 || request.PageNumber >= request.PageSize)
            throw new InvalidOperationException("Invalid page number or page size");
        
        var comments = await _commentRepository
            .GetPaginatedCommentsByArticleId(request.UserId, request.ArticleId, request.PageNumber, request.PageSize);
        
        
        
        return new ArticleCommentsResponse
        {
            Message = "Success",
            Comments = comments
        };
    }
}