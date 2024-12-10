using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.CategoryQuery;

public class ClubCategoryQueryHandler : IRequestHandler<ClubCategoryQuery, List<ClubCategoryDto>>
{
    private readonly IClubCategoryRepository _clubCategoryRepository;

    public ClubCategoryQueryHandler(IClubCategoryRepository clubCategoryRepository)
    {
        _clubCategoryRepository = clubCategoryRepository;
    }

    public async Task<List<ClubCategoryDto>> Handle(ClubCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _clubCategoryRepository.GetAllClubCategoriesAsync();
    }
}