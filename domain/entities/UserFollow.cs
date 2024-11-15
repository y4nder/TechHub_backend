namespace domain.entities;

public partial class UserFollow
{
    public int FollowerId { get; set; }

    public int FollowingId { get; set; }

    public DateTime? FollowedDate { get; set; }

    public virtual User Follower { get; set; } = null!;

    public virtual User Following { get; set; } = null!;

    public static UserFollow Create(int followerId, int followingId)
    {
        return new UserFollow
        {
            FollowerId = followerId,
            FollowingId = followingId,
            FollowedDate = DateTime.Now
        };
    } 
}
