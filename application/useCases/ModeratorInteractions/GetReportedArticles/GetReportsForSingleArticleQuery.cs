using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.ModeratorInteractions.GetReportedArticles;

public class GetReportsForSingleArticleQuery : IRequest<List<ReportResponseMinimal>>
{
    public int ArticleId { get; set; }
}

public class GetReportsForSingleArticleQueryHandler : IRequestHandler<GetReportsForSingleArticleQuery, List<ReportResponseMinimal>>
{
    private readonly IReportedArticleRepository _reportedArticleRepository;

    public GetReportsForSingleArticleQueryHandler(IReportedArticleRepository reportedArticleRepository)
    {
        _reportedArticleRepository = reportedArticleRepository;
    }

    public async Task<List<ReportResponseMinimal>> Handle(GetReportsForSingleArticleQuery request, CancellationToken cancellationToken)
    {
        var reports = await _reportedArticleRepository.GetReportsForArticle(request.ArticleId);
        return reports;
    }
}