namespace domain.entities;

public partial class Comment
{
    public int CommentId { get; set; }

    public int CommentCreatorId { get; set; }

    public int ArticleId { get; set; }

    public int? ParentCommentId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User CommentCreator { get; set; } = null!;

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Comment? ParentComment { get; set; }

    public virtual ICollection<UserCommentVote> UserCommentVotes { get; set; } = new List<UserCommentVote>();

    public static Comment Create(CommentDto dto)
    {
        return new Comment
        {
            CommentCreatorId = dto.CommentCreatorId,
            ArticleId = dto.ArticleId,
            ParentCommentId = dto.ParentCommentId,
            Content = dto.Content,
            CreatedDateTime = DateTime.Now,
            UpdateDateTime = DateTime.Now
        };
    }
}

public class CommentDto
{
    public int CommentCreatorId { get; set; }
    public int ArticleId { get; set; }
    public int? ParentCommentId { get; set; }
    public string Content { get; set; } = null!;
}
