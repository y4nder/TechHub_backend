using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class UserAdditionalInfoRepository : IUserAdditionalInfoRepository
{
    private readonly AppDbContext _context;

    public UserAdditionalInfoRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddAdditionalInfoAsync(UserAdditionalInfo userAdditionalInfo)
    {
        _context.UserAdditionalInfos.Add(userAdditionalInfo);
    }

    public async Task<UserAdditionalInfoDto?> GetAdditionalInfoAsync(int userId)
    {
        return await _context.UserAdditionalInfos
            .AsNoTracking()
            .Where(info => info.UserId == userId)
            .Select(info => new UserAdditionalInfoDto
            {
                Bio = info.Bio,
                Company = info.Company,
                ContactNumber = info.ContactNumber,
                Job = info.Job,
                GithubLink = info.GithubLink,
                LinkedInLink = info.LinkedInLink,
                XLink = info.XLink,
                PersonalWebsiteLink = info.PersonalWebsiteLink,
                YoutubeLink = info.YoutubeLink,
                StackOverflowLink = info.StackOverflowLink,
                RedditLink = info.RedditLink,
                ThreadsLink = info.ThreadsLink
            }).FirstOrDefaultAsync();
    }

    public async Task<UserAdditionalInfo?> GetUserAdditionalInfoForUpdateAsync(int userId)
    {
        return await _context.UserAdditionalInfos.FindAsync(userId);
    }

    public void UpdateAdditionalInfo(UserAdditionalInfo userAdditionalInfo)
    {
        _context.UserAdditionalInfos.Update(userAdditionalInfo);
    }
}