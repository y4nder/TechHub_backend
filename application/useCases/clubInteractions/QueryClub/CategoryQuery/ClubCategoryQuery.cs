using domain.entities;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.CategoryQuery;

public record ClubCategoryQuery() : IRequest<List<ClubCategoryDto>>;