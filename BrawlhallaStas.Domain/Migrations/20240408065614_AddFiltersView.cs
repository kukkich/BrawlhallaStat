using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrawlhallaStat.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddFiltersView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW ""FilterView"" AS
                SELECT filtered_games.""Id"" AS ""FilterId"", 
	                filtered_games.""UserId"" AS ""UserId"",
	                filtered_games.""GameType"" AS ""GameType"",
	                filtered_games.""LegendId"" AS ""LegendId"",
	                filtered_games.""WeaponId"" AS ""WeaponId"",
	                filtered_games.""EnemyLegendId"" AS ""EnemyLegendId"",
	                filtered_games.""EnemyWeaponId"" AS ""EnemyWeaponId"",
	                filtered_games.""TeammateLegendId"" AS ""TeammateLegendId"",
	                filtered_games.""TeammateWeaponId"" AS ""TeammateWeaponId"",
	                COUNT(*) FILTER (WHERE filtered_games.""IsWin"") as ""Wins"",
  	                COUNT(*) FILTER (WHERE NOT filtered_games.""IsWin"") as ""Defeats""
                FROM (
	                SELECT DISTINCT filters.*, gameViews.""GameDetailId"", gameViews.""IsWin""
	                FROM ""StatisticFilters"" as filters
	                LEFT JOIN ""GameStatisticView"" as gameViews 
		                ON (
			                filters.""UserId"" = gameViews.""UserId"" AND 
			                (filters.""GameType"" is NULL OR filters.""GameType"" = gameViews.""GameType"") AND
			                (filters.""LegendId"" is NULL OR filters.""LegendId"" = gameViews.""LegendId"") AND
			                (filters.""WeaponId"" is NULL OR filters.""WeaponId"" = gameViews.""WeaponId"") AND
			                (filters.""EnemyLegendId"" is NULL OR filters.""EnemyLegendId"" = gameViews.""EnemyLegendId"") AND
			                (filters.""EnemyWeaponId"" is NULL OR filters.""EnemyWeaponId"" = gameViews.""EnemyWeaponId"") AND
			                (filters.""TeammateLegendId"" is NULL OR filters.""TeammateLegendId"" = gameViews.""TeammateLegendId"") AND
			                (filters.""TeammateWeaponId"" is NULL OR filters.""TeammateWeaponId"" = gameViews.""TeammateWeaponId"")
		                )
	                ) AS filtered_games
                GROUP BY ""FilterId"",
	                ""UserId"",
	                ""GameType"",
	                ""LegendId"",
	                ""WeaponId"",
	                ""EnemyLegendId"",
	                ""EnemyWeaponId"",
	                ""TeammateLegendId"",
	                ""TeammateWeaponId""
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS ""FilterView""");
        }
    }
}
