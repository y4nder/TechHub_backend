namespace domain.entities;

public partial class UserAdditionalInfo
{
    public int UserId { get; set; }
    public int ReputationPoints { get; set; } = 0;
    
    public string Bio { get; set; } = String.Empty;
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

    public void Update(UserAdditionalInfoDto dto)
    {
        //todo validate null values
        Bio = dto.Bio;
        Company = dto.Company;
        ContactNumber = dto.ContactNumber;
        Job = dto.Job;
        GithubLink = dto.GithubLink;
        LinkedInLink = dto.LinkedInLink;
        XLink = dto.XLink;
        PersonalWebsiteLink = dto.PersonalWebsiteLink;
        YoutubeLink = dto.YoutubeLink;
        StackOverflowLink = dto.StackOverflowLink;
        RedditLink = dto.RedditLink;
        ThreadsLink = dto.ThreadsLink;
    }
}

public class UserAdditionalInfoDto
{
    public string Bio { get; set; } = null!;
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
}
