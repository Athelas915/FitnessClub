using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_People_CoachPersonID",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_CoachPersonID",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CoachPersonID",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "CoachRatings");

            migrationBuilder.AddColumn<int>(
                name: "SessionID",
                table: "CoachRatings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_PersonID",
                table: "Sessions",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_CoachRatings_SessionID",
                table: "CoachRatings",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachRatings_Sessions_SessionID",
                table: "CoachRatings",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_People_PersonID",
                table: "Sessions",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachRatings_Sessions_SessionID",
                table: "CoachRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_People_PersonID",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_PersonID",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_CoachRatings_SessionID",
                table: "CoachRatings");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SessionID",
                table: "CoachRatings");

            migrationBuilder.AddColumn<int>(
                name: "CoachPersonID",
                table: "Sessions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "CoachRatings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CoachPersonID",
                table: "Sessions",
                column: "CoachPersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_People_CoachPersonID",
                table: "Sessions",
                column: "CoachPersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
