using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course2.Migrations
{
    public partial class addEmloyeePositionenum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmloyeePosition",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmloyeePosition",
                table: "Users");
        }
    }
}
