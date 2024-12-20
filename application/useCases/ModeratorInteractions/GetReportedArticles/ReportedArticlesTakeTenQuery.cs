using application.useCases.ModeratorInteractions.Shared;
using domain.entities;
using domain.interfaces;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.ModeratorInteractions.GetReportedArticles;

public class ReportedArticlesTakeTenQuery : IRequest<ReportedArticlesTakeTenQueryResponse>
{
    public int ClubId { get; set; }
}

public class ReportedArticlesTakeTenQueryResponse
{
    public string Message { get; set; } = null!;
    public List<ReportedArticleDtoResponse> ReportedArticles { get; set; } = null!;
}

public class ReportedArticlesTakeTenQueryHandler : IRequestHandler<ReportedArticlesTakeTenQuery, ReportedArticlesTakeTenQueryResponse>
{
    private readonly IReportedArticleRepository _reportedArticleRepository;
    private readonly IUserContext _userContext;
    private readonly RoleChecker _roleChecker;

    public ReportedArticlesTakeTenQueryHandler(IReportedArticleRepository reportedArticleRepository, IUserContext userContext, RoleChecker roleChecker)
    {
        _reportedArticleRepository = reportedArticleRepository;
        _userContext = userContext;
        _roleChecker = roleChecker;
    }

    public async Task<ReportedArticlesTakeTenQueryResponse> Handle(ReportedArticlesTakeTenQuery request, CancellationToken cancellationToken)
    {
        int size = 10;
        var moderatorId = _userContext.GetUserId();
        
        await _roleChecker.CheckForRole(moderatorId, request.ClubId, DefaultRoles.Moderator);
        
        var reportedArticles = await _reportedArticleRepository
            .GetReportedArticlesBySize(request.ClubId, size);

        return new ReportedArticlesTakeTenQueryResponse
        {
            Message = $"Reported articles take ten, found {reportedArticles.Count} reported articles",
            ReportedArticles = reportedArticles,
        };
    }
}