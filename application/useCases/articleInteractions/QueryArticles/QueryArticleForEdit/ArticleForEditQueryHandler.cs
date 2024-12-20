using domain.interfaces;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.QueryArticleForEdit;

public class ArticleForEditQueryHandler : IRequestHandler<ArticleForEditQuery, ArticleForEditResponse>
{
    private readonly IUserContext _userContext;
    private readonly IArticleRepository _articleRepository;

    public ArticleForEditQueryHandler(IUserContext userContext, IArticleRepository articleRepository)
    {
        _userContext = userContext;
        _articleRepository = articleRepository;
    }

    public async Task<ArticleForEditResponse> Handle(ArticleForEditQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        // check if user is owner of article
        if(! await _articleRepository.IsAuthor(userId, request.ArticleId))
            throw new UnauthorizedAccessException("User does not have access to edit this article");
        
        // query article
        var article = await _articleRepository.GetArticleForEditByIdAsync(request.ArticleId); 
        
        // return article
        return new ArticleForEditResponse
        {
            Message = "Article found",
            Article = article
        };
    }
}