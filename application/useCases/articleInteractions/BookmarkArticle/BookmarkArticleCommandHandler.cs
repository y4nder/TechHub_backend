using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.articleInteractions.BookmarkArticle;

public class BookmarkArticleCommandHandler : IRequestHandler<BookmarkArticleCommand, BookmarkArticlResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleBookmarkRepository _articleBookmarkRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookmarkArticleCommandHandler(IUserRepository userRepository, IArticleRepository articleRepository, IArticleBookmarkRepository articleBookmarkRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
        _articleBookmarkRepository = articleBookmarkRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BookmarkArticlResponse> Handle(BookmarkArticleCommand request, CancellationToken cancellationToken)
    {
        if(! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        if(! await _articleRepository.ArticleExistsAsync(request.ArticleId))
            throw new KeyNotFoundException("Article not found");
        
        var userBookmarkRecord = UserArticleBookmark.Create(request.UserId, request.ArticleId);

        if (await _articleBookmarkRepository.BookmarkExist(userBookmarkRecord))
        {
            throw new InvalidOperationException("Bookmark already exists");
        }
        
        _articleBookmarkRepository.AddBookmark(userBookmarkRecord);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new BookmarkArticlResponse
        {
            Message = "Bookmark added",
        };
    }
}