using domain.interfaces;
using infrastructure.services.worker;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.ModeratorInteractions.UpdateArticleStatus;

public class PublishArticleCommand : IRequest<PublishArticleCommandResponse>
{
    public int ArticleId { get; set; }
}

public class PublishArticleCommandResponse
{
    public string Message { get; set; } = null!;

}

public class PublishArticleCommandHandler : IRequestHandler<PublishArticleCommand, PublishArticleCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IArticleRepository _articleRepository;
    private readonly IUserContext _userContext;


    public PublishArticleCommandHandler(IUnitOfWork unitOfWork, IArticleRepository articleRepository, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _articleRepository = articleRepository;
        _userContext = userContext;
    }

    public async Task<PublishArticleCommandResponse> Handle(PublishArticleCommand request, CancellationToken cancellationToken)
    {
        var userId =  _userContext.GetUserId();
        
        var article = await _articleRepository.GetArticleByIdAsync(request.ArticleId)??
                      throw new KeyNotFoundException("Article not found");
        
        article.UpdateToPublished();
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new PublishArticleCommandResponse
        {
            Message = $"Article {request.ArticleId} has been published."
        };
    }
}