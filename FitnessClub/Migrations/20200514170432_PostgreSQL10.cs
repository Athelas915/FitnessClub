using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Sessions",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "SessionEnrollments",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "People",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Memberships",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Holidays",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "CoachRatings",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Addresses",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "SessionEnrollments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "People",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Memberships",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Holidays",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "CoachRatings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Addresses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: -1);
        }
    }
}
