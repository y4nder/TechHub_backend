using domain.entities;
using MediatR;

namespace application.useCases.tagInteractions.QueryTags.QueryAllTags;

public record GetAllTagsQuery() : IRequest<GetAllTagsResponse>;

public class GetAllTagsResponse
{
    public string Message { get; set; } = null!;
    public List<TagDto> Tags { get; set; } = null!;
}