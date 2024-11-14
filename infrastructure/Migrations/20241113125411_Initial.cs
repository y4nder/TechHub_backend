using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClubCategory",
                columns: table => new
                {
                    clubCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clubCategoryName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ClubCate__008AAC5D82704D6C", x => x.clubCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ClubUserRole",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ClubUser__CD98462A79796A77", x => x.roleId);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    tagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tagName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    tagCount = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tag__50FC01574E1148B3", x => x.tagId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    email = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__CB9A1CFFAC1B85EF", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    clubId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clubCreatorId = table.Column<int>(type: "int", nullable: true),
                    clubImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    clubName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    clubIntroduction = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    clubCategoryId = table.Column<int>(type: "int", nullable: true),
                    postPermission = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)0),
                    invitePermission = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Club__DF4AEAB285DE4451", x => x.clubId);
                    table.ForeignKey(
                        name: "FK__Club__clubCatego__6383C8BA",
                        column: x => x.clubCategoryId,
                        principalTable: "ClubCategory",
                        principalColumn: "clubCategoryId");
                    table.ForeignKey(
                        name: "FK__Club__clubCreato__628FA481",
                        column: x => x.clubCreatorId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "SearchHistory",
                columns: table => new
                {
                    searchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    searchQuery = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    searchedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SearchHi__33FFD956DA1979CE", x => x.searchId);
                    table.ForeignKey(
                        name: "FK__SearchHis__userI__619B8048",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "UserAdditionalInfo",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    userProfilePicUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    reputationPoints = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    company = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    contactNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    job = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    githubLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    linkedInLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    xLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    personalWebsiteLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    youtubeLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    stackOverflowLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    redditLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    threadsLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserAddi__CB9A1CFF5B46A646", x => x.userId);
                    table.ForeignKey(
                        name: "FK__UserAddit__userI__5EBF139D",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "UserFollow",
                columns: table => new
                {
                    followerId = table.Column<int>(type: "int", nullable: false),
                    followingId = table.Column<int>(type: "int", nullable: false),
                    followedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserFoll__6FA3F10A4976AB2A", x => new { x.followerId, x.followingId });
                    table.ForeignKey(
                        name: "FK__UserFollo__follo__5CD6CB2B",
                        column: x => x.followerId,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK__UserFollo__follo__5DCAEF64",
                        column: x => x.followingId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "UserTagFollow",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    tagId = table.Column<int>(type: "int", nullable: false),
                    followedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserTagF__AE95DCEA718B7D70", x => new { x.userId, x.tagId });
                    table.ForeignKey(
                        name: "FK__UserTagFo__tagId__60A75C0F",
                        column: x => x.tagId,
                        principalTable: "Tag",
                        principalColumn: "tagId");
                    table.ForeignKey(
                        name: "FK__UserTagFo__userI__5FB337D6",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    articleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    article_author_id = table.Column<int>(type: "int", nullable: true),
                    clubId = table.Column<int>(type: "int", nullable: true),
                    articleThumbnailUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    createdDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    updateDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    isDrafted = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Article__75D3D37EA733C6CA", x => x.articleId);
                    table.ForeignKey(
                        name: "FK__Article__article__6D0D32F4",
                        column: x => x.article_author_id,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK__Article__clubId__6E01572D",
                        column: x => x.clubId,
                        principalTable: "Club",
                        principalColumn: "clubId");
                });

            migrationBuilder.CreateTable(
                name: "ClubAdditionalInfo",
                columns: table => new
                {
                    clubId = table.Column<int>(type: "int", nullable: true),
                    clubCreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    clubDescription = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__ClubAddit__clubI__6754599E",
                        column: x => x.clubId,
                        principalTable: "Club",
                        principalColumn: "clubId");
                });

            migrationBuilder.CreateTable(
                name: "ClubUser",
                columns: table => new
                {
                    clubId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ClubUser__33F34B7D37E4E2E3", x => new { x.clubId, x.userId });
                    table.ForeignKey(
                        name: "FK__ClubUser__clubId__6477ECF3",
                        column: x => x.clubId,
                        principalTable: "Club",
                        principalColumn: "clubId");
                    table.ForeignKey(
                        name: "FK__ClubUser__roleId__66603565",
                        column: x => x.roleId,
                        principalTable: "ClubUserRole",
                        principalColumn: "roleId");
                    table.ForeignKey(
                        name: "FK__ClubUser__userId__656C112C",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "ArticleBody",
                columns: table => new
                {
                    articleId = table.Column<int>(type: "int", nullable: true),
                    articleContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__ArticleBo__artic__6EF57B66",
                        column: x => x.articleId,
                        principalTable: "Article",
                        principalColumn: "articleId");
                });

            migrationBuilder.CreateTable(
                name: "ArticleTag",
                columns: table => new
                {
                    tagId = table.Column<int>(type: "int", nullable: false),
                    articleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ArticleT__A7A13C6086DD3216", x => new { x.tagId, x.articleId });
                    table.ForeignKey(
                        name: "FK__ArticleTa__artic__76969D2E",
                        column: x => x.articleId,
                        principalTable: "Article",
                        principalColumn: "articleId");
                    table.ForeignKey(
                        name: "FK__ArticleTa__tagId__75A278F5",
                        column: x => x.tagId,
                        principalTable: "Tag",
                        principalColumn: "tagId");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    commentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    commentCreatorId = table.Column<int>(type: "int", nullable: true),
                    articleId = table.Column<int>(type: "int", nullable: true),
                    parentCommentId = table.Column<int>(type: "int", nullable: true),
                    content = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    createdDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    updateDateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comment__CDDE919D08B27851", x => x.commentId);
                    table.ForeignKey(
                        name: "FK__Comment__article__693CA210",
                        column: x => x.articleId,
                        principalTable: "Article",
                        principalColumn: "articleId");
                    table.ForeignKey(
                        name: "FK__Comment__comment__68487DD7",
                        column: x => x.commentCreatorId,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK__Comment__parentC__6A30C649",
                        column: x => x.parentCommentId,
                        principalTable: "Comment",
                        principalColumn: "commentId");
                });

            migrationBuilder.CreateTable(
                name: "User_Article_Read",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    articleId = table.Column<int>(type: "int", nullable: false),
                    readDateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Art__3CC721C8917506F8", x => new { x.userId, x.articleId });
                    table.ForeignKey(
                        name: "FK__User_Arti__artic__70DDC3D8",
                        column: x => x.articleId,
                        principalTable: "Article",
                        principalColumn: "articleId");
                    table.ForeignKey(
                        name: "FK__User_Arti__userI__6FE99F9F",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "UserArticleBookmark",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    articleId = table.Column<int>(type: "int", nullable: false),
                    bookmarkDateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserArti__3CC721C8EFBE2D73", x => new { x.userId, x.articleId });
                    table.ForeignKey(
                        name: "FK__UserArtic__artic__72C60C4A",
                        column: x => x.articleId,
                        principalTable: "Article",
                        principalColumn: "articleId");
                    table.ForeignKey(
                        name: "FK__UserArtic__userI__71D1E811",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "UserArticleVote",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    articleId = table.Column<int>(type: "int", nullable: false),
                    voteType = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserArti__3CC721C8FD48DE06", x => new { x.userId, x.articleId });
                    table.ForeignKey(
                        name: "FK__UserArtic__artic__74AE54BC",
                        column: x => x.articleId,
                        principalTable: "Article",
                        principalColumn: "articleId");
                    table.ForeignKey(
                        name: "FK__UserArtic__userI__73BA3083",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "UserCommentVote",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    commentId = table.Column<int>(type: "int", nullable: false),
                    vote_type = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserComm__0747F5E6E0933D82", x => new { x.userId, x.commentId });
                    table.ForeignKey(
                        name: "FK__UserComme__comme__6C190EBB",
                        column: x => x.commentId,
                        principalTable: "Comment",
                        principalColumn: "commentId");
                    table.ForeignKey(
                        name: "FK__UserComme__userI__6B24EA82",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_article_author_id",
                table: "Article",
                column: "article_author_id");

            migrationBuilder.CreateIndex(
                name: "IX_Article_clubId",
                table: "Article",
                column: "clubId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleBody_articleId",
                table: "ArticleBody",
                column: "articleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTag_articleId",
                table: "ArticleTag",
                column: "articleId");

            migrationBuilder.CreateIndex(
                name: "IX_Club_clubCategoryId",
                table: "Club",
                column: "clubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Club_clubCreatorId",
                table: "Club",
                column: "clubCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubAdditionalInfo_clubId",
                table: "ClubAdditionalInfo",
                column: "clubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubUser_roleId",
                table: "ClubUser",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubUser_userId",
                table: "ClubUser",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_articleId",
                table: "Comment",
                column: "articleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_commentCreatorId",
                table: "Comment",
                column: "commentCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_parentCommentId",
                table: "Comment",
                column: "parentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistory_userId",
                table: "SearchHistory",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Article_Read_articleId",
                table: "User_Article_Read",
                column: "articleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleBookmark_articleId",
                table: "UserArticleBookmark",
                column: "articleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleVote_articleId",
                table: "UserArticleVote",
                column: "articleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommentVote_commentId",
                table: "UserCommentVote",
                column: "commentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollow_followingId",
                table: "UserFollow",
                column: "followingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTagFollow_tagId",
                table: "UserTagFollow",
                column: "tagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleBody");

            migrationBuilder.DropTable(
                name: "ArticleTag");

            migrationBuilder.DropTable(
                name: "ClubAdditionalInfo");

            migrationBuilder.DropTable(
                name: "ClubUser");

            migrationBuilder.DropTable(
                name: "SearchHistory");

            migrationBuilder.DropTable(
                name: "User_Article_Read");

            migrationBuilder.DropTable(
                name: "UserAdditionalInfo");

            migrationBuilder.DropTable(
                name: "UserArticleBookmark");

            migrationBuilder.DropTable(
                name: "UserArticleVote");

            migrationBuilder.DropTable(
                name: "UserCommentVote");

            migrationBuilder.DropTable(
                name: "UserFollow");

            migrationBuilder.DropTable(
                name: "UserTagFollow");

            migrationBuilder.DropTable(
                name: "ClubUserRole");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "ClubCategory");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
