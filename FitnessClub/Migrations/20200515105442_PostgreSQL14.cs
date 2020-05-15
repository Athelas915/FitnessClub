using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachRatings_Sessions_SessionID",
                table: "CoachRatings");

            migrationBuilder.DropIndex(
                name: "IX_CoachRatings_SessionID",
                table: "CoachRatings");

            migrationBuilder.AddColumn<int>(
                name: "CoachRatingID",
                table: "Sessions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CoachRatingID",
                table: "CoachRatings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachRatings_Sessions_CoachRatingID",
                table: "CoachRatings",
                column: "CoachRatingID",
                principalTable: "Sessions",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachRatings_Sessions_CoachRatingID",
                table: "CoachRatings");

            migrationBuilder.DropColumn(
                name: "CoachRatingID",
                table: "Sessions");

            migrationBuilder.AlterColumn<int>(
                name: "CoachRatingID",
                table: "CoachRatings",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_CoachRatings_SessionID",
                table: "CoachRatings",
                column: "SessionID");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachRatings_Sessions_SessionID",
                table: "CoachRatings",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
