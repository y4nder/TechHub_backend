using application.utilities.UserContext;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.ModeratorInteractions.UpdateArticleStatus;

public class RemoveArticleCommand : IRequest<RemoveArticleCommandCommandResponse>
{
    public int ArticleId { get; set; }
}

public class RemoveArticleCommandCommandResponse
{
    public string Message { get; set; } = null!;
}

public class UpdateArticleStatusCommandHandler : IRequestHandler<RemoveArticleCommand, RemoveArticleCommandCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IArticleRepository _articleRepository;
    private readonly IUserContext _userContext;

    public UpdateArticleStatusCommandHandler(IUnitOfWork unitOfWork, IArticleRepository articleRepository, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _articleRepository = articleRepository;
        _userContext = userContext;
    }

    public async Task<RemoveArticleCommandCommandResponse> Handle(RemoveArticleCommand request, CancellationToken cancellationToken)
    {
        var userId =  _userContext.GetUserId();
        
        var article = await _articleRepository.GetArticleByIdAsync(request.ArticleId)??
                      throw new KeyNotFoundException("Article not found");
        
        article.UpdateToRemoved();
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new RemoveArticleCommandCommandResponse
        {
            Message = $"Article {request.ArticleId} has been removed."
        };
    }
}