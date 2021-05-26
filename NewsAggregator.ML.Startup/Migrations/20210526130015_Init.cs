using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.ML.Startup.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataSourceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    NbRead = table.Column<int>(type: "int", nullable: false),
                    NbLikes = table.Column<int>(type: "int", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NbFollowers = table.Column<int>(type: "int", nullable: false),
                    NbStoriesPerMonth = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleLike",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArticleAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleLike_Articles_ArticleAggregateId",
                        column: x => x.ArticleAggregateId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleRead",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    ArticleAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleRead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleRead_Articles_ArticleAggregateId",
                        column: x => x.ArticleAggregateId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataSourceArticle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NbArticles = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    DataSourceAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceArticle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSourceArticle_DataSources_DataSourceAggregateId",
                        column: x => x.DataSourceAggregateId,
                        principalTable: "DataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataSourceExtractionHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastArticlePublicationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    NbExtractedArticle = table.Column<int>(type: "int", nullable: false),
                    DataSourceAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceExtractionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSourceExtractionHistory_DataSources_DataSourceAggregateId",
                        column: x => x.DataSourceAggregateId,
                        principalTable: "DataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataSourceTopic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nb = table.Column<int>(type: "int", nullable: false),
                    DataSourceAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSourceTopic_DataSources_DataSourceAggregateId",
                        column: x => x.DataSourceAggregateId,
                        principalTable: "DataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedDatasource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasourceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedDatasource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedDatasource_Feeds_FeedAggregateId",
                        column: x => x.FeedAggregateId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecommendationArticle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<double>(type: "float", nullable: false),
                    RecommendationAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationArticle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecommendationArticle_Recommendations_RecommendationAggregateId",
                        column: x => x.RecommendationAggregateId,
                        principalTable: "Recommendations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticleLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InteractionType = table.Column<int>(type: "int", nullable: true),
                    ActionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionAction_Sessions_SessionAggregateId",
                        column: x => x.SessionAggregateId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLike_ArticleAggregateId",
                table: "ArticleLike",
                column: "ArticleAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleRead_ArticleAggregateId",
                table: "ArticleRead",
                column: "ArticleAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSourceArticle_DataSourceAggregateId",
                table: "DataSourceArticle",
                column: "DataSourceAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSourceExtractionHistory_DataSourceAggregateId",
                table: "DataSourceExtractionHistory",
                column: "DataSourceAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSourceTopic_DataSourceAggregateId",
                table: "DataSourceTopic",
                column: "DataSourceAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedDatasource_FeedAggregateId",
                table: "FeedDatasource",
                column: "FeedAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationArticle_RecommendationAggregateId",
                table: "RecommendationArticle",
                column: "RecommendationAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionAction_SessionAggregateId",
                table: "SessionAction",
                column: "SessionAggregateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleLike");

            migrationBuilder.DropTable(
                name: "ArticleRead");

            migrationBuilder.DropTable(
                name: "DataSourceArticle");

            migrationBuilder.DropTable(
                name: "DataSourceExtractionHistory");

            migrationBuilder.DropTable(
                name: "DataSourceTopic");

            migrationBuilder.DropTable(
                name: "FeedDatasource");

            migrationBuilder.DropTable(
                name: "RecommendationArticle");

            migrationBuilder.DropTable(
                name: "SessionAction");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "DataSources");

            migrationBuilder.DropTable(
                name: "Feeds");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
