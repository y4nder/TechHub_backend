using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.articleInteractions.RemoveArticleVote;

public class RemoveArticleVoteCommandHandler : IRequestHandler<RemoveArticleVoteCommand, RemovedArticleVoteResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IUserArticleVoteRepository _userArticleVoteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveArticleVoteCommandHandler(IUserRepository userRepository, IArticleRepository articleRepository, IUserArticleVoteRepository userArticleVoteRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
        _userArticleVoteRepository = userArticleVoteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RemovedArticleVoteResponse> Handle(RemoveArticleVoteCommand request, CancellationToken cancellationToken)
    {
        if(! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        if(! await _articleRepository.ArticleExistsByIdIgnoreArchived(request.ArticleId))
            throw new KeyNotFoundException("Article not found");
        
        var record = UserArticleVote.CreateForRemovalRecord(request.UserId, request.ArticleId);
        
        await _userArticleVoteRepository.RemoveUserArticleVote(record);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new RemovedArticleVoteResponse
        {
            Message = "Removed article vote",
        };
    }
}