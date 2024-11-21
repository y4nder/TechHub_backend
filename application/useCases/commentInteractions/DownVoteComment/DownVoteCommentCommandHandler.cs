using application.useCases.commentInteractions.UpvoteComment;
using domain.entities;
using domain.interfaces;
using domain.shared;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.commentInteractions.DownVoteComment;

public class DownVoteCommentCommandHandler : IRequestHandler<DownVoteCommentCommand, DownVoteCommentResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserCommentVoteRepository _userCommentVoteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DownVoteCommentCommandHandler(
        IUserRepository userRepository,
        ICommentRepository commentRepository,
        IUserCommentVoteRepository userCommentVoteRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _userCommentVoteRepository = userCommentVoteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DownVoteCommentResponse> Handle(DownVoteCommentCommand request, CancellationToken cancellationToken)
    {
        if(! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");

        if (! await _commentRepository.CheckCommentIdExists(request.CommentId))
            throw new KeyNotFoundException("Comment not found");
        
        var commentVoteRecord = UserCommentVote.CreateDownVoteRecord(request.UserId, request.CommentId);

        var existingRecord = await _userCommentVoteRepository.GetUserCommentRecord(commentVoteRecord);

        if (existingRecord == null)
        {
            _userCommentVoteRepository.AddUserCommentVote(commentVoteRecord);
        }
        else
        {
            if (existingRecord.VoteType == VoteTypes.DownVote)
            {
                throw new InvalidOperationException("Comment is already upVoted");
            }
            existingRecord.VoteType = VoteTypes.DownVote;
        }
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new DownVoteCommentResponse
        {
            Message = "Success down vote"
        };
    }
}