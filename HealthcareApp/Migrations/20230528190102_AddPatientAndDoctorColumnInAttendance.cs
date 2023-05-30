using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareApp.Migrations
{
    public partial class AddPatientAndDoctorColumnInAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Users_UserAccountId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Attendances_AttendanceId",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Users_UserAccountId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Medications_AttendanceId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "AttendanceId",
                table: "Medications");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Attendances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicationId",
                table: "Attendances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_MedicationId",
                table: "Attendances",
                column: "MedicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Medications_MedicationId",
                table: "Attendances",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Users_UserAccountId",
                table: "Doctors",
                column: "UserAccountId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Users_UserAccountId",
                table: "Patients",
                column: "UserAccountId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Medications_MedicationId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Users_UserAccountId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Users_UserAccountId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_MedicationId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "MedicationId",
                table: "Attendances");

            migrationBuilder.AddColumn<string>(
                name: "AttendanceId",
                table: "Medications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Attendances",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_AttendanceId",
                table: "Medications",
                column: "AttendanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Users_UserAccountId",
                table: "Doctors",
                column: "UserAccountId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Attendances_AttendanceId",
                table: "Medications",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Users_UserAccountId",
                table: "Patients",
                column: "UserAccountId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
