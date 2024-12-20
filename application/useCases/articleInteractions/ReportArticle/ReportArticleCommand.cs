using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.articleInteractions.ReportArticle;

public class ReportArticleCommand : IRequest<ReportArticleCommandResponse>
{
    public int ArticleId { get; set; }
    public string Reason { get; set; } = null!;
    public string AdditionalNotes { get; set; } = "";
}

public class ReportArticleCommandResponse
{
    public string Message { get; set; } = null!;
}

public class ReportArticleCommandHandler : IRequestHandler<ReportArticleCommand, ReportArticleCommandResponse>
{
    private readonly IUserContext _userContext;
    private readonly IReportedArticleRepository _reportedArticleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReportArticleCommandHandler(IUserContext userContext, IReportedArticleRepository reportedArticleRepository, IUnitOfWork unitOfWork)
    {
        _userContext = userContext;
        _reportedArticleRepository = reportedArticleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReportArticleCommandResponse> Handle(ReportArticleCommand request, CancellationToken cancellationToken)
    {
        var reporterId = _userContext.GetUserId();
        
        if(string.IsNullOrEmpty(request.Reason))
            throw new InvalidOperationException("Invalid request reason");
        
        if(await _reportedArticleRepository.HasReportedRecord(request.ArticleId, reporterId))
            throw new InvalidOperationException("The article is already reported by this user"); 

        var reportRecord = ReportedArticle.Create(new ReportArticleDto
        {
            ReporterId = reporterId,
            ArticleId = request.ArticleId,
            ReportReason = request.Reason,
            AdditionalNotes = request.AdditionalNotes,
        });
        
        _reportedArticleRepository.AddReportedArticle(reportRecord);

        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new ReportArticleCommandResponse
        {
            Message = "Reported article successfully."
        };
    }
}