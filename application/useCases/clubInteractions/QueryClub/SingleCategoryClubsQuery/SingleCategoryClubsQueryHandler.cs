using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.SingleCategoryClubsQuery;

public class SingleCategoryClubsQueryHandler : IRequestHandler<SingleCategoryClubsQuery, SingleCategoryClubStandardResponseDto>
{
    private readonly IClubCategoryRepository _clubCategoryRepository;
    private readonly IClubRepository _clubRepository;

    public SingleCategoryClubsQueryHandler(IClubCategoryRepository clubCategoryRepository, IClubRepository clubRepository)
    {
        _clubCategoryRepository = clubCategoryRepository;
        _clubRepository = clubRepository;
    }

    public async Task<SingleCategoryClubStandardResponseDto> Handle(SingleCategoryClubsQuery request, CancellationToken cancellationToken)
    {
        if (!await _clubCategoryRepository.CheckClubCategoryExists(request.CategoryId))
        {
            throw new KeyNotFoundException("Category does not exist");
        }
        
        var categoryClub = await _clubRepository.GetSingleCategoryClubByIdAsync(request.CategoryId);

        return categoryClub!;
    }
}