using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareApp.Migrations
{
    public partial class ChangeMedicationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Medications",
                newName: "Indication");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Medications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Medications");

            migrationBuilder.RenameColumn(
                name: "Indication",
                table: "Medications",
                newName: "Quantity");
        }
    }
}
