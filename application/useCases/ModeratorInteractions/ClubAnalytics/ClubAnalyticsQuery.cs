using infrastructure.services.analytics;
using MediatR;

namespace application.useCases.ModeratorInteractions.ClubAnalytics;

public class ClubAnalyticsQuery : IRequest<ClubAnalyticsQueryReponse>
{
    public int ClubId { get; set; }
}

public class ClubAnalyticsQueryReponse
{
    public string Message { get; set; } = null!;
    public AnalyticsDto AnalyticsData { get; set; } = new();
}

public class ClubAnalyticsQueryHandler : IRequestHandler<ClubAnalyticsQuery, ClubAnalyticsQueryReponse>
{
    private readonly IAnalyticsProcessor _analyticsProcessor;

    public ClubAnalyticsQueryHandler(IAnalyticsProcessor analyticsProcessor)
    {
        _analyticsProcessor = analyticsProcessor;
    }

    public async Task<ClubAnalyticsQueryReponse> Handle(ClubAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var analytics = await _analyticsProcessor.GetAllAnalyticsForClub(request.ClubId);

        return new ClubAnalyticsQueryReponse
        {
            AnalyticsData = new AnalyticsDto
            {
                Analytics = analytics
            },
            Message = "Analytics found"
        };
    }
}

