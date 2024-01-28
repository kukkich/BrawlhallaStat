using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BrawlhallaStat.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddGameStatisticView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW ""GameStatisticView"" AS
                SELECT users.""Id"" AS ""UserId"",
	                game_details.""Id"" AS ""GameDetailId"", game_details.""Type"" AS ""GameType"", 
	                author_player.""IsWinner"" AS ""IsWin"",
	                legends.""Id"" AS ""LegendId"", weapons.""Id"" AS ""WeaponId"",
	                enemy_legends.""Id"" AS ""EnemyLegendId"", enemy_weapons.""Id"" AS ""EnemyWeaponId"",
	                teammate_legends.""Id"" AS ""TeammateLegendId"", teammate_weapons.""Id"" AS ""TeammateWeaponId""
                FROM public.""Users"" AS users
                JOIN public.""Games"" AS games
	                ON users.""Id"" = games.""AuthorId""
                JOIN public.""GameDetails"" AS game_details
	                ON games.""DetailId"" = game_details.""Id""

                JOIN public.""Players"" AS author_player
	                ON games.""AuthorPlayerId"" = author_player.""Id""
                JOIN public.""Legends"" AS legends
	                ON author_player.""LegendDetails_LegendId"" = legends.""Id""
                JOIN public.""Weapons"" AS weapons
	                ON legends.""FirstWeaponId"" = weapons.""Id"" OR
		                legends.""SecondWeaponId"" = weapons.""Id""

                JOIN public.""Players"" AS enemies
	                ON games.""DetailId"" = enemies.""GameDetailId"" AND
		                enemies.""Team"" != author_player.""Team""
                JOIN public.""Legends"" AS enemy_legends
	                ON enemies.""LegendDetails_LegendId"" = enemy_legends.""Id""
                JOIN public.""Weapons"" AS enemy_weapons
	                ON enemy_legends.""FirstWeaponId"" = enemy_weapons.""Id"" OR
		                enemy_legends.""SecondWeaponId"" = enemy_weapons.""Id""

                LEFT JOIN public.""Players"" AS teammates
	                ON games.""DetailId"" = teammates.""GameDetailId"" AND
		                teammates.""Team"" = author_player.""Team"" AND
		                teammates.""Id"" != author_player.""Id""
                LEFT JOIN public.""Legends"" AS teammate_legends
	                ON teammates.""LegendDetails_LegendId"" = teammate_legends.""Id""
                LEFT JOIN public.""Weapons"" AS teammate_weapons
	                ON teammate_legends.""FirstWeaponId"" = teammate_weapons.""Id"" OR
		                teammate_legends.""SecondWeaponId"" = teammate_weapons.""Id""
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW ""GameStatisticView""");
        }
    }
}
