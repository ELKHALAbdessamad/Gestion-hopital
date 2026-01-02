using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagement.Migrations
{
    public partial class AddDossierMedicalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateConsultation",
                table: "DossiersMedicaux",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Diagnostic",
                table: "DossiersMedicaux",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "DossiersMedicaux",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Traitement",
                table: "DossiersMedicaux",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateConsultation",
                table: "DossiersMedicaux");

            migrationBuilder.DropColumn(
                name: "Diagnostic",
                table: "DossiersMedicaux");

            migrationBuilder.DropColumn(
                name: "Observations",
                table: "DossiersMedicaux");

            migrationBuilder.DropColumn(
                name: "Traitement",
                table: "DossiersMedicaux");
        }
    }
}
