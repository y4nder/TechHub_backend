using domain.entities;
using domain.interfaces;
using domain.shared;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.articleInteractions.UpvoteArticle;

public class UpVoteArticleCommandHandler : IRequestHandler<UpVoteArticleCommand, UpVoteArticleResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IUserArticleVoteRepository _userArticleVoteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpVoteArticleCommandHandler(
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


    public async Task<UpVoteArticleResponse> Handle(UpVoteArticleCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        if (!await _articleRepository.ArticleExistsByIdIgnoreArchived(request.ArticleId))
            throw new KeyNotFoundException("Article not found");
        
        var userUpvoteRecord = UserArticleVote.CreateUpvoteRecord(request.UserId, request.ArticleId);
        
        var existingRecord = await _userArticleVoteRepository
            .GetUserArticleVoteByRecord(userUpvoteRecord);

        if (existingRecord == null)
        {
            _userArticleVoteRepository.AddUserArticleVote(userUpvoteRecord);
        }
        else
        {
            if(existingRecord.VoteType == VoteTypes.Upvote)
                throw new Exception($"User {request.UserId} already upVoted");
            
            existingRecord.VoteType = VoteTypes.Upvote;
        }
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new UpVoteArticleResponse
        {
            Message = "Upvote success",
        };
    }
}