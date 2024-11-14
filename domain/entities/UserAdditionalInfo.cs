namespace domain.entities;

public partial class UserAdditionalInfo
{
    public int UserId { get; set; }

    public string UserProfilePicUrl { get; set; } = string.Empty;

    public int ReputationPoints { get; set; } = 0;

    public string? Company { get; set; }

    public string? ContactNumber { get; set; }

    public string? Job { get; set; }

    public string? GithubLink { get; set; }

    public string? LinkedInLink { get; set; }

    public string? XLink { get; set; }

    public string? PersonalWebsiteLink { get; set; }

    public string? YoutubeLink { get; set; }

    public string? StackOverflowLink { get; set; }

    public string? RedditLink { get; set; }

    public string? ThreadsLink { get; set; }

    public virtual User User { get; set; } = null!;

    public static UserAdditionalInfo Create(User user)
    {
        return new UserAdditionalInfo
        {
            UserId = user.UserId,
            User = user,
        };
    }
}
