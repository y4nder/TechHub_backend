using domain.entities;
using infrastructure.Data.SchemaConfigurations;
using infrastructure.Data.Seeders;
using Microsoft.EntityFrameworkCore;

namespace infrastructure;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleBody> ArticleBodies { get; set; }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<ClubAdditionalInfo> ClubAdditionalInfos { get; set; }

    public virtual DbSet<ClubCategory> ClubCategories { get; set; }

    public virtual DbSet<ClubUser> ClubUsers { get; set; }

    public virtual DbSet<ClubUserRole> ClubUserRoles { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<SearchHistory> SearchHistories { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAdditionalInfo> UserAdditionalInfos { get; set; }

    public virtual DbSet<UserArticleBookmark> UserArticleBookmarks { get; set; }

    public virtual DbSet<UserArticleRead> UserArticleReads { get; set; }

    public virtual DbSet<UserArticleVote> UserArticleVotes { get; set; }

    public virtual DbSet<UserCommentVote> UserCommentVotes { get; set; }

    public virtual DbSet<UserFollow> UserFollows { get; set; }

    public virtual DbSet<UserTagFollow> UserTagFollows { get; set; }

    public virtual DbSet<ReportedArticle> ReportedArticles { get; set; }
    
    public virtual DbSet<Notification> Notifications { get; set; }
    
    public virtual DbSet<UserNotification> UserNotifications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddSchemaConfiguration();
        
        modelBuilder.Seed();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    
    public void EnsureFullTextCatalog()
    {
        using (var command = this.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = @"
                IF NOT EXISTS (SELECT * FROM sys.fulltext_catalogs WHERE name = 'ftCatalog')
                BEGIN
                    CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;
                END;";
            this.Database.OpenConnection();
            command.ExecuteNonQuery();
        }
    }
}
