using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course2.Migrations
{
    public partial class renameDbSetUsersEmployeesrenameEmployeepropp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Employees");

            migrationBuilder.RenameColumn(
                name: "videoURL",
                table: "Videos",
                newName: "VideoURL");

            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "LogWatchVideos",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "EmloyeeName",
                table: "Employees",
                newName: "EmployeeName");

            migrationBuilder.RenameColumn(
                name: "EmloyeeEmail",
                table: "Employees",
                newName: "EmployeeEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "VideoURL",
                table: "Videos",
                newName: "videoURL");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "LogWatchVideos",
                newName: "dateTime");

            migrationBuilder.RenameColumn(
                name: "EmployeeName",
                table: "Users",
                newName: "EmloyeeName");

            migrationBuilder.RenameColumn(
                name: "EmployeeEmail",
                table: "Users",
                newName: "EmloyeeEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
