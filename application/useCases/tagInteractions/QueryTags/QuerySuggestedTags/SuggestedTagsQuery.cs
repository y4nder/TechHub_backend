using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.tagInteractions.QueryTags.QuerySuggestedTags;

public class SuggestedTagsQuery  : IRequest<SuggestedTagsQueryResponse>
{
    public string TagSearchTerm { get; set; } = null!;
}

public class SuggestedTagsQueryResponse
{
    public string Message { get; set; } = null!;
    public List<TagDto> Tags { get; set; } = new();
}

public class SuggestedTagsQueryHandler : IRequestHandler<SuggestedTagsQuery, SuggestedTagsQueryResponse>
{
    private readonly ITagRepository _tagRepository;

    public SuggestedTagsQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<SuggestedTagsQueryResponse> Handle(SuggestedTagsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.TagSearchTerm))
        {
            return new SuggestedTagsQueryResponse
            {
                Message = "Please enter a tag search term",
            };
        }
        
        var suggestedTags = await _tagRepository.GetSuggestedTagsAsync(request.TagSearchTerm);

        return new SuggestedTagsQueryResponse
        {
            Message = "Suggested tags",
            Tags = suggestedTags
        };
    }
}