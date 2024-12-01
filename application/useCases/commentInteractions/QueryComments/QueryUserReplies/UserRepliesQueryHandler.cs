using domain.interfaces;
using MediatR;

namespace application.useCases.commentInteractions.QueryComments.QueryUserReplies;

public class UserRepliesQueryHandler : IRequestHandler<UserRepliesQuery, UserRepliesResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;

    public UserRepliesQueryHandler(IUserRepository userRepository, ICommentRepository commentRepository)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
    }

    public async Task<UserRepliesResponse> Handle(UserRepliesQuery request, CancellationToken cancellationToken)
    {
        if(!await  _userRepository.CheckIdExists(request.UserId)) 
            throw new KeyNotFoundException("User not found");
        
        var replies = await _commentRepository.GetUserReplies(request.UserId, request.PageNumber, request.PageSize);

        return new UserRepliesResponse
        {
            Message = "User replies fetched",
            Replies = replies
        };
    }
}