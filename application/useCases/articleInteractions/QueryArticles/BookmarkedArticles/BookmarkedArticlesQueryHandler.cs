using domain.interfaces;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.BookmarkedArticles;

public class BookmarkedArticlesQueryHandler : IRequestHandler<BookmarkedArticlesQuery, BookmarkedArticlesResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;

    public BookmarkedArticlesQueryHandler(IUserRepository userRepository, IArticleRepository articleRepository)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
    }

    public async Task<BookmarkedArticlesResponse> Handle(BookmarkedArticlesQuery request, CancellationToken cancellationToken)
    {
        if(! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        var articles = await _articleRepository
            .GetPaginatedBookmarkedArticles(request.UserId, request.PageNumber, request.PageSize);

        return new BookmarkedArticlesResponse
        {
            Message = "Bookmarked articles",
            Articles = articles
        };
    }
}