using Microsoft.EntityFrameworkCore.Migrations;

namespace sveikata.Migrations
{
    public partial class AddConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Diseases",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Pirmosios ligos aprasymas", "Pirmoji liga" },
                    { 2, "Antrosios ligos aprasymas", "Antroji liga" },
                    { 3, "Treciosios ligos aprasymas", "Trecioji liga" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Pirmojo gydymo budo aprasymas", "Pirmasis gydymo budas" },
                    { 2, "Antrojo gydymo budo aprasymas", "Antrasis gydymo budas" },
                    { 3, "Treciojo gydymo budo aprasymas", "Treciasis gydymo budas" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
