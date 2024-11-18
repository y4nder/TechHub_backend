namespace domain.entities;

public partial class UserCommentVote
{
    public int UserId { get; set; }

    public int CommentId { get; set; }

    public short VoteType { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
