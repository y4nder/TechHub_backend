using domain.entities;
using domain.interfaces;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.tagInteractions.QueryTags.QueryTagPage;

public class TagPageQuery : IRequest<TagPageQueryResponse>
{
    public int TagId { get; set; }
}

public class TagPageQueryResponse
{
    public string Message { get; set; } = null!;
    public TagPageDto Tag { get; set; } = new();
}

public class TagPageQueryHandler : IRequestHandler<TagPageQuery, TagPageQueryResponse>
{
    private readonly IUserContext _userContext;
    private readonly ITagRepository _tagRepository;

    public TagPageQueryHandler(IUserContext userContext, ITagRepository tagRepository)
    {
        _userContext = userContext;
        _tagRepository = tagRepository;
    }

    public async Task<TagPageQueryResponse> Handle(TagPageQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        var tagPage = await _tagRepository.GetTagPageAsync(request.TagId, userId)??
                      throw new KeyNotFoundException("Tag does not exist");

        return new TagPageQueryResponse
        {
            Tag = tagPage,
            Message = "Tag Page"
        };
    }
}
