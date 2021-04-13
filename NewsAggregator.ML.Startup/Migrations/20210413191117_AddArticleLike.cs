using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.ML.Startup.Migrations
{
    public partial class AddArticleLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLike_ArticleAggregateId",
                table: "ArticleLike",
                column: "ArticleAggregateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleLike");
        }
    }
}
