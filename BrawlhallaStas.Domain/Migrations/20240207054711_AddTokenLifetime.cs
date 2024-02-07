using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrawlhallaStat.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenLifetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "Tokens",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: DateTime.Today);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFrom",
                table: "Tokens",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: DateTime.Today.AddDays(1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ValidFrom",
                table: "Tokens");
        }
    }
}
