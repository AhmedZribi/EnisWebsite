using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnisProject.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[] { Guid.NewGuid().ToString(), "User", "User".ToUpper(), Guid.NewGuid().ToString() },
            schema: "security");
            migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() },
            schema: "security");
            migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[] { Guid.NewGuid().ToString(), "Student", "Student".ToUpper(), Guid.NewGuid().ToString() },
            schema: "security");
            migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[] { Guid.NewGuid().ToString(), "Professor", "Professor".ToUpper(), Guid.NewGuid().ToString() },
            schema: "security");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Roles]");
        }
    }
}
