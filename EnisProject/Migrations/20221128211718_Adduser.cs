using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnisProject.Migrations
{
    public partial class Adduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                schema: "security",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                schema: "security",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "Internship",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timetable = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassId",
                schema: "security",
                table: "Users",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SpecialityId",
                schema: "security",
                table: "Users",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Internship_SpecialityId",
                table: "Internship",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Internship_Speciality_SpecialityId",
                table: "Internship",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Class_ClassId",
                schema: "security",
                table: "Users",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Speciality_SpecialityId",
                schema: "security",
                table: "Users",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internship_Speciality_SpecialityId",
                table: "Internship");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Class_ClassId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Speciality_SpecialityId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClassId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SpecialityId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Internship_SpecialityId",
                table: "Internship");

            migrationBuilder.DropColumn(
                name: "ClassId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Internship");
        }
    }
}
