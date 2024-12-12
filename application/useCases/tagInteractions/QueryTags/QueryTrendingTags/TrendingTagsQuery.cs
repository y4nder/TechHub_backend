using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.tagInteractions.QueryTags.QueryTrendingTags;

public class TrendingTagsQuery : IRequest<TrendingTagsResponse>
{ }

public class TrendingTagsResponse
{
    public string Message { get; set; } = null!;
    public List<TrendingTagDto> Tags { get; set; } = new();
}

public class TrendingTagsQueryHandler : IRequestHandler<TrendingTagsQuery, TrendingTagsResponse>
{
    private readonly ITagRepository _tagRepository;

    public TrendingTagsQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<TrendingTagsResponse> Handle(TrendingTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetTrendingTagsAsync();

        return new TrendingTagsResponse
        {
            Message = "Trending tags",
            Tags = tags
        };
    }
}