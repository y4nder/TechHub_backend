using MediatR;

namespace application.useCases.tagInteractions.UnfollowOneTag;

public class UnfollowTagCommand : IRequest<UnfollowTagResponse>
{
    public int TagId { get; set; }
}

public class UnfollowTagResponse
{
    public string Message { get; set; } = null!;
}