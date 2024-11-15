using domain.entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data.SchemaConfigurations;

public static class SchemaConfiguration
{
    public static void AddSchemaConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__Article__75D3D37EA733C6CA");
            

            entity.ToTable("Article");

            entity.Property(e => e.ArticleId).HasColumnName("articleId");
            entity.Property(e => e.ArticleAuthorId).HasColumnName("article_author_id");
            entity.Property(e => e.ArticleThumbnailUrl)
                .HasMaxLength(255)
                .HasColumnName("articleThumbnailUrl");
            entity.Property(e => e.ClubId).HasColumnName("clubId");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createdDateTime");
            entity.Property(e => e.IsDrafted)
                .HasDefaultValue(true)
                .HasColumnName("isDrafted");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UpdateDateTime)
                .HasColumnType("datetime")
                .HasColumnName("updateDateTime");

            entity.HasOne(d => d.ArticleAuthor).WithMany(p => p.Articles)
                .HasForeignKey(d => d.ArticleAuthorId)
                .HasConstraintName("FK__Article__article__6D0D32F4");

            entity.HasOne(d => d.Club).WithMany(p => p.Articles)
                .HasForeignKey(d => d.ClubId)
                .HasConstraintName("FK__Article__clubId__6E01572D");
        });

        modelBuilder.Entity<ArticleBody>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ArticleBody");

            entity.Property(e => e.ArticleContent).HasColumnName("articleContent");
            entity.Property(e => e.ArticleId).HasColumnName("articleId");

            entity.HasOne(d => d.Article).WithMany()
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("FK__ArticleBo__artic__6EF57B66");
        });

        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.ClubId).HasName("PK__Club__DF4AEAB285DE4451");

            entity.ToTable("Club");

            entity.Property(e => e.ClubId).HasColumnName("clubId");
            entity.Property(e => e.ClubCategoryId).HasColumnName("clubCategoryId");
            entity.Property(e => e.ClubCreatorId).HasColumnName("clubCreatorId");
            entity.Property(e => e.ClubImageUrl)
                .HasMaxLength(255)
                .HasColumnName("clubImageUrl");
            entity.Property(e => e.ClubIntroduction)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("clubIntroduction");
            entity.Property(e => e.ClubName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("clubName");
            entity.Property(e => e.InvitePermission)
                .HasDefaultValue((short)0)
                .HasColumnName("invitePermission");
            entity.Property(e => e.PostPermission)
                .HasDefaultValue((short)0)
                .HasColumnName("postPermission");

            entity.HasOne(d => d.ClubCategory).WithMany(p => p.Clubs)
                .HasForeignKey(d => d.ClubCategoryId)
                .HasConstraintName("FK__Club__clubCatego__6383C8BA")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.ClubCreator).WithMany(p => p.Clubs)
                .HasForeignKey(d => d.ClubCreatorId)
                .HasConstraintName("FK__Club__clubCreato__628FA481")
                .OnDelete(DeleteBehavior.Cascade);;
        });

        modelBuilder.Entity<ClubAdditionalInfo>(entity =>
        {
            entity
                .HasKey(e => e.ClubId)
                .HasName("PK__ClubAdditionalInfo");
            
            entity.ToTable("ClubAdditionalInfo");

            entity.Property(e => e.ClubCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("clubCreatedDate");
            entity.Property(e => e.ClubDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("clubDescription");
            entity.Property(e => e.ClubId).HasColumnName("clubId");

            entity.HasOne(d => d.Club).WithMany()
                .HasForeignKey(d => d.ClubId)
                .HasConstraintName("FK__ClubAddit__clubI__6754599E");
        });

        modelBuilder.Entity<ClubCategory>(entity =>
        {
            entity.HasKey(e => e.ClubCategoryId).HasName("PK__ClubCate__008AAC5D82704D6C");

            entity.ToTable("ClubCategory");

            entity.Property(e => e.ClubCategoryId).HasColumnName("clubCategoryId");
            entity.Property(e => e.ClubCategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("clubCategoryName");
        });

        modelBuilder.Entity<ClubUser>(entity =>
        {
            entity.HasKey(e => new {e.ClubId, e.UserId, e.RoleId}).HasName("PK_ClubUserRole");

            entity.ToTable("ClubUser");

            entity.Property(e => e.ClubId).HasColumnName("clubId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.RoleId).HasColumnName("roleId");

            entity.HasOne(d => d.Club).WithMany(p => p.ClubUsers)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ClubUser__clubId__6477ECF3");

            entity.HasOne(d => d.Role).WithMany(p => p.ClubUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__ClubUser__roleId__66603565");

            entity.HasOne(d => d.User).WithMany(p => p.ClubUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK__ClubUser__userId__656C112C");
        });

        modelBuilder.Entity<ClubUserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__ClubUser__CD98462A79796A77");

            entity.ToTable("ClubUserRole");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__CDDE919D08B27851");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("commentId");
            entity.Property(e => e.ArticleId).HasColumnName("articleId");
            entity.Property(e => e.CommentCreatorId).HasColumnName("commentCreatorId");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createdDateTime");
            entity.Property(e => e.ParentCommentId).HasColumnName("parentCommentId");
            entity.Property(e => e.UpdateDateTime)
                .HasColumnType("datetime")
                .HasColumnName("updateDateTime");

            entity.HasOne(d => d.Article).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("FK__Comment__article__693CA210");

            entity.HasOne(d => d.CommentCreator).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentCreatorId)
                .HasConstraintName("FK__Comment__comment__68487DD7");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK__Comment__parentC__6A30C649");
        });

        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity.HasKey(e => e.SearchId).HasName("PK__SearchHi__33FFD956DA1979CE");

            entity.ToTable("SearchHistory");

            entity.Property(e => e.SearchId).HasColumnName("searchId");
            entity.Property(e => e.SearchQuery)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("searchQuery");
            entity.Property(e => e.SearchedDate)
                .HasColumnType("datetime")
                .HasColumnName("searchedDate");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.SearchHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__SearchHis__userI__619B8048");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tag__50FC01574E1148B3");

            entity.ToTable("Tag");

            entity.Property(e => e.TagId).HasColumnName("tagId");
            entity.Property(e => e.TagCount)
                .HasDefaultValue(0)
                .HasColumnName("tagCount");
            entity.Property(e => e.TagName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tagName");

            entity.HasMany(d => d.Articles).WithMany(p => p.Tags)
                .UsingEntity<Dictionary<string, object>>(
                    "ArticleTag",
                    r => r.HasOne<Article>().WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArticleTa__artic__76969D2E"),
                    l => l.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArticleTa__tagId__75A278F5"),
                    j =>
                    {
                        j.HasKey("TagId", "ArticleId").HasName("PK__ArticleT__A7A13C6086DD3216");
                        j.ToTable("ArticleTag");
                        j.IndexerProperty<int>("TagId").HasColumnName("tagId");
                        j.IndexerProperty<int>("ArticleId").HasColumnName("articleId");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFFAC1B85EF");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserAdditionalInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserAddi__CB9A1CFF5B46A646");

            entity.ToTable("UserAdditionalInfo");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("contactNumber");
            entity.Property(e => e.GithubLink)
                .HasMaxLength(255)
                .HasColumnName("githubLink");
            entity.Property(e => e.Job)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("job");
            entity.Property(e => e.LinkedInLink)
                .HasMaxLength(255)
                .HasColumnName("linkedInLink");
            entity.Property(e => e.PersonalWebsiteLink)
                .HasMaxLength(255)
                .HasColumnName("personalWebsiteLink");
            entity.Property(e => e.RedditLink)
                .HasMaxLength(255)
                .HasColumnName("redditLink");
            entity.Property(e => e.ReputationPoints)
                .HasDefaultValue(0)
                .HasColumnName("reputationPoints");
            entity.Property(e => e.StackOverflowLink)
                .HasMaxLength(255)
                .HasColumnName("stackOverflowLink");
            entity.Property(e => e.ThreadsLink)
                .HasMaxLength(255)
                .HasColumnName("threadsLink");
            entity.Property(e => e.UserProfilePicUrl)
                .HasMaxLength(255)
                .HasColumnName("userProfilePicUrl");
            entity.Property(e => e.XLink)
                .HasMaxLength(255)
                .HasColumnName("xLink");
            entity.Property(e => e.YoutubeLink)
                .HasMaxLength(255)
                .HasColumnName("youtubeLink");

            entity.HasOne(d => d.User).WithOne(p => p.UserAdditionalInfo)
                .HasForeignKey<UserAdditionalInfo>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UserAddit__userI__5EBF139D");
        });

        modelBuilder.Entity<UserArticleBookmark>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ArticleId }).HasName("PK__UserArti__3CC721C8EFBE2D73");

            entity.ToTable("UserArticleBookmark");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ArticleId).HasColumnName("articleId");
            entity.Property(e => e.BookmarkDateTime)
                .HasColumnType("datetime")
                .HasColumnName("bookmarkDateTime");

            entity.HasOne(d => d.Article).WithMany(p => p.UserArticleBookmarks)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserArtic__artic__72C60C4A");

            entity.HasOne(d => d.User).WithMany(p => p.UserArticleBookmarks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserArtic__userI__71D1E811");
        });

        modelBuilder.Entity<UserArticleRead>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ArticleId }).HasName("PK__User_Art__3CC721C8917506F8");

            entity.ToTable("User_Article_Read");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ArticleId).HasColumnName("articleId");
            entity.Property(e => e.ReadDateTime)
                .HasColumnType("datetime")
                .HasColumnName("readDateTime");

            entity.HasOne(d => d.Article).WithMany(p => p.UserArticleReads)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Arti__artic__70DDC3D8");

            entity.HasOne(d => d.User).WithMany(p => p.UserArticleReads)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Arti__userI__6FE99F9F");
        });

        modelBuilder.Entity<UserArticleVote>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ArticleId }).HasName("PK__UserArti__3CC721C8FD48DE06");

            entity.ToTable("UserArticleVote");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ArticleId).HasColumnName("articleId");
            entity.Property(e => e.VoteType).HasColumnName("voteType");

            entity.HasOne(d => d.Article).WithMany(p => p.UserArticleVotes)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserArtic__artic__74AE54BC");

            entity.HasOne(d => d.User).WithMany(p => p.UserArticleVotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserArtic__userI__73BA3083");
        });

        modelBuilder.Entity<UserCommentVote>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CommentId }).HasName("PK__UserComm__0747F5E6E0933D82");

            entity.ToTable("UserCommentVote");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CommentId).HasColumnName("commentId");
            entity.Property(e => e.VoteType).HasColumnName("vote_type");

            entity.HasOne(d => d.Comment).WithMany(p => p.UserCommentVotes)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserComme__comme__6C190EBB");

            entity.HasOne(d => d.User).WithMany(p => p.UserCommentVotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserComme__userI__6B24EA82");
        });

        modelBuilder.Entity<UserFollow>(entity =>
        {
            entity.HasKey(e => new { e.FollowerId, e.FollowingId }).HasName("PK__UserFoll__6FA3F10A4976AB2A");

            entity.ToTable("UserFollow");

            entity.Property(e => e.FollowerId).HasColumnName("followerId");
            entity.Property(e => e.FollowingId).HasColumnName("followingId");
            entity.Property(e => e.FollowedDate)
                .HasColumnType("datetime")
                .HasColumnName("followedDate");

            entity.HasOne(d => d.Follower).WithMany(p => p.UserFollowFollowers)
                .HasForeignKey(d => d.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserFollo__follo__5CD6CB2B");

            entity.HasOne(d => d.Following).WithMany(p => p.UserFollowFollowings)
                .HasForeignKey(d => d.FollowingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserFollo__follo__5DCAEF64");
        });

        modelBuilder.Entity<UserTagFollow>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TagId }).HasName("PK__UserTagF__AE95DCEA718B7D70");

            entity.ToTable("UserTagFollow");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TagId).HasColumnName("tagId");
            entity.Property(e => e.FollowedDate)
                .HasColumnType("datetime")
                .HasColumnName("followedDate");

            entity.HasOne(d => d.Tag).WithMany(p => p.UserTagFollows)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserTagFo__tagId__60A75C0F");

            entity.HasOne(d => d.User).WithMany(p => p.UserTagFollows)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserTagFo__userI__5FB337D6");
        });
    }
}