using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_AspNetUserId",
                table: "People");

            migrationBuilder.CreateIndex(
                name: "IX_People_AspNetUserId",
                table: "People",
                column: "AspNetUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_AspNetUserId",
                table: "People");

            migrationBuilder.CreateIndex(
                name: "IX_People_AspNetUserId",
                table: "People",
                column: "AspNetUserId");
        }
    }
}
