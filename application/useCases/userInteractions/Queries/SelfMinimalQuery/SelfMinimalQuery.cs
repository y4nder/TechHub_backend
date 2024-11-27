using domain.entities;
using MediatR;

namespace application.useCases.userInteractions.Queries.SelfMinimalQuery;

public record SelfMinimalQuery(int UserId) : IRequest<UserMinimalDto>;