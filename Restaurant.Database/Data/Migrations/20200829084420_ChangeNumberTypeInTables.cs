using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Database.Data.Migrations
{
    public partial class ChangeNumberTypeInTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Tables",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Number",
                table: "Tables",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
