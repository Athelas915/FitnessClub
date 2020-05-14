using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_AspNetUsers_AspNetUserId",
                table: "People");

            migrationBuilder.AddColumn<int>(
                name: "CoachRatingID",
                table: "SessionEnrollments",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AspNetUserId",
                table: "People",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionEnrollments_CoachRatingID",
                table: "SessionEnrollments",
                column: "CoachRatingID");

            migrationBuilder.AddForeignKey(
                name: "FK_People_AspNetUsers_AspNetUserId",
                table: "People",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionEnrollments_CoachRatings_CoachRatingID",
                table: "SessionEnrollments",
                column: "CoachRatingID",
                principalTable: "CoachRatings",
                principalColumn: "CoachRatingID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_AspNetUsers_AspNetUserId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionEnrollments_CoachRatings_CoachRatingID",
                table: "SessionEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_SessionEnrollments_CoachRatingID",
                table: "SessionEnrollments");

            migrationBuilder.DropColumn(
                name: "CoachRatingID",
                table: "SessionEnrollments");

            migrationBuilder.AlterColumn<int>(
                name: "AspNetUserId",
                table: "People",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_People_AspNetUsers_AspNetUserId",
                table: "People",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
