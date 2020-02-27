using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_People_PersonID",
                table: "Adresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachRatings_People_CoachID",
                table: "CoachRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_People_People_EmployeeID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_EmployeeID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_CoachRatings_CoachID",
                table: "CoachRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses");

            migrationBuilder.DropIndex(
                name: "IX_Adresses_PersonID",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "CoachID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "CoachID",
                table: "CoachRatings");

            migrationBuilder.DropColumn(
                name: "AdressID",
                table: "Adresses");

            migrationBuilder.AddColumn<int>(
                name: "EmployeePersonID",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoachPersonID",
                table: "CoachRatings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "CoachRatings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "Adresses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "PersonID1",
                table: "Adresses",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_People_EmployeePersonID",
                table: "People",
                column: "EmployeePersonID");

            migrationBuilder.CreateIndex(
                name: "IX_CoachRatings_CoachPersonID",
                table: "CoachRatings",
                column: "CoachPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_PersonID1",
                table: "Adresses",
                column: "PersonID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_People_PersonID1",
                table: "Adresses",
                column: "PersonID1",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachRatings_People_CoachPersonID",
                table: "CoachRatings",
                column: "CoachPersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_EmployeePersonID",
                table: "People",
                column: "EmployeePersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_People_PersonID1",
                table: "Adresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachRatings_People_CoachPersonID",
                table: "CoachRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_People_People_EmployeePersonID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_EmployeePersonID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_CoachRatings_CoachPersonID",
                table: "CoachRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses");

            migrationBuilder.DropIndex(
                name: "IX_Adresses_PersonID1",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "EmployeePersonID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "CoachPersonID",
                table: "CoachRatings");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "CoachRatings");

            migrationBuilder.DropColumn(
                name: "PersonID1",
                table: "Adresses");

            migrationBuilder.AddColumn<int>(
                name: "CoachID",
                table: "People",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "People",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "People",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoachID",
                table: "CoachRatings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "Adresses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "AdressID",
                table: "Adresses",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses",
                column: "AdressID");

            migrationBuilder.CreateIndex(
                name: "IX_People_EmployeeID",
                table: "People",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_CoachRatings_CoachID",
                table: "CoachRatings",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_PersonID",
                table: "Adresses",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_People_PersonID",
                table: "Adresses",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoachRatings_People_CoachID",
                table: "CoachRatings",
                column: "CoachID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_EmployeeID",
                table: "People",
                column: "EmployeeID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
