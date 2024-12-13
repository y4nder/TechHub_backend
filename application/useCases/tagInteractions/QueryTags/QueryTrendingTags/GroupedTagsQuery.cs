using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.tagInteractions.QueryTags.QueryTrendingTags;

public record GroupedTagsQuery() : IRequest<GroupedTagsQueryResponse>;

public class GroupedTagsQueryResponse
{
    public string Message { get; set; } = null!;
    public List<GroupedTagList> GroupedTags { get; set; } = new();
}



public class GroupedTagsQueryHandler : IRequestHandler<GroupedTagsQuery, GroupedTagsQueryResponse>
{
    private readonly ITagRepository _tagRepository;

    public GroupedTagsQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<GroupedTagsQueryResponse> Handle(GroupedTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetGroupedTagsAsync();

        return new GroupedTagsQueryResponse
        {
            GroupedTags = tags,
            Message = "Grouped tags retrieved"
        };
    }
}