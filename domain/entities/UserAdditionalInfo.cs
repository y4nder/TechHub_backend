using domain.shared;

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
        Bio = Updater.UpdateProperty(Bio, dto.Bio);
        Company = Updater.UpdateProperty(Company, dto.Company);
        ContactNumber = Updater.UpdateProperty(ContactNumber, dto.ContactNumber);
        Job = Updater.UpdateProperty(Job, dto.Job);
        GithubLink = Updater.UpdateProperty(GithubLink, dto.GithubLink);
        LinkedInLink = Updater.UpdateProperty(LinkedInLink, dto.LinkedInLink);
        XLink = Updater.UpdateProperty(XLink, dto.XLink);
        PersonalWebsiteLink = Updater.UpdateProperty(PersonalWebsiteLink, dto.PersonalWebsiteLink);
        YoutubeLink = Updater.UpdateProperty(YoutubeLink, dto.YoutubeLink);
        StackOverflowLink = Updater.UpdateProperty(StackOverflowLink, dto.StackOverflowLink);
        RedditLink = Updater.UpdateProperty(RedditLink, dto.RedditLink);
        ThreadsLink = Updater.UpdateProperty(ThreadsLink, dto.ThreadsLink);
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
