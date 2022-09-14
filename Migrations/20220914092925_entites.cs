using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course2.Migrations
{
    public partial class entites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeName",
                table: "Users",
                newName: "EmloyeeName");

            migrationBuilder.AddColumn<string>(
                name: "EmloyeeEmail",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmloyeeEmail",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "EmloyeeName",
                table: "Users",
                newName: "EmployeeName");
        }
    }
}
