using domain.entities;
using domain.pagination;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.ClubArticles;

public class GetClubArticlesQuery : IRequest<GetClubArticlesResponse>
{
    public int ClubId { get; set; }
    public int UserId { get; set; }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
}

public class GetClubArticlesResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<ArticleResponseDto> Articles { get; set; } = null!;
}