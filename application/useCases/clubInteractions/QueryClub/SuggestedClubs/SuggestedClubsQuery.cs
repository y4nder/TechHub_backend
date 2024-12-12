using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.SuggestedClubs;

public class SuggestedClubsQuery : IRequest<SuggestedClubsQueryResponse>
{
    public string SearchTerm { get; set; } = null!;
}

public class SuggestedClubsQueryResponse
{
    public string Message { get; set; } = null!;
    public List<SuggestedClubDto> Clubs { get; set; } = new();
}

public class SuggestedClubsQueryHandler : IRequestHandler<SuggestedClubsQuery, SuggestedClubsQueryResponse>
{
    private readonly IClubRepository _clubRepository;

    public SuggestedClubsQueryHandler(IClubRepository clubRepository)
    {
        _clubRepository = clubRepository;
    }

    public async Task<SuggestedClubsQueryResponse> Handle(SuggestedClubsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.SearchTerm))
            return new SuggestedClubsQueryResponse
            {
                Message = $"Please enter a search term!",
            };
        
        var suggestedClubs = await _clubRepository.GetSuggestedClubs(request.SearchTerm);

        return new SuggestedClubsQueryResponse
        {
            Message = "Suggested clubs",
            Clubs = suggestedClubs
        };
    }
}