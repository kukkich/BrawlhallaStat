using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrawlhallaStat.Domain.Migrations
{
    /// <inheritdoc />
    public partial class StatisticFilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatisticFilters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    GameType = table.Column<int>(type: "integer", nullable: true),
                    LegendId = table.Column<int>(type: "integer", nullable: true),
                    WeaponId = table.Column<int>(type: "integer", nullable: true),
                    EnemyLegendId = table.Column<int>(type: "integer", nullable: true),
                    EnemyWeaponId = table.Column<int>(type: "integer", nullable: true),
                    TeammateLegendId = table.Column<int>(type: "integer", nullable: true),
                    TeammateWeaponId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticFilters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatisticFilters_UserId",
                table: "StatisticFilters",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticFilters");
        }
    }
}
