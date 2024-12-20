using domain.entities;
using domain.interfaces;
using domain.shared;
using infrastructure.services.Notification;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.articleInteractions.UpvoteArticle;

public class UpVoteArticleCommandHandler : IRequestHandler<UpVoteArticleCommand, UpVoteArticleResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IUserArticleVoteRepository _userArticleVoteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly NotificationService _notificationService;

    public UpVoteArticleCommandHandler(
        IUserRepository userRepository,
        IArticleRepository articleRepository,
        IUserArticleVoteRepository userArticleVoteRepository,
        IUnitOfWork unitOfWork, NotificationService notificationService)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
        _userArticleVoteRepository = userArticleVoteRepository;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }


    public async Task<UpVoteArticleResponse> Handle(UpVoteArticleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId)??
            throw new KeyNotFoundException("User not found");
        
        var article =  await _articleRepository.GetArticleByIdNoTracking(request.ArticleId) ??
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
        
        await _notificationService.NotifyUser(article.ArticleAuthorId, $"{user.Username} upvoted your article");

        return new UpVoteArticleResponse
        {
            Message = "Upvote success",
        };
    }
}