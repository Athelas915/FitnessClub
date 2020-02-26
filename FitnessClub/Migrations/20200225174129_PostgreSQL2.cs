using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonID1",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Employee_PersonID1",
                table: "People",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_EmployeeID",
                table: "People",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_People_PersonID1",
                table: "People",
                column: "PersonID1");

            migrationBuilder.CreateIndex(
                name: "IX_People_Employee_PersonID1",
                table: "People",
                column: "Employee_PersonID1");

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_EmployeeID",
                table: "People",
                column: "EmployeeID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_People_EmployeeID",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_People_PersonID1",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_People_Employee_PersonID1",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_EmployeeID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_PersonID1",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_Employee_PersonID1",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PersonID1",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Employee_PersonID1",
                table: "People");
        }
    }
}
