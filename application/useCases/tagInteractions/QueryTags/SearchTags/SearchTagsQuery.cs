using domain.entities;
using MediatR;

namespace application.useCases.tagInteractions.QueryTags.SearchTags;

public class SearchTagsQuery : IRequest<SearchTagsResponse>
{
    public string TagQuery { get; set; } = null!;
}

public class SearchTagsResponse
{
    public List<TagDto> QueriedTags { get; set; } = null!;
}