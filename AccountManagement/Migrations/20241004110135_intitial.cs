using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccountManagement.Migrations
{
    /// <inheritdoc />
    public partial class intitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthcareProfessional",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareProfessional", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatientContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonForAppointment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HealthcareProfessionalId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointment_HealthcareProfessional_HealthcareProfessionalId1",
                        column: x => x.HealthcareProfessionalId,
                        principalTable: "HealthcareProfessional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "AppointmentId", "AppointmentDateTime", "HealthcareProfessionalId", "PatientContact", "PatientName", "ReasonForAppointment", "UserName" },
                values: new object[] { 1, new DateTime(2024, 10, 5, 14, 30, 0, 0, DateTimeKind.Unspecified), 0, "1234567890", "John Doe", "Annual check-up", null });

            migrationBuilder.InsertData(
                table: "HealthcareProfessional",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 21, "Dr. Kumar" },
                    { 22, "Dr. N" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_HealthcareProfessionalId1",
                table: "Appointment",
                column: "HealthcareProfessionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "HealthcareProfessional");
        }
    }
}
