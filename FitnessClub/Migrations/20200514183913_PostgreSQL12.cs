﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Migrations
{
    public partial class PostgreSQL12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_People_PersonID",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PersonID",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Sessions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "SessionEnrollments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "People",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Memberships",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Holidays",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "CoachRatings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: -1);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonID",
                table: "Addresses",
                column: "PersonID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_People_PersonID",
                table: "Addresses",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_People_PersonID",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PersonID",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "SessionEnrollments",
                type: "integer",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "People",
                type: "integer",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Memberships",
                type: "integer",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Holidays",
                type: "integer",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "CoachRatings",
                type: "integer",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "Addresses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Addresses",
                type: "integer",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonID",
                table: "Addresses",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_People_PersonID",
                table: "Addresses",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}