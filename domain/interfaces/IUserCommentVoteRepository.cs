using domain.entities;

namespace domain.interfaces;

public interface IUserCommentVoteRepository
{
    void AddUserCommentVote(UserCommentVote userCommentVote);
    
    Task<UserCommentVote?> GetUserCommentRecord(UserCommentVote userCommentVote);
}