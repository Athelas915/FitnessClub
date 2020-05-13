using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_AspNetUsers_Id",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_Id",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "People");

            migrationBuilder.AddColumn<int>(
                name: "AspNetUserId",
                table: "People",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_People_AspNetUserId",
                table: "People",
                column: "AspNetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_AspNetUsers_AspNetUserId",
                table: "People",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_AspNetUsers_AspNetUserId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_AspNetUserId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "People");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "People",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_Id",
                table: "People",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_People_AspNetUsers_Id",
                table: "People",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
