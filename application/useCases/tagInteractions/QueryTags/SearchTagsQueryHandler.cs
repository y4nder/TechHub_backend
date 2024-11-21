using domain.interfaces;
using MediatR;

namespace application.useCases.tagInteractions.QueryTags;

public class SearchTagsQueryHandler : IRequestHandler<SearchTagsQuery, SearchTagsResponse>
{
    private readonly ITagRepository _tagRepository;

    public SearchTagsQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<SearchTagsResponse> Handle(SearchTagsQuery request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(request.TagQuery))
            throw new InvalidOperationException("Tag query is empty");
        
        var tags = await _tagRepository.GetTagsByQueryAsync(request.TagQuery);

        return new SearchTagsResponse
        {
            QueriedTags = tags
        };
    }
}