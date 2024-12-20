using application.Events.ReportEvents;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.ModeratorInteractions.EvaluateArticle;

public class EvaluateArticleCommand : IRequest<EvaluateArticleCommandResponse>
{
    public int ArticleId { get; set; }
    public int ArticleAuthorId { get; set; }
    public string EvaluationNotes { get; set; } = string.Empty;
    public int EvaluationActionType { get; set; }
}

public class EvaluateArticleCommandResponse
{
    public string Message { get; set; } = null!;
}

public class EvaluateArticleCommandHandler : IRequestHandler<EvaluateArticleCommand, EvaluateArticleCommandResponse>
{
    private readonly IReportedArticleRepository _reportedArticleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public EvaluateArticleCommandHandler(IReportedArticleRepository reportedArticleRepository, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _reportedArticleRepository = reportedArticleRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<EvaluateArticleCommandResponse> Handle(EvaluateArticleCommand request, CancellationToken cancellationToken)
    {
        var validEnum = ReportType.IsValidEnum(request.EvaluationActionType);   
        
        if (!validEnum) throw new InvalidOperationException("Invalid value for EvaluationActionType");
        
        // get all records of reports of the article
        var reports = await _reportedArticleRepository.GetArticleReportRecords(request.ArticleId);
        
        if(reports is null || !reports.Any())
            throw new InvalidOperationException("No article report records found");
        
        // add evaluation to each record
        foreach (var report in reports)
        {
            report.Evaluate(request.EvaluationNotes);
        }
        
        //save
        await _unitOfWork.CommitAsync(cancellationToken);

        await _mediator.Publish(new EvaluatedReportEvent
        {
            ArticleId = request.ArticleId,
            ArticleAuthorId = request.ArticleAuthorId,
            EvaluationNotes = request.EvaluationNotes,
            EvaluationActionType = request.EvaluationActionType
        }, cancellationToken);

        return new EvaluateArticleCommandResponse
        {
            Message = $"Report records for {request.ArticleId} has been evaluated."
        };
    }
    
}
