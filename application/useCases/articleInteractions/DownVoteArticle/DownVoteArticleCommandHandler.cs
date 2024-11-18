using application.useCases.articleInteractions.UpvoteArticle;
using domain.entities;
using domain.interfaces;
using domain.shared;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.articleInteractions.DownVoteArticle;

public class DownVoteArticleCommandHandler : IRequestHandler<DownVoteArticleCommand, DownVoteArticleCommandResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IUserArticleVoteRepository _userArticleVoteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DownVoteArticleCommandHandler(
        IUserRepository userRepository,
        IArticleRepository articleRepository,
        IUserArticleVoteRepository userArticleVoteRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
        _userArticleVoteRepository = userArticleVoteRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<DownVoteArticleCommandResponse> Handle(DownVoteArticleCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        if (!await _articleRepository.ArticleExistsByIdIgnoreArchived(request.ArticleId))
            throw new KeyNotFoundException("Article not found");
        
        var userDownVoteRecord = UserArticleVote.CreateDownVoteRecord(request.UserId, request.ArticleId);
        
        var existingRecord = await _userArticleVoteRepository
            .GetUserArticleVoteByRecord(userDownVoteRecord);

        if (existingRecord == null)
        {
            _userArticleVoteRepository.AddUserArticleVote(userDownVoteRecord);
        }
        else
        {
            if(existingRecord.VoteType == VoteTypes.DownVote)
                throw new Exception($"User {request.UserId} already down voted");
            
            existingRecord.VoteType = VoteTypes.DownVote;
        }
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new DownVoteArticleCommandResponse
        {
            Message = "Down vote success",
        };
    }
}