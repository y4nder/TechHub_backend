using application.useCases.ModeratorInteractions.Shared;
using domain.entities;
using domain.interfaces;
using domain.pagination;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.ModeratorInteractions.GetReportedArticles;

public class ReportedArticlesQuery : IRequest<ReportedArticlesQueryResponse>
{
    public int ClubId { get; set; }
}

public class ReportedArticlesQueryResponse
{
    public string Message { get; set; } = null!;
    public List<ReportedArticleDtoResponse> ReportedArticles { get; set; } = new();
}

public class ReportedArticlesQueryHandler : IRequestHandler<ReportedArticlesQuery, ReportedArticlesQueryResponse>
{
    private readonly IReportedArticleRepository _reportedArticleRepository;
    private readonly IUserContext _userContext;
    private readonly RoleChecker _roleChecker;

    public ReportedArticlesQueryHandler(
        IReportedArticleRepository reportedArticleRepository, 
        RoleChecker roleChecker, 
        IUserContext userContext)
    {
        _reportedArticleRepository = reportedArticleRepository;
        _roleChecker = roleChecker;
        _userContext = userContext;
    }

    public async Task<ReportedArticlesQueryResponse> Handle(ReportedArticlesQuery request, CancellationToken cancellationToken)
    {
        var moderatorId = _userContext.GetUserId();
        
        await _roleChecker.CheckForRole(moderatorId, request.ClubId, DefaultRoles.Moderator);
        
        var reportedArticles = await _reportedArticleRepository
            .GetReportedArticlesByClubId(request.ClubId);

        return new ReportedArticlesQueryResponse
        {
            Message = "Reported articles found",
            ReportedArticles = reportedArticles
        };
    }
}