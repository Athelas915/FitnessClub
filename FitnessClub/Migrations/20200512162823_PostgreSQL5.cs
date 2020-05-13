using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_People_EmployeePersonID",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_People_PersonID1",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_People_Employee_PersonID1",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_EmployeePersonID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_PersonID1",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_Employee_PersonID1",
                table: "People");

            migrationBuilder.DropColumn(
                name: "EmployeePersonID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PersonID1",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Employee_PersonID1",
                table: "People");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeePersonID",
                table: "People",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID1",
                table: "People",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Employee_PersonID1",
                table: "People",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_EmployeePersonID",
                table: "People",
                column: "EmployeePersonID");

            migrationBuilder.CreateIndex(
                name: "IX_People_PersonID1",
                table: "People",
                column: "PersonID1");

            migrationBuilder.CreateIndex(
                name: "IX_People_Employee_PersonID1",
                table: "People",
                column: "Employee_PersonID1");

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_EmployeePersonID",
                table: "People",
                column: "EmployeePersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_PersonID1",
                table: "People",
                column: "PersonID1",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_Employee_PersonID1",
                table: "People",
                column: "Employee_PersonID1",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
