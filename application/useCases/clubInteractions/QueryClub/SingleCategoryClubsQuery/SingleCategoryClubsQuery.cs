using domain.entities;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.SingleCategoryClubsQuery;

public class SingleCategoryClubsQuery : IRequest<SingleCategoryClubStandardResponseDto>
{
    public int CategoryId { get; set; }
}