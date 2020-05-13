using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "People",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
