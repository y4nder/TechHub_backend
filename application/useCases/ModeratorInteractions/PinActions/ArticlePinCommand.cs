using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.ModeratorInteractions.PinActions;

public class ArticlePinCommand : IRequest<ArticlePinCommandResponse>
{
    public int ArticleId { get; set; }
    public bool Pinned { get; set; }
}

public class ArticlePinCommandResponse
{
    public string Message { get; set; } = null!;
    public bool Pinned { get; set; }
}

public class ArticlePinCommandHandler : IRequestHandler<ArticlePinCommand, ArticlePinCommandResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ArticlePinCommandHandler(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
    {
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ArticlePinCommandResponse> Handle(ArticlePinCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetArticleByIdAsync(request.ArticleId)??
                      throw new KeyNotFoundException("Article not found");
        
        article.UpdatePin(request.Pinned);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new ArticlePinCommandResponse { Message = $"Pinned: {request.Pinned}" , Pinned = request.Pinned };
    }
}