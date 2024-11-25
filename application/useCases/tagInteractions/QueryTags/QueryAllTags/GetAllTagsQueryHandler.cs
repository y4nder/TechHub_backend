using domain.interfaces;
using MediatR;

namespace application.useCases.tagInteractions.QueryTags.QueryAllTags;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, GetAllTagsResponse>
{
    private readonly ITagRepository _tagRepository;

    public GetAllTagsQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }


    public async Task<GetAllTagsResponse> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetAllTagsAsync();

        return new GetAllTagsResponse
        {
            Message = "All tags retrieved",
            Tags = tags
        };
    }
}