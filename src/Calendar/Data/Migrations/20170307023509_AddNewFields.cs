using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.Data.Migrations
{
    public partial class AddNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventStatus",
                table: "Event",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FallbackProcedure",
                table: "Event",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImpactAanlsys",
                table: "Event",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaintProcedure",
                table: "Event",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationStep",
                table: "Event",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventStatus",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "FallbackProcedure",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "ImpactAanlsys",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "MaintProcedure",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "VerificationStep",
                table: "Event");
        }
    }
}
