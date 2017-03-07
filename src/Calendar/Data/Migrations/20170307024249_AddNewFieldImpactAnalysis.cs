using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.Data.Migrations
{
    public partial class AddNewFieldImpactAnalysis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImpactAanlsys",
                table: "Event");

            migrationBuilder.AddColumn<string>(
                name: "ImpactAnalysis",
                table: "Event",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImpactAnalysis",
                table: "Event");

            migrationBuilder.AddColumn<string>(
                name: "ImpactAanlsys",
                table: "Event",
                maxLength: 1000,
                nullable: true);
        }
    }
}
