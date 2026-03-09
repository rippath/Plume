using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plume.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    ColorHex = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: true),
                    IsFeatured = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ParentTagId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ArticleCount = table.Column<int>(type: "int", nullable: false),
                    FollowerCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Tags_ParentTagId",
                        column: x => x.ParentTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    DisplayName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    EmailVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastActiveAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExternalId = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    ExternalProvider = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Role = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "User"),
                    HasLocalCredentials = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArticleSeries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    CoverImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    IsComplete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleSeries_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BookmarkFolders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    IsPrivate = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookmarkFolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookmarkFolders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    ActionUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    RelatedEntityId = table.Column<Guid>(type: "char(36)", nullable: true),
                    RelatedEntityType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Token = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedByIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    RevokedByIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    RevokedReason = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SearchQueries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Query = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    NormalizedQuery = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    ResultCount = table.Column<int>(type: "int", nullable: false),
                    HasClick = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ClickedArticleId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ClickedPosition = table.Column<int>(type: "int", nullable: true),
                    TimeToClickSeconds = table.Column<int>(type: "int", nullable: true),
                    FiltersApplied = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    SessionId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    SearchedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchQueries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchQueries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAuthorAffinities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false),
                    AffinityScore = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    IsFollowing = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ArticlesRead = table.Column<int>(type: "int", nullable: false),
                    ArticlesCompleted = table.Column<int>(type: "int", nullable: false),
                    TotalReactionsGiven = table.Column<int>(type: "int", nullable: false),
                    TotalClapsGiven = table.Column<int>(type: "int", nullable: false),
                    ArticlesBookmarked = table.Column<int>(type: "int", nullable: false),
                    CommentsWritten = table.Column<int>(type: "int", nullable: false),
                    TotalReadingTimeSeconds = table.Column<int>(type: "int", nullable: false),
                    AvgCompletionRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    FirstInteractionAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastInteractionAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecencyWeight = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false, defaultValue: 1.0m),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuthorAffinities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAuthorAffinities_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAuthorAffinities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserFollows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FollowerId = table.Column<Guid>(type: "char(36)", nullable: false),
                    FolloweeId = table.Column<Guid>(type: "char(36)", nullable: false),
                    NotifyOnNewArticle = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFollows_Users_FolloweeId",
                        column: x => x.FolloweeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFollows_Users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Bio = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    Tagline = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: true),
                    AvatarUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    CoverImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Location = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    TwitterHandle = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    LinkedInProfile = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    GitHubUsername = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FollowerCount = table.Column<int>(type: "int", nullable: false),
                    FollowingCount = table.Column<int>(type: "int", nullable: false),
                    ArticleCount = table.Column<int>(type: "int", nullable: false),
                    TotalArticleViews = table.Column<long>(type: "bigint", nullable: false),
                    TotalReactionsReceived = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserTopicInterests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TagId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Source = table.Column<int>(type: "int", nullable: false),
                    InterestScore = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    Confidence = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    ArticlesRead = table.Column<int>(type: "int", nullable: false),
                    TotalReadingTimeSeconds = table.Column<int>(type: "int", nullable: false),
                    ReactionsGiven = table.Column<int>(type: "int", nullable: false),
                    ArticlesBookmarked = table.Column<int>(type: "int", nullable: false),
                    AvgCompletionRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    LastActivityAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FirstActivityAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecencyWeight = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false, defaultValue: 1.0m),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopicInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTopicInterests_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTopicInterests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Title = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Slug = table.Column<string>(type: "varchar(350)", maxLength: 350, nullable: false),
                    Summary = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Content = table.Column<string>(type: "longtext", nullable: false),
                    ContentFormat = table.Column<int>(type: "int", nullable: false),
                    FeaturedImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    FeaturedImageAlt = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EstimatedReadingTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    WordCount = table.Column<int>(type: "int", nullable: false),
                    IsFeatured = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowComments = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SeriesId = table.Column<Guid>(type: "char(36)", nullable: true),
                    SeriesOrder = table.Column<int>(type: "int", nullable: true),
                    CanonicalUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    LanguageCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValue: "dv"),
                    IsRightToLeft = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false),
                    ReactionCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    BookmarkCount = table.Column<int>(type: "int", nullable: false),
                    ShareCount = table.Column<int>(type: "int", nullable: false),
                    EngagementScore = table.Column<double>(type: "double", precision: 18, scale: 6, nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleSeries_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "ArticleSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Articles_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArticleEngagementStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArticleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TotalViews = table.Column<long>(type: "bigint", nullable: false),
                    UniqueViewers = table.Column<long>(type: "bigint", nullable: false),
                    ViewsLast24Hours = table.Column<long>(type: "bigint", nullable: false),
                    ViewsLast7Days = table.Column<long>(type: "bigint", nullable: false),
                    ViewsLast30Days = table.Column<long>(type: "bigint", nullable: false),
                    AvgReadingTimeSeconds = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AvgScrollDepth = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    CompletionRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    BounceRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    TotalReactions = table.Column<int>(type: "int", nullable: false),
                    TotalClaps = table.Column<int>(type: "int", nullable: false),
                    UniqueReactors = table.Column<int>(type: "int", nullable: false),
                    TotalComments = table.Column<int>(type: "int", nullable: false),
                    UniqueCommenters = table.Column<int>(type: "int", nullable: false),
                    TotalBookmarks = table.Column<int>(type: "int", nullable: false),
                    TotalShares = table.Column<int>(type: "int", nullable: false),
                    ViewsFromFeed = table.Column<int>(type: "int", nullable: false),
                    ViewsFromSearch = table.Column<int>(type: "int", nullable: false),
                    ViewsFromRecommendation = table.Column<int>(type: "int", nullable: false),
                    ViewsFromExternal = table.Column<int>(type: "int", nullable: false),
                    ViewsFromDirect = table.Column<int>(type: "int", nullable: false),
                    EngagementQualityScore = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false),
                    ReactionRate = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false),
                    BookmarkRate = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false),
                    CommentRate = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false),
                    EngagementVelocity = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false),
                    OverallScore = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    LastComputedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleEngagementStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleEngagementStats_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArticleRevisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArticleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RevisionNumber = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false),
                    ContentFormat = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    RevisionNote = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    WordCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleRevisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleRevisions_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArticleTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArticleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TagId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleTags_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArticleViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArticleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Source = table.Column<int>(type: "int", nullable: false),
                    SourceEntityId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ReferrerUrl = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    SessionId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    UserAgent = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    CountryCode = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    DeviceType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    IsFirstView = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ViewedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleViews_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleViews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bookmarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArticleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Note = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    FolderId = table.Column<Guid>(type: "char(36)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookmarks_BookmarkFolders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "BookmarkFolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArticleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ParentCommentId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Content = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false),
                    IsEdited = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsHighlighted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsHidden = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HideReason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    ReplyCount = table.Column<int>(type: "int", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArticleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reactions_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RecommendationEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArticleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Placement = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Algorithm = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ModelVersion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PredictedScore = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false),
                    WasDisplayed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    WasClicked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TimeSpentSeconds = table.Column<int>(type: "int", nullable: true),
                    WasSuccessful = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    RecommendationReason = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    FeatureSnapshot = table.Column<string>(type: "json", nullable: true),
                    ExperimentGroup = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    RecommendedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClickedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecommendationEvents_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecommendationEvents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserArticleInteractions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArticleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    FirstViewedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastViewedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TotalReadingTimeSeconds = table.Column<int>(type: "int", nullable: false),
                    MaxScrollDepth = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    MaxReadingProgress = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    HasCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasReacted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ReactionCount = table.Column<int>(type: "int", nullable: false),
                    ClapCount = table.Column<int>(type: "int", nullable: false),
                    HasBookmarked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasCommented = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    HasShared = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasSelectedText = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasCopiedText = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EngagementScore = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false),
                    IsPositiveInteraction = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticleInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserArticleInteractions_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArticleInteractions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReadingSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArticleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ArticleViewId = table.Column<Guid>(type: "char(36)", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ActiveReadingTimeSeconds = table.Column<int>(type: "int", nullable: false),
                    TotalTimeOnPageSeconds = table.Column<int>(type: "int", nullable: false),
                    MaxScrollDepth = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    ReadingProgress = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    IsCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsBounce = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ScrollPauseCount = table.Column<int>(type: "int", nullable: false),
                    HasTextSelection = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasCopiedText = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SessionId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadingSessions_ArticleViews_ArticleViewId",
                        column: x => x.ArticleViewId,
                        principalTable: "ArticleViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ReadingSessions_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReadingSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommentLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CommentId = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedById = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentLikes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleEngagementStats_ArticleId",
                table: "ArticleEngagementStats",
                column: "ArticleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleEngagementStats_EngagementQualityScore",
                table: "ArticleEngagementStats",
                column: "EngagementQualityScore");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleEngagementStats_OverallScore",
                table: "ArticleEngagementStats",
                column: "OverallScore");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleEngagementStats_ViewsLast24Hours",
                table: "ArticleEngagementStats",
                column: "ViewsLast24Hours");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleRevisions_ArticleId_RevisionNumber",
                table: "ArticleRevisions",
                columns: new[] { "ArticleId", "RevisionNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_EngagementScore",
                table: "Articles",
                column: "EngagementScore");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_IsFeatured",
                table: "Articles",
                column: "IsFeatured");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PublishedAt",
                table: "Articles",
                column: "PublishedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_SeriesId",
                table: "Articles",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Slug",
                table: "Articles",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Status",
                table: "Articles",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Status_PublishedAt",
                table: "Articles",
                columns: new[] { "Status", "PublishedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleSeries_AuthorId",
                table: "ArticleSeries",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleSeries_Slug",
                table: "ArticleSeries",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_ArticleId_Order",
                table: "ArticleTags",
                columns: new[] { "ArticleId", "Order" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_ArticleId_TagId",
                table: "ArticleTags",
                columns: new[] { "ArticleId", "TagId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_TagId",
                table: "ArticleTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleViews_ArticleId",
                table: "ArticleViews",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleViews_ArticleId_ViewedAt",
                table: "ArticleViews",
                columns: new[] { "ArticleId", "ViewedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleViews_CountryCode",
                table: "ArticleViews",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleViews_Source",
                table: "ArticleViews",
                column: "Source");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleViews_UserId",
                table: "ArticleViews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleViews_ViewedAt",
                table: "ArticleViews",
                column: "ViewedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BookmarkFolders_UserId_Name",
                table: "BookmarkFolders",
                columns: new[] { "UserId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookmarkFolders_UserId_Order",
                table: "BookmarkFolders",
                columns: new[] { "UserId", "Order" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_ArticleId",
                table: "Bookmarks",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_FolderId",
                table: "Bookmarks",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId_ArticleId",
                table: "Bookmarks",
                columns: new[] { "UserId", "ArticleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId_CreatedDate",
                table: "Bookmarks",
                columns: new[] { "UserId", "CreatedDate" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikes_CommentId",
                table: "CommentLikes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikes_UserId_CommentId",
                table: "CommentLikes",
                columns: new[] { "UserId", "CommentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId_CreatedDate",
                table: "Comments",
                columns: new[] { "ArticleId", "CreatedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IsHidden",
                table: "Comments",
                column: "IsHidden");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_CreatedDate",
                table: "Notifications",
                columns: new[] { "UserId", "CreatedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_IsRead",
                table: "Notifications",
                columns: new[] { "UserId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_ArticleId",
                table: "Reactions",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UserId",
                table: "Reactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UserId_ArticleId_Type",
                table: "Reactions",
                columns: new[] { "UserId", "ArticleId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReadingSessions_ArticleId",
                table: "ReadingSessions",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingSessions_ArticleViewId",
                table: "ReadingSessions",
                column: "ArticleViewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingSessions_IsBounce",
                table: "ReadingSessions",
                column: "IsBounce");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingSessions_IsCompleted",
                table: "ReadingSessions",
                column: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingSessions_StartedAt",
                table: "ReadingSessions",
                column: "StartedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingSessions_UserId",
                table: "ReadingSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingSessions_UserId_ArticleId",
                table: "ReadingSessions",
                columns: new[] { "UserId", "ArticleId" });

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationEvents_Algorithm_ModelVersion_RecommendedAt",
                table: "RecommendationEvents",
                columns: new[] { "Algorithm", "ModelVersion", "RecommendedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationEvents_ArticleId",
                table: "RecommendationEvents",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationEvents_ExperimentGroup",
                table: "RecommendationEvents",
                column: "ExperimentGroup");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationEvents_UserId_RecommendedAt",
                table: "RecommendationEvents",
                columns: new[] { "UserId", "RecommendedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationEvents_WasDisplayed_WasClicked",
                table: "RecommendationEvents",
                columns: new[] { "WasDisplayed", "WasClicked" });

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationEvents_WasSuccessful",
                table: "RecommendationEvents",
                column: "WasSuccessful");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ExpiresAt",
                table: "RefreshTokens",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchQueries_HasClick",
                table: "SearchQueries",
                column: "HasClick");

            migrationBuilder.CreateIndex(
                name: "IX_SearchQueries_NormalizedQuery",
                table: "SearchQueries",
                column: "NormalizedQuery");

            migrationBuilder.CreateIndex(
                name: "IX_SearchQueries_SearchedAt",
                table: "SearchQueries",
                column: "SearchedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SearchQueries_UserId",
                table: "SearchQueries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ArticleCount",
                table: "Tags",
                column: "ArticleCount");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_IsFeatured",
                table: "Tags",
                column: "IsFeatured");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ParentTagId",
                table: "Tags",
                column: "ParentTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Slug",
                table: "Tags",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleInteractions_ArticleId",
                table: "UserArticleInteractions",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleInteractions_HasCompleted",
                table: "UserArticleInteractions",
                column: "HasCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleInteractions_UserId_ArticleId",
                table: "UserArticleInteractions",
                columns: new[] { "UserId", "ArticleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleInteractions_UserId_IsPositiveInteraction",
                table: "UserArticleInteractions",
                columns: new[] { "UserId", "IsPositiveInteraction" });

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleInteractions_UserId_LastViewedAt",
                table: "UserArticleInteractions",
                columns: new[] { "UserId", "LastViewedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthorAffinities_AuthorId",
                table: "UserAuthorAffinities",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthorAffinities_AuthorId_IsFollowing",
                table: "UserAuthorAffinities",
                columns: new[] { "AuthorId", "IsFollowing" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthorAffinities_UserId_AffinityScore",
                table: "UserAuthorAffinities",
                columns: new[] { "UserId", "AffinityScore" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthorAffinities_UserId_AuthorId",
                table: "UserAuthorAffinities",
                columns: new[] { "UserId", "AuthorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_FolloweeId",
                table: "UserFollows",
                column: "FolloweeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_FollowerId",
                table: "UserFollows",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_FollowerId_FolloweeId",
                table: "UserFollows",
                columns: new[] { "FollowerId", "FolloweeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_FollowerCount",
                table: "UserProfiles",
                column: "FollowerCount");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_TotalArticleViews",
                table: "UserProfiles",
                column: "TotalArticleViews");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExternalProvider_ExternalId",
                table: "Users",
                columns: new[] { "ExternalProvider", "ExternalId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsActive",
                table: "Users",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicInterests_Source",
                table: "UserTopicInterests",
                column: "Source");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicInterests_TagId",
                table: "UserTopicInterests",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicInterests_UserId_InterestScore",
                table: "UserTopicInterests",
                columns: new[] { "UserId", "InterestScore" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicInterests_UserId_TagId",
                table: "UserTopicInterests",
                columns: new[] { "UserId", "TagId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleEngagementStats");

            migrationBuilder.DropTable(
                name: "ArticleRevisions");

            migrationBuilder.DropTable(
                name: "ArticleTags");

            migrationBuilder.DropTable(
                name: "Bookmarks");

            migrationBuilder.DropTable(
                name: "CommentLikes");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "ReadingSessions");

            migrationBuilder.DropTable(
                name: "RecommendationEvents");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "SearchQueries");

            migrationBuilder.DropTable(
                name: "UserArticleInteractions");

            migrationBuilder.DropTable(
                name: "UserAuthorAffinities");

            migrationBuilder.DropTable(
                name: "UserFollows");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "UserTopicInterests");

            migrationBuilder.DropTable(
                name: "BookmarkFolders");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ArticleViews");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleSeries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
