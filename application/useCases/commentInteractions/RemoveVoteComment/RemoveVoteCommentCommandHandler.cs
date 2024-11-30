using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.commentInteractions.RemoveVoteComment;

public class RemoveVoteCommentCommandHandler : IRequestHandler<RemoveVoteCommentCommand, RemoveVoteResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserCommentVoteRepository _userCommentVoteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveVoteCommentCommandHandler(IUserRepository userRepository, ICommentRepository commentRepository, IUserCommentVoteRepository userCommentVoteRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _userCommentVoteRepository = userCommentVoteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RemoveVoteResponse> Handle(RemoveVoteCommentCommand request, CancellationToken cancellationToken)
    {
        if(!await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        if(! await _commentRepository.CheckCommentIdExists(request.CommentId))
            throw new KeyNotFoundException("Comment not found");
        
        var record = UserCommentVote.CreateRemovalRecord(request.UserId, request.CommentId);

        await _userCommentVoteRepository.RemoveUserCommentVote(record);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new RemoveVoteResponse
        {
            Message = "Removed vote comment",
        };
    }
}