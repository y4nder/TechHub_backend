using Microsoft.EntityFrameworkCore;

namespace infrastructure.services.analytics;

public interface IAnalyticsProcessor
{
    Task<List<AnalyticsDetail>> GetAllAnalyticsForClub(int clubId);
}

public class AnalyticsProcessor : IAnalyticsProcessor
{
    private readonly AppDbContext _context;

    public AnalyticsProcessor(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AnalyticsDetail>> GetAllAnalyticsForClub(int clubId)
    {
        var analytics = new List<AnalyticsDetail>();
        
        var baseQuery = _context.Clubs
            .AsNoTracking()
            .Where(c => c.ClubId == clubId);
        
        // post count
        var postCount = await baseQuery
            .Select(c => c.Articles.Count).FirstOrDefaultAsync();
        analytics.Add(AnalyticsDetail.CreateInteger("Posts", postCount));
        
        // members count
        var memberCount = await baseQuery
            .Select(c => c.ClubUsers.GroupBy(cu => new {cu.ClubId, cu.UserId})
                .Count()).FirstOrDefaultAsync();
        analytics.Add(AnalyticsDetail.CreateInteger("Members", memberCount));
        
        // view count
        var viewCount = await baseQuery
            .Select(c => c.ClubViews).FirstOrDefaultAsync();
        analytics.Add(AnalyticsDetail.CreateInteger("Views", viewCount));
        
        return analytics;
    }
}