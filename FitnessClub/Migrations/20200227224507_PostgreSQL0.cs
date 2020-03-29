using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    PersonID1 = table.Column<int>(nullable: true),
                    Employee_PersonID1 = table.Column<int>(nullable: true),
                    EmployeePersonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_People_People_EmployeePersonID",
                        column: x => x.EmployeePersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_People_PersonID1",
                        column: x => x.PersonID1,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_People_Employee_PersonID1",
                        column: x => x.Employee_PersonID1,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    PersonID = table.Column<int>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Addresses_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoachRatings",
                columns: table => new
                {
                    CoachRatingID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    PersonID = table.Column<int>(nullable: false),
                    CoachPersonID = table.Column<int>(nullable: true),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachRatings", x => x.CoachRatingID);
                    table.ForeignKey(
                        name: "FK_CoachRatings_People_CoachPersonID",
                        column: x => x.CoachPersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    HolidayID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    PersonID = table.Column<int>(nullable: false),
                    EmployeePersonID = table.Column<int>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    Finish = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.HolidayID);
                    table.ForeignKey(
                        name: "FK_Holidays_People_EmployeePersonID",
                        column: x => x.EmployeePersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    MembershipID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    MembershipNo = table.Column<int>(nullable: false),
                    PersonID = table.Column<int>(nullable: false),
                    CustomerPersonID = table.Column<int>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    Finish = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.MembershipID);
                    table.ForeignKey(
                        name: "FK_Memberships_People_CustomerPersonID",
                        column: x => x.CustomerPersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    PersonID = table.Column<int>(nullable: false),
                    CoachPersonID = table.Column<int>(nullable: true),
                    SessionType = table.Column<int>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    Finish = table.Column<DateTime>(nullable: false),
                    Room = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_Sessions_People_CoachPersonID",
                        column: x => x.CoachPersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionEnrollments",
                columns: table => new
                {
                    SessionEnrollmentID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    SessionID = table.Column<int>(nullable: true),
                    PersonID = table.Column<int>(nullable: true),
                    CustomerPersonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionEnrollments", x => x.SessionEnrollmentID);
                    table.ForeignKey(
                        name: "FK_SessionEnrollments_People_CustomerPersonID",
                        column: x => x.CustomerPersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionEnrollments_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Sessions",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonID",
                table: "Addresses",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_CoachRatings_CoachPersonID",
                table: "CoachRatings",
                column: "CoachPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_EmployeePersonID",
                table: "Holidays",
                column: "EmployeePersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_CustomerPersonID",
                table: "Memberships",
                column: "CustomerPersonID");

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

            migrationBuilder.CreateIndex(
                name: "IX_SessionEnrollments_CustomerPersonID",
                table: "SessionEnrollments",
                column: "CustomerPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionEnrollments_SessionID",
                table: "SessionEnrollments",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CoachPersonID",
                table: "Sessions",
                column: "CoachPersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CoachRatings");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "SessionEnrollments");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
