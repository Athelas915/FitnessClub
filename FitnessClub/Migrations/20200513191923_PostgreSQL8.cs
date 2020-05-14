using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SessionEnrollments_CoachRatingID",
                table: "SessionEnrollments");

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "CoachRatings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "SessionID",
                table: "CoachRatings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionEnrollments_CoachRatingID",
                table: "SessionEnrollments",
                column: "CoachRatingID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SessionEnrollments_CoachRatingID",
                table: "SessionEnrollments");

            migrationBuilder.DropColumn(
                name: "SessionID",
                table: "CoachRatings");

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "CoachRatings",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionEnrollments_CoachRatingID",
                table: "SessionEnrollments",
                column: "CoachRatingID");
        }
    }
}
