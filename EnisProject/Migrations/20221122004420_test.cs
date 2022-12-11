using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnisProject.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Class_ClassId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClassId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClassId",
                schema: "security",
                table: "Users");
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

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassId",
                schema: "security",
                table: "Users",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Class_ClassId",
                schema: "security",
                table: "Users",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
