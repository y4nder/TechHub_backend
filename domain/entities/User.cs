using domain.shared;

namespace domain.entities;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
    
    public string UserProfilePicUrl { get; set; } = string.Empty;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<SearchHistory> SearchHistories { get; set; } = new List<SearchHistory>();

    public virtual UserAdditionalInfo? UserAdditionalInfo { get; set; }

    public virtual ICollection<UserArticleBookmark> UserArticleBookmarks { get; set; } = new List<UserArticleBookmark>();

    public virtual ICollection<UserArticleRead> UserArticleReads { get; set; } = new List<UserArticleRead>();

    public virtual ICollection<UserArticleVote> UserArticleVotes { get; set; } = new List<UserArticleVote>();

    public virtual ICollection<UserCommentVote> UserCommentVotes { get; set; } = new List<UserCommentVote>();

    public virtual ICollection<UserFollow> UserFollowFollowers { get; set; } = new List<UserFollow>();

    public virtual ICollection<UserFollow> UserFollowFollowings { get; set; } = new List<UserFollow>();

    public virtual ICollection<UserTagFollow> UserTagFollows { get; set; } = new List<UserTagFollow>();

    public ICollection<ReportedArticle> ReportedArticles { get; set; } = new List<ReportedArticle>();

    public static User Create(string username, string email, string password, string? userProfilePicUrl)
    {
        return new User
        {
            Username = username,
            Email = email,
            Password = password,
            UserProfilePicUrl = userProfilePicUrl ?? UserDefaults.DefaultProfilePictureUrl,
        };
    }

    public void ProfileUpdate(string userProfilePic, string username)
    {
        UserProfilePicUrl = Updater.UpdateProperty(UserProfilePicUrl, userProfilePic);
        Username = Updater.UpdateProperty(Username, username);
    }
}

public class UserMinimalDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string UserProfilePicUrl { get; set; }
    public int ReputationPoints { get; set; }
    public string Email { get; set; }
    public UserMinimalDto(User user)
    {
        UserId = user.UserId;
        Username = user.Username!;   
        UserProfilePicUrl = user.UserProfilePicUrl!;
        ReputationPoints = user.UserAdditionalInfo!.ReputationPoints;
        Email = user.Email!;
    }
}

public class ClubUserMinimalDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string UserProfilePicUrl { get; set; } = null!;
    public int ReputationPoints { get; set; }
    public string Email { get; set; } = null!;
    public DateTime? DateJoined { get; set; }
    public List<ClubUserRoleMinimalDto> Roles { get; set; } = new();
}

public static class UserDefaults
{
    public const string DefaultProfilePictureUrl = "NoProfilePic";
}

public class UserProfileDto
{
    public string Username { get; set; } = null!;
    public string UserProfilePicUrl { get; set; } = null!;
}

public class UserFollowsListDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string UserProfilePicUrl { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int ReputationPoints { get; set; }
    public bool Followed { get; set; }
}

public class UserDetailsDto
{
    public string UserProfilePicUrl { get; set; } = null!;
    public string Username { get; set; } = null!;
    public int ReputationPoints { get; set; }
    public UserAdditionalInfoDto UserAdditionalInfo { get; set; } = null!;
}

