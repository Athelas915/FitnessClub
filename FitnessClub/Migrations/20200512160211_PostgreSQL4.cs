using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_AspNetUsers_AspNetUserId",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "AspNetUserId",
                table: "People",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_People_AspNetUsers_AspNetUserId",
                table: "People",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_AspNetUsers_AspNetUserId",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "AspNetUserId",
                table: "People",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_People_AspNetUsers_AspNetUserId",
                table: "People",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
