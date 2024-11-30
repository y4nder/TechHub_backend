using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.articleInteractions.UnBookmarkArticle;

public class UnBookmarkArticleHandler : IRequestHandler<UnBookmarkArticleCommand, UnBookmarkArticleResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleBookmarkRepository _articleBookmarkRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnBookmarkArticleHandler(IUserRepository userRepository, IArticleRepository articleRepository, IArticleBookmarkRepository articleBookmarkRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
        _articleBookmarkRepository = articleBookmarkRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UnBookmarkArticleResponse> Handle(UnBookmarkArticleCommand request, CancellationToken cancellationToken)
    {
        if(! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        if(! await _articleRepository.ArticleExistsAsync(request.ArticleId))
            throw new KeyNotFoundException("Article not found");
        
        var userBookmarkRecord = UserArticleBookmark.Create(request.UserId, request.ArticleId);
        
        await _articleBookmarkRepository.RemoveBookmark(userBookmarkRecord);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new UnBookmarkArticleResponse
        {
            Message = "Unbookmarked article",
        };
    }
}