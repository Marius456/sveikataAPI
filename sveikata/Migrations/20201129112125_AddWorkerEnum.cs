using Microsoft.EntityFrameworkCore.Migrations;

namespace sveikata.Migrations
{
    public partial class AddWorkerEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                column: "Name",
                value: "Worker");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Name",
                keyValue: "Worker");
        }
    }
}
