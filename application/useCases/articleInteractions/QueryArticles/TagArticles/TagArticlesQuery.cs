using domain.entities;
using domain.interfaces;
using domain.pagination;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.TagArticles;

public class TagArticlesQuery : IRequest<TagArticlesQueryResponse>
{
    public int TagId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class TagArticlesQueryResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<ArticleResponseDto> Articles { get; set; } = null!;
}

public class TagArticlesQueryHandler : IRequestHandler<TagArticlesQuery, TagArticlesQueryResponse>
{
    private readonly IUserContext _userContext;
    private readonly IArticleRepository _articleRepository; 
    private readonly ITagRepository _tagRepository;

    public TagArticlesQueryHandler(IUserContext userContext, IArticleRepository articleRepository, ITagRepository tagRepository)
    {
        _userContext = userContext;
        _articleRepository = articleRepository;
        _tagRepository = tagRepository;
    }

    public async Task<TagArticlesQueryResponse> Handle(TagArticlesQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        if(! await _tagRepository.TagIdExistsAsync(request.TagId))
            throw new InvalidDataException("Tag doesn't exist");
        
        var articles = await _articleRepository.GetPaginatedArticlesByTagId(userId, request.TagId, request.PageNumber, request.PageSize);

        return new TagArticlesQueryResponse
        {
            Articles = articles,
            Message = "Success"
        };
    }
}