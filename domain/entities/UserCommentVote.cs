using domain.shared;

namespace domain.entities;

public partial class UserCommentVote
{
    public int UserId { get; set; }

    public int CommentId { get; set; }

    public short VoteType { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public static UserCommentVote CreateUpVoteRecord(int userId, int commentId)
    {
        return new UserCommentVote
        {
            UserId = userId,
            CommentId = commentId,
            VoteType = VoteTypes.Upvote
        };
    }
    
    public static UserCommentVote CreateDownVoteRecord(int userId, int commentId)
    {
        return new UserCommentVote
        {
            UserId = userId,
            CommentId = commentId,
            VoteType = VoteTypes.DownVote
        };
    }

    public static UserCommentVote CreateRemovalRecord(int userId, int commentId)
    {
        return new UserCommentVote
        {
            UserId = userId,
            CommentId = commentId,
            VoteType = -1,
        };
    }
}
