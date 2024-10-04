using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OABSystem.Migrations
{
    public partial class hpdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "HealthcareProfessional",
                columns: new[] { "Id", "Name" },
                values: new object[] { 10, "PAnkaj" });

            migrationBuilder.InsertData(
                table: "HealthcareProfessional",
                columns: new[] { "Id", "Name" },
                values: new object[] { 11, "Kumar" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HealthcareProfessional",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "HealthcareProfessional",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Appointment");
        }
    }
}
