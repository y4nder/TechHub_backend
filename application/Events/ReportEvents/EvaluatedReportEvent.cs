using application.useCases.ModeratorInteractions.UpdateArticleStatus;
using domain.entities;
using MediatR;

namespace application.Events.ReportEvents;

public class EvaluatedReportEvent : INotification
{
    public int ArticleAuthorId { get; set; }
    public int ArticleId { get; set; }
    public int EvaluationActionType { get; set; }
    public string EvaluationNotes { get; set; } = string.Empty;
    
}

public class EvaluatedReportEventHandler : INotificationHandler<EvaluatedReportEvent>
{
    private readonly IMediator _mediator;

    public EvaluatedReportEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(EvaluatedReportEvent notification, CancellationToken cancellationToken)
    {

        var action = notification.EvaluationActionType;

        switch (action)
        {
            case (int)ReportAction.NoAction:
                return;
            case (int)ReportAction.RemoveArticle:
                await _mediator.Send(new RemoveArticleCommand
                {
                    ArticleId = notification.ArticleId
                }, cancellationToken);
                return;
            case (int)ReportAction.BanUser:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
    
    
}