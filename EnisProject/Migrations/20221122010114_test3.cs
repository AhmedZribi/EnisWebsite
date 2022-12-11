using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnisProject.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassId",
                schema: "security",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpecialityId",
                schema: "security",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpecialityId",
                table: "Internship",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeTable = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Class_ClassId",
                schema: "security",
                table: "Users",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Speciality_SpecialityId",
                schema: "security",
                table: "Users",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
