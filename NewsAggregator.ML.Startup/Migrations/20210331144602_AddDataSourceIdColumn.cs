using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.ML.Startup.Migrations
{
    public partial class AddDataSourceIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataSourceId",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataSourceId",
                table: "Articles");
        }
    }
}
