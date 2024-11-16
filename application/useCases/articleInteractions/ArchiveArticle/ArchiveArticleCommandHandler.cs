using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.articleInteractions.ArchiveArticle;

public class ArchiveArticleCommandHandler : IRequestHandler<ArchiveArticleCommand, ArchiveArticleResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ArchiveArticleCommandHandler(
        IArticleRepository articleRepository,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository)
    {
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<ArchiveArticleResponse> Handle(ArchiveArticleCommand request, CancellationToken cancellationToken)
    {
        // check if author id exists
        if (! await _userRepository.CheckIdExists(request.AuthorId))
            throw new UnauthorizedAccessException("You do not have permission to access this resource.");
        
        // query article
        var article = await _articleRepository.GetArticleByIdAsync(request.ArticleId)??
                      throw new KeyNotFoundException("Article not found.");
        
        // check if author id is the author of the article
        if(article.ArticleAuthorId != request.AuthorId)
            throw new UnauthorizedAccessException("You are not the author of this article.");
        
        // check if article is already archived 
        if(article.Archived)
            throw new InvalidOperationException("The article is already archived.");
        
        // update article 
        article.Archived = true;
        
        // repo
        _articleRepository.Update(article);
        
        // unit of work
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ArchiveArticleResponse
        {
            Message = "Article Archived",
        };
    }
}