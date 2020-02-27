using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_People_EmployeeID",
                table: "Holidays");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_People_CustomerID",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionEnrollments_People_CustomerID",
                table: "SessionEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_People_CoachID",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_CoachID",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_SessionEnrollments_CustomerID",
                table: "SessionEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_CustomerID",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_EmployeeID",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "CoachID",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "SessionEnrollments");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Holidays");

            migrationBuilder.AddColumn<int>(
                name: "CoachPersonID",
                table: "Sessions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "Sessions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerPersonID",
                table: "SessionEnrollments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "SessionEnrollments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerPersonID",
                table: "Memberships",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "Memberships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeePersonID",
                table: "Holidays",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "Holidays",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CoachPersonID",
                table: "Sessions",
                column: "CoachPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionEnrollments_CustomerPersonID",
                table: "SessionEnrollments",
                column: "CustomerPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_CustomerPersonID",
                table: "Memberships",
                column: "CustomerPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_EmployeePersonID",
                table: "Holidays",
                column: "EmployeePersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_People_EmployeePersonID",
                table: "Holidays",
                column: "EmployeePersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_People_CustomerPersonID",
                table: "Memberships",
                column: "CustomerPersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionEnrollments_People_CustomerPersonID",
                table: "SessionEnrollments",
                column: "CustomerPersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_People_CoachPersonID",
                table: "Sessions",
                column: "CoachPersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_People_EmployeePersonID",
                table: "Holidays");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_People_CustomerPersonID",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionEnrollments_People_CustomerPersonID",
                table: "SessionEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_People_CoachPersonID",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_CoachPersonID",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_SessionEnrollments_CustomerPersonID",
                table: "SessionEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_CustomerPersonID",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_EmployeePersonID",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "CoachPersonID",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CustomerPersonID",
                table: "SessionEnrollments");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "SessionEnrollments");

            migrationBuilder.DropColumn(
                name: "CustomerPersonID",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "EmployeePersonID",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Holidays");

            migrationBuilder.AddColumn<int>(
                name: "CoachID",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "SessionEnrollments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Memberships",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "Holidays",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CoachID",
                table: "Sessions",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionEnrollments_CustomerID",
                table: "SessionEnrollments",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_CustomerID",
                table: "Memberships",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_EmployeeID",
                table: "Holidays",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_People_EmployeeID",
                table: "Holidays",
                column: "EmployeeID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_People_CustomerID",
                table: "Memberships",
                column: "CustomerID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionEnrollments_People_CustomerID",
                table: "SessionEnrollments",
                column: "CustomerID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_People_CoachID",
                table: "Sessions",
                column: "CoachID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
