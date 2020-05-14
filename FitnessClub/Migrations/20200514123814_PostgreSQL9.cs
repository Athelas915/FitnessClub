using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionEnrollments_CoachRatingID",
                table: "SessionEnrollments",
                column: "CoachRatingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SessionEnrollments_CoachRatingID",
                table: "SessionEnrollments");

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "CoachRatings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SessionID",
                table: "CoachRatings",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionEnrollments_CoachRatingID",
                table: "SessionEnrollments",
                column: "CoachRatingID",
                unique: true);
        }
    }
}
