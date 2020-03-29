using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_People_PersonID",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionEnrollments_Sessions_SessionID",
                table: "SessionEnrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionEnrollments",
                table: "SessionEnrollments");

            migrationBuilder.DropColumn(
                name: "SessionEnrollmentID",
                table: "SessionEnrollments");

            migrationBuilder.AlterColumn<int>(
                name: "SessionID",
                table: "SessionEnrollments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "SessionEnrollments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionEnrollments",
                table: "SessionEnrollments",
                columns: new[] { "PersonID", "SessionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_People_PersonID",
                table: "Addresses",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionEnrollments_Sessions_SessionID",
                table: "SessionEnrollments",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_People_PersonID",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionEnrollments_Sessions_SessionID",
                table: "SessionEnrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionEnrollments",
                table: "SessionEnrollments");

            migrationBuilder.AlterColumn<int>(
                name: "SessionID",
                table: "SessionEnrollments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "SessionEnrollments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SessionEnrollmentID",
                table: "SessionEnrollments",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "Addresses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionEnrollments",
                table: "SessionEnrollments",
                column: "SessionEnrollmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_People_PersonID",
                table: "Addresses",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionEnrollments_Sessions_SessionID",
                table: "SessionEnrollments",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
