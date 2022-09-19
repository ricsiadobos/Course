using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course2.Migrations
{
    public partial class PosPositionIdremove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "PositionId",
            //    table: "Positions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "PositionId",
            //    table: "Positions",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);
        }
    }
}
