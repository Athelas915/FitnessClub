using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachRatings_Sessions_CoachRatingID",
                table: "CoachRatings");

            migrationBuilder.AlterColumn<int>(
                name: "CoachRatingID",
                table: "CoachRatings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_CoachRatings_SessionID",
                table: "CoachRatings",
                column: "SessionID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachRatings_Sessions_SessionID",
                table: "CoachRatings",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachRatings_Sessions_SessionID",
                table: "CoachRatings");

            migrationBuilder.DropIndex(
                name: "IX_CoachRatings_SessionID",
                table: "CoachRatings");

            migrationBuilder.AlterColumn<int>(
                name: "CoachRatingID",
                table: "CoachRatings",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachRatings_Sessions_CoachRatingID",
                table: "CoachRatings",
                column: "CoachRatingID",
                principalTable: "Sessions",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
