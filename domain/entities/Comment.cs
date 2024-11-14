namespace domain.entities;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? CommentCreatorId { get; set; }

    public int? ArticleId { get; set; }

    public int? ParentCommentId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public virtual Article? Article { get; set; }

    public virtual User? CommentCreator { get; set; }

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Comment? ParentComment { get; set; }

    public virtual ICollection<UserCommentVote> UserCommentVotes { get; set; } = new List<UserCommentVote>();
}
