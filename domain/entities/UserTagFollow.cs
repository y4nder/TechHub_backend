namespace domain.entities;

public partial class UserTagFollow
{
    public int UserId { get; set; }

    public int TagId { get; set; }

    public DateTime? FollowedDate { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public static UserTagFollow Create(int userId, int tagId)
    {
        return new UserTagFollow
        {
            UserId = userId,
            TagId = tagId,
            FollowedDate = DateTime.Now
        };
    }
}
